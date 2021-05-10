using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerParameter
    {
        public virtual string Code { get; private set; }
        public virtual string Name { get; private set; }
        public virtual List<FileCrawlerParameterValue> ParameterValues { get; protected set; }

        protected FileCrawlerParameter() { }

        public FileCrawlerParameter([NotNull] string code, [NotNull] string name)
        {
            Code = Check.NotNullOrWhiteSpace(code, nameof(code));
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));

            ParameterValues = new List<FileCrawlerParameterValue>();
        }
    }
}
