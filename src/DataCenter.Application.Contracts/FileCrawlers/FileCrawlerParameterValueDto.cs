using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerParameterValueDto
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public List<FileCrawlerParameterDto> Parameters { get; protected set; }
        public List<FileCrawlerPeriodDto> Periods { get; protected set; }
    }
}
