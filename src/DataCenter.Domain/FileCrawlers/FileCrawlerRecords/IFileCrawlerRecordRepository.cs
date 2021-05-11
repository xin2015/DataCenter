using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public interface IFileCrawlerRecordRepository : IRepository<FileCrawlerRecord, Guid>
    {
        Task<List<FileCrawlerRecord>> GetListAsync(Guid parameterCombinationId, DateTime date);
    }
}
