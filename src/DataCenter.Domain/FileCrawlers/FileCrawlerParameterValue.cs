using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerParameterValue
    {
        public virtual string Code { get; private set; }
        public virtual string Name { get; private set; }
        public virtual List<FileCrawlerParameter> Parameters { get; protected set; }
        public virtual List<FileCrawlerPeriod> Periods { get; protected set; }

        protected FileCrawlerParameterValue() { }

        public FileCrawlerParameterValue([NotNull] string code, [NotNull] string name)
        {
            Code = Check.NotNullOrWhiteSpace(code, nameof(code));
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));

            Parameters = new List<FileCrawlerParameter>();
            Periods = new List<FileCrawlerPeriod>();
        }
    }
}
