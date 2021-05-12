using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Json;

namespace DataCenter.FileCrawlers.ParameterCombinations
{
    public class ParameterCombinationDomainService : DomainService
    {
        protected IParameterCombinationRepository ParameterCombinationRepository { get; set; }
        protected IJsonSerializer JsonSerializer { get; set; }

        public ParameterCombinationDomainService(IParameterCombinationRepository parameterCombinationRepository,
            IJsonSerializer jsonSerializer)
        {
            ParameterCombinationRepository = parameterCombinationRepository;
            JsonSerializer = jsonSerializer;
        }

        public async Task InsertAsync(FileCrawler fileCrawler)
        {
            List<Dictionary<string, string>> list = GetParameterCombinations(fileCrawler);
            foreach (Dictionary<string, string> dic in list)
            {
                ParameterCombination parameterCombination = new ParameterCombination(GuidGenerator.Create(), fileCrawler.Id);
                if (dic.ContainsKey("Periods"))
                {
                    parameterCombination.Periods = dic["Periods"];
                    dic.Remove("Periods");
                }
                else
                {
                    parameterCombination.Periods = fileCrawler.Periods;
                }
                parameterCombination.Parameters = JsonSerializer.Serialize(dic);
                parameterCombination.Enabled = true;
                await ParameterCombinationRepository.InsertAsync(parameterCombination);
            }
        }

        protected List<Dictionary<string, string>> GetParameterCombinations(FileCrawler fileCrawler)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            list.Add(new Dictionary<string, string>());
            List<FileCrawlerParameter> parameters = JsonSerializer.Deserialize<List<FileCrawlerParameter>>(fileCrawler.Parameters);
            foreach (FileCrawlerParameter parameter in parameters)
            {
                list = CartesianProduct(list, GetParameterCombinations(parameter));
            }
            return list;
        }

        protected List<Dictionary<string, string>> GetParameterCombinations(FileCrawlerParameter parameter)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (FileCrawlerParameterValue parameterValue in parameter.ParameterValues)
            {
                list.AddRange(GetParameterCombinations(parameter.Code, parameterValue));
            }
            return list;
        }

        protected List<Dictionary<string, string>> GetParameterCombinations(string code, FileCrawlerParameterValue parameterValue)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(code, parameterValue.Code);
            if (parameterValue.Periods.Any())
            {
                dic.Add("Periods", JsonSerializer.Serialize(parameterValue.Periods));
            }
            list.Add(dic);
            foreach (FileCrawlerParameter parameter in parameterValue.Parameters)
            {
                list = CartesianProduct(list, GetParameterCombinations(parameter));
            }
            return list;
        }

        /// <summary>
        /// 笛卡尔乘积
        /// </summary>
        /// <param name="leftList"></param>
        /// <param name="rightList"></param>
        /// <returns></returns>
        protected List<Dictionary<string, string>> CartesianProduct(List<Dictionary<string, string>> leftList, List<Dictionary<string, string>> rightList)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (Dictionary<string, string> left in leftList)
            {
                foreach (Dictionary<string, string> right in rightList)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>(left);
                    foreach (var item in right)
                    {
                        dic[item.Key] = item.Value;
                    }
                    list.Add(dic);
                }
            }
            return list;
        }

        public async Task UpdateAsync(FileCrawler fileCrawler)
        {
            List<ParameterCombination> parameterCombinationList = await ParameterCombinationRepository.GetListAsync(fileCrawler.Id);
            parameterCombinationList.ForEach(x => x.Enabled = false);
            List<Dictionary<string, string>> list = GetParameterCombinations(fileCrawler);
            foreach (Dictionary<string, string> dic in list)
            {
                string periods;
                if (dic.ContainsKey("Periods"))
                {
                    periods = dic["Periods"];
                    dic.Remove("Periods");
                }
                else
                {
                    periods = fileCrawler.Periods;
                }
                ParameterCombination parameterCombination = parameterCombinationList.FirstOrDefault(x => Equal(x, dic));
                if (parameterCombination == null)
                {
                    parameterCombination = new ParameterCombination(GuidGenerator.Create(), fileCrawler.Id);
                    parameterCombination.Periods = periods;
                    parameterCombination.Parameters = JsonSerializer.Serialize(dic);
                    parameterCombination.Enabled = true;
                    await ParameterCombinationRepository.InsertAsync(parameterCombination);
                }
                else
                {
                    parameterCombination.Periods = periods;
                    parameterCombination.Enabled = true;
                }
            }
            await ParameterCombinationRepository.UpdateManyAsync(parameterCombinationList);
        }

        protected bool Equal(ParameterCombination parameterCombination, Dictionary<string, string> dic)
        {
            Dictionary<string, string> parameters = JsonSerializer.Deserialize<Dictionary<string, string>>(parameterCombination.Parameters);
            return parameters.All(x => dic.Contains(x)) && dic.All(x => parameters.Contains(x));
        }
    }
}
