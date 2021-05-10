using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    /// <summary>
    /// 文件爬虫记录下载参数
    /// </summary>
    public class FileCrawlerRecordDownloadingArgs
    {
        public Guid FileCrawlerRecordId { get; set; }

        public FileCrawlerRecordDownloadingArgs(Guid fileCrawlerRecordId)
        {
            FileCrawlerRecordId = fileCrawlerRecordId;
        }
    }
}
