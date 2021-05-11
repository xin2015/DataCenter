using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DataCenter.FileCrawlers.ParameterCombinations
{
    public interface IParameterCombinationRepository : IRepository<ParameterCombination, Guid>
    {
        Task<List<ParameterCombination>> GetListAsync(Guid fileCrawlerId);
        Task<List<ParameterCombination>> GetListAsync(Guid fileCrawlerId, bool enabled);
    }
}
