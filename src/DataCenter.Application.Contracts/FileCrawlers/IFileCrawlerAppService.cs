using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataCenter.FileCrawlers
{
    public interface IFileCrawlerAppService
    {
        Task FileCrawlerRecordInsertAsync(FileCrawlerRecordInsertDto input);

        Task FileCrawlerRecordUpdateAsync(FileCrawlerRecordUpdateDto input);

        Task<FileCrawlerDto> CreateAsync(CreateFileCrawlerDto input);

        Task UpdateAsync(Guid id, UpdateFileCrawlerDto input);
    }
}
