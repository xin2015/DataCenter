using DataCenter.FileCrawlers.ParameterCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Json;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public class FileCrawlerRecordAppService : DataCenterAppService, IFileCrawlerRecordAppService
    {
        protected IFileCrawlerRecordRepository FileCrawlerRecordRepository { get; set; }
        protected IFileCrawlerRepository FileCrawlerRepository { get; set; }
        protected IParameterCombinationRepository ParameterCombinationRepository { get; set; }
        protected IJsonSerializer JsonSerializer { get; set; }

        public FileCrawlerRecordAppService(IFileCrawlerRecordRepository fileCrawlerRecordRepository,
            IFileCrawlerRepository fileCrawlerRepository,
            IParameterCombinationRepository parameterCombinationRepository,
            IJsonSerializer jsonSerializer)
        {
            FileCrawlerRecordRepository = fileCrawlerRecordRepository;
            FileCrawlerRepository = fileCrawlerRepository;
            ParameterCombinationRepository = parameterCombinationRepository;
            JsonSerializer = jsonSerializer;
        }

        public async Task<List<FileCrawlerRecordDto>> GetListAsync(GetFileCrawlerRecordListDto input)
        {
            FileCrawler fileCrawler = await FileCrawlerRepository.FindAsync(x => x.Code == input.FileCrawlerCode);
            if (fileCrawler != null)
            {
                List<ParameterCombination> parameterCombinationList = await ParameterCombinationRepository.GetListAsync(fileCrawler.Id);
                ParameterCombination parameterCombination = parameterCombinationList.FirstOrDefault(x => Equal(x.Parameters, input.Parameters));
                if (parameterCombination != null)
                {
                    List<FileCrawlerRecord> list = await FileCrawlerRecordRepository.GetListAsync(parameterCombination.Id, input.Date);
                    return ObjectMapper.Map<List<FileCrawlerRecord>, List<FileCrawlerRecordDto>>(list);
                }
            }
            return new List<FileCrawlerRecordDto>();
        }

        protected bool Equal(string left, string right)
        {
            Dictionary<string, string> leftDic = JsonSerializer.Deserialize<Dictionary<string, string>>(left);
            Dictionary<string, string> rightDic = JsonSerializer.Deserialize<Dictionary<string, string>>(right);
            return leftDic.All(x => rightDic.Contains(x)) && rightDic.All(x => leftDic.Contains(x));
        }
    }
}
