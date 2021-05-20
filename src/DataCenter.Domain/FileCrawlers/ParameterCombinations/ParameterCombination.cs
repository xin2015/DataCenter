using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DataCenter.FileCrawlers.ParameterCombinations
{
    public class ParameterCombination : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid FileCrawlerId { get; set; }
        public virtual string Parameters { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string Periods { get; set; }

        protected ParameterCombination() { }

        public ParameterCombination(Guid id, Guid fileCrawlerId) : base(id)
        {
            FileCrawlerId = fileCrawlerId;
        }
    }
}
