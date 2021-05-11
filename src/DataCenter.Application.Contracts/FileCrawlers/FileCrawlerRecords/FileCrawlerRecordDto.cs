using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public class FileCrawlerRecordDto : EntityDto<Guid>
    {
        public Guid ParameterCombinationId { get; set; }
        public DateTime SourceTime { get; set; }
        public DateTime TargetTime { get; set; }
        public string DirectoryName { get; set; }
        public string FileName { get; set; }
        public string Stamp { get; set; }
    }
}
