using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Timing;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public class FileCrawlerRecordDownloadingWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public FileCrawlerRecordDownloadingWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
        {
            Timer.Period = 60000; //1 minute
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogDebug("Starting: {0}", GetType());

            IClock clock = workerContext.ServiceProvider.GetRequiredService<IClock>();
            if (clock.Now.Hour == 1 && clock.Now.Minute == 23)
            {
                Logger.LogInformation("Starting: {0}", GetType());
                IFileCrawlerRepository fileCrawlerRepository = workerContext.ServiceProvider.GetRequiredService<IFileCrawlerRepository>();
                List<FileCrawler> fileCrawlerList = await fileCrawlerRepository.GetListAsync();
                FileCrawlerRecordDomainService recordDomainService = workerContext.ServiceProvider.GetRequiredService<FileCrawlerRecordDomainService>();
                foreach (FileCrawler fileCrawler in fileCrawlerList)
                {
                    await recordDomainService.InsertAsync(fileCrawler, clock.Now.Date);
                }
                Logger.LogInformation("Completed: {0}", GetType());
            }

            Logger.LogDebug("Completed: {0}", GetType());
        }
    }
}
