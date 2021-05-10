using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public class FileCrawlerRecord : Entity<Guid>, IHasCreationTime, IHasModificationTime
    {
        public virtual Guid ParameterCombinationId { get; set; }
        public virtual DateTime SourceTime { get; set; }
        public virtual DateTime TargetTime { get; set; }
        public virtual bool Status { get; set; }
        public virtual string Url { get; set; }
        public virtual string DirectoryName { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Stamp { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }

        protected FileCrawlerRecord() { }

        public FileCrawlerRecord(Guid id, Guid parameterCombinationId, DateTime sourceTime, DateTime targetTime) : base(id)
        {
            ParameterCombinationId = parameterCombinationId;
            SourceTime = sourceTime;
            TargetTime = targetTime;
        }
    }
}
