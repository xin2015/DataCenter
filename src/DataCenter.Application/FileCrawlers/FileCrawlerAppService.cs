using DataCenter.FileCrawlers.FileCrawlerRecords;
using DataCenter.FileCrawlers.ParameterCombinations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerAppService : DataCenterAppService, IFileCrawlerAppService
    {
        protected IFileCrawlerRepository FileCrawlerRepository { get; set; }
        protected FileCrawlerDomainService FileCrawlerDomainService { get; set; }
        protected ParameterCombinationDomainService ParameterCombinationDomainService { get; set; }
        protected FileCrawlerRecordDomainService FileCrawlerRecordDomainService { get; set; }

        public FileCrawlerAppService(IFileCrawlerRepository fileCrawlerRepository,
            FileCrawlerDomainService fileCrawlerDomainService,
            ParameterCombinationDomainService parameterCombinationDomainService,
            FileCrawlerRecordDomainService fileCrawlerRecordDomainService)
        {
            FileCrawlerRepository = fileCrawlerRepository;
            FileCrawlerDomainService = fileCrawlerDomainService;
            ParameterCombinationDomainService = parameterCombinationDomainService;
            FileCrawlerRecordDomainService = fileCrawlerRecordDomainService;
        }

        [Route("api/app/file-crawler/get-list")]
        public async Task<List<FileCrawlerDto>> GetListAsync(GetFileCrawlerListDto input)
        {
            List<FileCrawler> list = await FileCrawlerRepository.GetListAsync();
            return ObjectMapper.Map<List<FileCrawler>, List<FileCrawlerDto>>(list);
        }

        public async Task<FileCrawlerDto> GetAsync(GetFileCrawlerDto input)
        {
            FileCrawler fileCrawler = await FileCrawlerRepository.GetAsync(x => x.Code == input.FileCrawlerCode);
            return ObjectMapper.Map<FileCrawler, FileCrawlerDto>(fileCrawler);
        }

        public async Task FileCrawlerRecordInsertAsync(FileCrawlerRecordInsertDto input)
        {
            FileCrawler fileCrawler = await FileCrawlerRepository.GetAsync(x => x.Code == input.FileCrawlerCode);
            if (fileCrawler != null)
            {
                await FileCrawlerRecordDomainService.InsertAsync(fileCrawler, input.Date);
            }
        }

        public async Task FileCrawlerRecordUpdateAsync(FileCrawlerRecordUpdateDto input)
        {
            FileCrawler fileCrawler = await FileCrawlerRepository.GetAsync(x => x.Code == input.FileCrawlerCode);
            if (fileCrawler != null)
            {
                await FileCrawlerRecordDomainService.UpdateAsync(fileCrawler, input.Date);
            }
        }

        public async Task<FileCrawlerDto> CreateAsync(CreateFileCrawlerDto input)
        {
            FileCrawler fileCrawler = ObjectMapper.Map<CreateFileCrawlerDto, FileCrawler>(input);
            await FileCrawlerDomainService.CreateAsync(fileCrawler);
            await FileCrawlerRepository.InsertAsync(fileCrawler);
            await ParameterCombinationDomainService.InsertAsync(fileCrawler);
            return ObjectMapper.Map<FileCrawler, FileCrawlerDto>(fileCrawler);
        }

        public async Task UpdateAsync(Guid id, UpdateFileCrawlerDto input)
        {
            FileCrawler fileCrawler = await FileCrawlerRepository.GetAsync(id);
            if (fileCrawler.Code != input.Code)
            {
                await FileCrawlerDomainService.ChangeCodeAsync(fileCrawler, input.Code);
            }

            fileCrawler.SetName(input.Name);
            fileCrawler.Type = input.Type;
            fileCrawler.DelaySeconds = input.DelaySeconds;
            fileCrawler.UrlDateTimeKind = input.UrlDateTimeKind;
            fileCrawler.UrlFormat = input.UrlFormat;
            fileCrawler.FileNameFormat = input.FileNameFormat;
            fileCrawler.StampFormat = input.StampFormat;
            fileCrawler.Periods = input.Periods;
            fileCrawler.Parameters = input.Parameters;

            await FileCrawlerRepository.UpdateAsync(fileCrawler);
            await ParameterCombinationDomainService.UpdateAsync(fileCrawler);
        }
    }
}
