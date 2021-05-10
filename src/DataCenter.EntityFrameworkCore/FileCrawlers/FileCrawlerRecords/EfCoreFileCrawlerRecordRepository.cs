using DataCenter.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public class EfCoreFileCrawlerRecordRepository : EfCoreRepository<DataCenterDbContext, FileCrawlerRecord, Guid>, IFileCrawlerRecordRepository
    {
        public EfCoreFileCrawlerRecordRepository(IDbContextProvider<DataCenterDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
