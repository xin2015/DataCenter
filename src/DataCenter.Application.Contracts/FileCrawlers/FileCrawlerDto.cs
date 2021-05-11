using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerDto : EntityDto<Guid>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public FileCrawlerType Type { get; set; }
        public int DelaySeconds { get; set; }
        public DateTimeKind UrlDateTimeKind { get; set; }
        public string UrlFormat { get; set; }
        public string FileNameFormat { get; set; }
        public string StampFormat { get; set; }
        public string Periods { get; set; }
        public string Parameters { get; set; }
    }
}
