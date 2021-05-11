using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerPeriodDto
    {
        public FileCrawlerPeriodType Type { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int Interval { get; private set; }
    }
}
