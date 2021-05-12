using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DataCenter.FileCrawlers
{
    public interface IFileCrawlerAppService : IApplicationService
    {
        Task<List<FileCrawlerDto>> GetListAsync(GetFileCrawlerListDto input);

        Task<FileCrawlerDto> GetAsync(GetFileCrawlerDto input);

        Task FileCrawlerRecordInsertAsync(FileCrawlerRecordInsertDto input);

        Task FileCrawlerRecordUpdateAsync(FileCrawlerRecordUpdateDto input);

        Task<FileCrawlerDto> CreateAsync(CreateFileCrawlerDto input);

        Task UpdateAsync(Guid id, UpdateFileCrawlerDto input);
    }
}
