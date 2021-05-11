using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataCenter.FileCrawlers
{
    public class UpdateFileCrawlerDto
    {
        [Required]
        [StringLength(DataCenterSharedConsts.MaxCodeLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(DataCenterSharedConsts.MaxNameLength)]
        public string Name { get; set; }
        public FileCrawlerType Type { get; set; }
        public int DelaySeconds { get; set; }
        public DateTimeKind UrlDateTimeKind { get; set; }
        [Required]
        [StringLength(DataCenterSharedConsts.MaxFullNameLength)]
        public string UrlFormat { get; set; }
        [Required]
        [StringLength(DataCenterSharedConsts.MaxNameLength)]
        public string FileNameFormat { get; set; }
        [Required]
        [StringLength(DataCenterSharedConsts.MaxNameLength)]
        public string StampFormat { get; set; }
        [Required]
        [StringLength(DataCenterSharedConsts.MaxJsonStringLength)]
        public string Periods { get; set; }
        [Required]
        [StringLength(DataCenterSharedConsts.MaxFullJsonStringLength)]
        public string Parameters { get; set; }
    }
}
