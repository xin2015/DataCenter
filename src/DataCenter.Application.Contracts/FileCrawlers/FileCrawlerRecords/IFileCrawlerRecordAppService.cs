using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public interface IFileCrawlerRecordAppService : IApplicationService
    {
        Task<List<FileCrawlerRecordDto>> GetListAsync(GetFileCrawlerRecordListDto input);
    }
}
