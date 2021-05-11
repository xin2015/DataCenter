using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerParameterDto
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public List<FileCrawlerParameterValueDto> ParameterValues { get; protected set; }
    }
}
