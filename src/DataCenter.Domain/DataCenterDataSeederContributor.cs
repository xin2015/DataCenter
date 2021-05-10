using DataCenter.FileCrawlers;
using DataCenter.FileCrawlers.ParameterCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Json;

namespace DataCenter
{
    public class DataCenterDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        protected IFileCrawlerRepository FileCrawlerRepository { get; set; }
        protected ParameterCombinationDomainService ParameterCombinationDomainService { get; set; }
        protected IGuidGenerator GuidGenerator { get; set; }
        protected IJsonSerializer JsonSerializer { get; set; }

        public DataCenterDataSeederContributor(IFileCrawlerRepository fileCrawlerRepository,
            ParameterCombinationDomainService parameterCombinationDomainService,
            IGuidGenerator guidGenerator,
            IJsonSerializer jsonSerializer)
        {
            FileCrawlerRepository = fileCrawlerRepository;
            ParameterCombinationDomainService = parameterCombinationDomainService;
            GuidGenerator = guidGenerator;
            JsonSerializer = jsonSerializer;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            #region 000000 天气图
            if (!await FileCrawlerRepository.AnyAsync(x => x.Code == "000000"))
            {
                FileCrawler fileCrawler = new FileCrawler(GuidGenerator.Create(), "000000", "天气图", FileCrawlerType.Archive)
                {
                    DelaySeconds = 5400,
                    UrlDateTimeKind = DateTimeKind.Utc,
                    UrlFormat = "http://image.nmc.cn/product/{3:yyyy/MM/dd}/WESA/SEVP_NMC_WESA_SFER_{1}_{0}_{2}_P9_{3:yyyyMMddHH}0000000.jpg",
                    FileNameFormat = "SEVP_NMC_WESA_SFER_{1}_{0}_{2}_P9_{3:yyyyMMddHH}0000000.jpg",
                    StampFormat = "{3:MM/dd HH:00}",
                    Periods = "[{\"type\":2,\"start\":8,\"end\":20,\"interval\":12}]",
                    Parameters = "[{\"code\":\"Area\",\"name\":\"区域\",\"parameterValues\":[{\"code\":\"ACWP\",\"name\":\"中国\",\"parameters\":[{\"code\":\"Type\",\"name\":\"类型\",\"parameterValues\":[{\"code\":\"EGH\",\"name\":\"基本天气分析\",\"parameters\":[],\"periods\":[]},{\"code\":\"ESPCT\",\"name\":\"叠加卫星云图\",\"parameters\":[],\"periods\":[]},{\"code\":\"ESPBT\",\"name\":\"叠加雷达拼图\",\"parameters\":[],\"periods\":[]}]}],\"periods\":[]},{\"code\":\"ACHN\",\"name\":\"亚欧\",\"parameters\":[{\"code\":\"Type\",\"name\":\"类型\",\"parameterValues\":[{\"code\":\"EGH\",\"name\":\"基本天气分析\",\"parameters\":[],\"periods\":[]},{\"code\":\"ESPCT\",\"name\":\"叠加卫星云图\",\"parameters\":[],\"periods\":[]}]}],\"periods\":[]},{\"code\":\"ANHE\",\"name\":\"北半球\",\"parameters\":[{\"code\":\"Type\",\"name\":\"类型\",\"parameterValues\":[{\"code\":\"EGH\",\"name\":\"基本天气分析\",\"parameters\":[],\"periods\":[]}]}],\"periods\":[]}]},{\"code\":\"Level\",\"name\":\"层次\",\"parameterValues\":[{\"code\":\"L00\",\"name\":\"地面\",\"parameters\":[],\"periods\":[{\"type\":2,\"start\":2,\"end\":23,\"interval\":3}]},{\"code\":\"L92\",\"name\":\"925hPa\",\"parameters\":[],\"periods\":[]},{\"code\":\"L85\",\"name\":\"850hPa\",\"parameters\":[],\"periods\":[]},{\"code\":\"L70\",\"name\":\"700hPa\",\"parameters\":[],\"periods\":[]},{\"code\":\"L50\",\"name\":\"500hPa\",\"parameters\":[],\"periods\":[]},{\"code\":\"L20\",\"name\":\"200hPa\",\"parameters\":[],\"periods\":[]},{\"code\":\"L10\",\"name\":\"100hPa\",\"parameters\":[],\"periods\":[]}]}]"
                };
                await FileCrawlerRepository.InsertAsync(fileCrawler);
                await ParameterCombinationDomainService.InsertAsync(fileCrawler);
            }
            #endregion
            #region 000100 降水量预报
            if (!await FileCrawlerRepository.AnyAsync(x => x.Code == "000100"))
            {
                FileCrawler fileCrawler = new FileCrawler(GuidGenerator.Create(), "000100", "降水量预报", FileCrawlerType.Forecast)
                {
                    DelaySeconds = -3000,
                    UrlDateTimeKind = DateTimeKind.Utc,
                    UrlFormat = "http://image.nmc.cn/product/{1:yyyy/MM/dd}/STFC/SEVP_NMC_STFC_SFER_ER24_ACHN_L88_P9_{1:yyyyMMddHH}00{6:D3}00.JPG",
                    FileNameFormat = "SEVP_NMC_STFC_SFER_ER24_ACHN_L88_P9_{1:yyyyMMddHH}00{6:D3}00.JPG",
                    StampFormat = "{6}小时",
                    Periods = "[{\"type\":2,\"start\":24,\"end\":168,\"interval\":24}]",
                    Parameters = "[{\"code\":\"TimeOffset\",\"name\":\"起报时间\",\"parameterValues\":[{\"code\":\"8\",\"name\":\"08\",\"parameters\":[],\"periods\":[]},{\"code\":\"20\",\"name\":\"20\",\"parameters\":[],\"periods\":[]}]}]"
                };
                await FileCrawlerRepository.InsertAsync(fileCrawler);
                await ParameterCombinationDomainService.InsertAsync(fileCrawler);
            }
            #endregion
            #region 000101 6小时降水量预报
            if (!await FileCrawlerRepository.AnyAsync(x => x.Code == "000101"))
            {
                FileCrawler fileCrawler = new FileCrawler(GuidGenerator.Create(), "000101", "6小时降水量预报", FileCrawlerType.Forecast)
                {
                    DelaySeconds = -3000,
                    UrlDateTimeKind = DateTimeKind.Utc,
                    UrlFormat = "http://image.nmc.cn/product/{1:yyyy/MM/dd}/STFC/SEVP_NMC_STFC_SFER_ER6T{6:D2}_ACHN_L88_P9_{1:yyyyMMddHH}00{6:D3}06.JPG",
                    FileNameFormat = "SEVP_NMC_STFC_SFER_ER6T{6:D2}_ACHN_L88_P9_{1:yyyyMMddHH}00{6:D3}06.JPG",
                    StampFormat = "{6}小时",
                    Periods = "[{\"type\":2,\"start\":6,\"end\":24,\"interval\":6}]",
                    Parameters = "[{\"code\":\"TimeOffset\",\"name\":\"起报时间\",\"parameterValues\":[{\"code\":\"8\",\"name\":\"08\",\"parameters\":[],\"periods\":[]},{\"code\":\"20\",\"name\":\"20\",\"parameters\":[],\"periods\":[]}]}]"
                };
                await FileCrawlerRepository.InsertAsync(fileCrawler);
                await ParameterCombinationDomainService.InsertAsync(fileCrawler);
            }
            #endregion
            #region 000102 气温预报
            if (!await FileCrawlerRepository.AnyAsync(x => x.Code == "000102"))
            {
                FileCrawler fileCrawler = new FileCrawler(GuidGenerator.Create(), "000102", "气温预报", FileCrawlerType.Forecast)
                {
                    DelaySeconds = -3000,
                    UrlDateTimeKind = DateTimeKind.Local,
                    UrlFormat = "http://image.nmc.cn/product/{2:yyyy/MM/dd}/RFFC/SEVP_NMC_RFFC_SNWFD_{0}_ACHN_L88_P9_{2:yyyyMMddHH}00{7:D3}12.jpg",
                    FileNameFormat = "SEVP_NMC_RFFC_SNWFD_{0}_ACHN_L88_P9_{2:yyyyMMddHH}00{7:D3}12.jpg",
                    StampFormat = "{7}小时",
                    Periods = "[{\"type\":2,\"start\":24,\"end\":168,\"interval\":24}]",
                    Parameters = "[{\"code\":\"Type\",\"name\":\"类型\",\"parameterValues\":[{\"code\":\"ETM\",\"name\":\"最高气温\",\"parameters\":[],\"periods\":[]},{\"code\":\"ETN\",\"name\":\"最低气温\",\"parameters\":[],\"periods\":[]}]},{\"code\":\"TimeOffset\",\"name\":\"起报时间\",\"parameterValues\":[{\"code\":\"8\",\"name\":\"08\",\"parameters\":[],\"periods\":[]},{\"code\":\"20\",\"name\":\"20\",\"parameters\":[],\"periods\":[]}]}]"
                };
                await FileCrawlerRepository.InsertAsync(fileCrawler);
                await ParameterCombinationDomainService.InsertAsync(fileCrawler);
            }
            #endregion
        }
    }
}
