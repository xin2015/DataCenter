using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerPeriod
    {
        public virtual FileCrawlerPeriodType Type { get; set; }
        public virtual int Start { get; set; }
        public virtual int End { get; set; }
        public virtual int Interval { get; private set; }

        protected FileCrawlerPeriod() { }

        public FileCrawlerPeriod(FileCrawlerPeriodType type, int start, int end, int interval)
        {
            Type = type;
            Start = start;
            End = end;
            Interval = 1;
            SetInterval(interval);
        }

        public virtual void SetInterval(int interval)
        {
            if (interval == 0)
            {
                return;
            }

            Interval = interval;
        }
    }
}
