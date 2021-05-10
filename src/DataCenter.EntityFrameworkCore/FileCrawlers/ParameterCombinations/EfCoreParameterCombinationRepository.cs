using DataCenter.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DataCenter.FileCrawlers.ParameterCombinations
{
    public class EfCoreParameterCombinationRepository : EfCoreRepository<DataCenterDbContext, ParameterCombination, Guid>, IParameterCombinationRepository
    {
        public EfCoreParameterCombinationRepository(IDbContextProvider<DataCenterDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<ParameterCombination>> GetListAsync(Guid fileCrawlerId, bool enabled)
        {
            return await GetListAsync(x => x.FileCrawlerId == fileCrawlerId && x.Enabled == enabled);
        }
    }
}
