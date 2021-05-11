using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataCenter.FileCrawlers
{
    public class GetFileCrawlerDto
    {
        [Required]
        public string FileCrawlerCode { get; set; }
    }
}
