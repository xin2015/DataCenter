using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerRecordUpdateDto
    {
        [Required]
        public string FileCrawlerCode { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
