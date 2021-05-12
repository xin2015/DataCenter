using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public class GetFileCrawlerRecordListDto
    {
        [Required]
        [StringLength(DataCenterSharedConsts.MaxCodeLength)]
        public string FileCrawlerCode { get; set; }
        public string Parameters { get; set; }
        public DateTime Date { get; set; }
    }
}
