using DataCenter.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DataCenter.FileCrawlers
{
    public class EfCoreFileCrawlerRepository : EfCoreRepository<DataCenterDbContext, FileCrawler, Guid>, IFileCrawlerRepository
    {
        public EfCoreFileCrawlerRepository(IDbContextProvider<DataCenterDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

    }
}
