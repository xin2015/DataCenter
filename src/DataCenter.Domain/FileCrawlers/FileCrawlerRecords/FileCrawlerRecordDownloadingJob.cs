using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public class FileCrawlerRecordDownloadingJob : AsyncBackgroundJob<FileCrawlerRecordDownloadingArgs>, ITransientDependency
    {
        protected IFileCrawlerRecordRepository FileCrawlerRecordRepository { get; set; }
        protected IHttpClientFactory HttpClientFactory { get; set; }
        protected IClock Clock { get; set; }

        public FileCrawlerRecordDownloadingJob(IFileCrawlerRecordRepository fileCrawlerRecordRepository,
            IHttpClientFactory httpClientFactory,
            IClock clock)
        {
            FileCrawlerRecordRepository = fileCrawlerRecordRepository;
            HttpClientFactory = httpClientFactory;
            Clock = clock;
        }

        public override async Task ExecuteAsync(FileCrawlerRecordDownloadingArgs args)
        {
            FileCrawlerRecord record = await FileCrawlerRecordRepository.GetAsync(args.FileCrawlerRecordId);
            if (!Directory.Exists(record.DirectoryName))
            {
                Directory.CreateDirectory(record.DirectoryName);
            }
            HttpClient httpClient = HttpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(20);
            byte[] bytes = await httpClient.GetByteArrayAsync(record.Url);
            await File.WriteAllBytesAsync(string.Format("{0}\\{1}", record.DirectoryName, record.FileName), bytes);
            record.Status = true;
            await FileCrawlerRecordRepository.UpdateAsync(record);
        }
    }
}
