using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace DataCenter.FileCrawlers
{
    public class FileCrawler : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Code { get; private set; }
        public virtual string Name { get; private set; }
        public virtual FileCrawlerType Type { get; set; }
        public virtual int DelaySeconds { get; set; }
        public virtual DateTimeKind UrlDateTimeKind { get; set; }
        public virtual string UrlFormat { get; set; }
        public virtual string FileNameFormat { get; set; }
        public virtual string StampFormat { get; set; }
        public virtual string Periods { get; set; }
        public virtual string Parameters { get; set; }

        protected FileCrawler() { }

        public FileCrawler(Guid id, [NotNull] string code, [NotNull] string name, FileCrawlerType type) : base(id)
        {
            SetCode(code);
            SetName(name);
            Type = type;
        }

        public virtual void SetCode([NotNull] string code)
        {
            Code = Check.NotNullOrWhiteSpace(code, nameof(code), DataCenterSharedConsts.MaxCodeLength);
        }

        public virtual void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), DataCenterSharedConsts.MaxNameLength);
        }
    }
}
