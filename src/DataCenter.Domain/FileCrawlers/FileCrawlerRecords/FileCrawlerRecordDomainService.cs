using DataCenter.FileCrawlers.ParameterCombinations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Services;
using Volo.Abp.Json;

namespace DataCenter.FileCrawlers.FileCrawlerRecords
{
    public class FileCrawlerRecordDomainService : DomainService
    {
        protected IFileCrawlerRecordRepository FileCrawlerRecordRepository { get; set; }
        protected IParameterCombinationRepository ParameterCombinationRepository { get; set; }
        protected IJsonSerializer JsonSerializer { get; set; }
        protected IConfiguration Configuration { get; set; }
        protected IBackgroundJobManager BackgroundJobManager { get; set; }

        public FileCrawlerRecordDomainService(IFileCrawlerRecordRepository fileCrawlerRecordRepository,
            IParameterCombinationRepository parameterCombinationRepository,
            IJsonSerializer jsonSerializer,
            IConfiguration configuration,
            IBackgroundJobManager backgroundJobManager)
        {
            FileCrawlerRecordRepository = fileCrawlerRecordRepository;
            ParameterCombinationRepository = parameterCombinationRepository;
            JsonSerializer = jsonSerializer;
            Configuration = configuration;
            BackgroundJobManager = backgroundJobManager;
        }

        public async Task InsertAsync(FileCrawler fileCrawler, DateTime date)
        {
            List<ParameterCombination> parameterCombinations = await ParameterCombinationRepository.GetListAsync(fileCrawler.Id, true);
            foreach (ParameterCombination parameterCombination in parameterCombinations)
            {
                Dictionary<string, object> dic = JsonSerializer.Deserialize<Dictionary<string, object>>(parameterCombination.Parameters);
                List<FileCrawlerPeriod> periods = JsonSerializer.Deserialize<List<FileCrawlerPeriod>>(parameterCombination.Periods);
                if (fileCrawler.Type == FileCrawlerType.Archive)
                {
                    foreach (FileCrawlerPeriod period in periods)
                    {
                        Func<DateTime, int, DateTime> getTimeFunc = GetGetTimeFunc(period.Type);
                        Func<TimeSpan, int> getOffsetFunc = GetGetOffsetFunc(period.Type);
                        for (int i = period.Start; i <= period.End; i += period.Interval)
                        {
                            DateTime sourceTime = getTimeFunc(date, i);
                            dic["SourceTime"] = sourceTime;
                            dic["SourceTimeSpan"] = sourceTime - date;
                            dic["SourceTimeOffset"] = i;
                            object[] args = dic.Values.ToArray();
                            object[] urlArgs;
                            if (fileCrawler.UrlDateTimeKind == DateTimeKind.Utc)
                            {
                                DateTime sourceTimeUtc = sourceTime.ToUniversalTime();
                                dic["SourceTime"] = sourceTimeUtc;
                                dic["SourceTimeSpan"] = sourceTimeUtc - sourceTimeUtc.Date;
                                dic["SourceTimeOffset"] = getOffsetFunc(sourceTimeUtc - sourceTimeUtc.Date);
                                urlArgs = dic.Values.ToArray();
                            }
                            else
                            {
                                urlArgs = args;
                            }
                            FileCrawlerRecord record = new FileCrawlerRecord(GuidGenerator.Create(), parameterCombination.Id, sourceTime, sourceTime)
                            {
                                Url = string.Format(fileCrawler.UrlFormat, urlArgs),
                                DirectoryName = string.Format("{0}\\{1:yyyyMMdd}", Configuration["Settings:Crawlers.Files.RootDirectory"], sourceTime),
                                FileName = string.Format(fileCrawler.FileNameFormat, args),
                                Stamp = string.Format(fileCrawler.StampFormat, args)
                            };
                            await FileCrawlerRecordRepository.InsertAsync(record);
                            await EnqueueAsync(record.Id, sourceTime, fileCrawler.DelaySeconds);
                        }
                    }
                }
                else
                {
                    int sourceTimeOffset = int.Parse(dic["TimeOffset"] as string);
                    DateTime sourceTime = date.AddHours(sourceTimeOffset);
                    dic["SourceTime"] = sourceTime;
                    dic["SourceTimeSpan"] = sourceTime - date;
                    dic["SourceTimeOffset"] = sourceTimeOffset;
                    foreach (FileCrawlerPeriod period in periods)
                    {
                        Func<DateTime, int, DateTime> getTimeFunc = GetGetTimeFunc(period.Type);
                        for (int i = period.Start; i <= period.End; i += period.Interval)
                        {
                            DateTime targetTime = getTimeFunc(sourceTime, i);
                            dic["TargetTime"] = targetTime;
                            dic["TargetTimeSpan"] = targetTime - sourceTime;
                            dic["TargetTimeOffset"] = i;
                            object[] args = dic.Values.ToArray();
                            object[] urlArgs;
                            if (fileCrawler.UrlDateTimeKind == DateTimeKind.Utc)
                            {
                                DateTime sourceTimeUtc = sourceTime.ToUniversalTime();
                                DateTime targetTimeUtc = targetTime.ToUniversalTime();
                                dic["SourceTime"] = sourceTimeUtc;
                                dic["SourceTimeSpan"] = sourceTimeUtc - sourceTimeUtc.Date;
                                dic["SourceTimeOffset"] = (sourceTimeUtc - sourceTimeUtc.Date).Hours;
                                dic["TargetTime"] = targetTimeUtc;
                                urlArgs = dic.Values.ToArray();
                            }
                            else
                            {
                                urlArgs = args;
                            }
                            FileCrawlerRecord record = new FileCrawlerRecord(GuidGenerator.Create(), parameterCombination.Id, sourceTime, targetTime)
                            {
                                Url = string.Format(fileCrawler.UrlFormat, urlArgs),
                                DirectoryName = string.Format("{0}\\{1:yyyyMMdd}", Configuration["Settings:Crawlers.Files.RootDirectory"], sourceTime),
                                FileName = string.Format(fileCrawler.FileNameFormat, args),
                                Stamp = string.Format(fileCrawler.StampFormat, args)
                            };
                            await FileCrawlerRecordRepository.InsertAsync(record);
                            await EnqueueAsync(record.Id, sourceTime, fileCrawler.DelaySeconds);
                        }
                    }
                }
            }
        }

        protected Func<DateTime, int, DateTime> GetGetTimeFunc(FileCrawlerPeriodType periodType)
        {
            switch (periodType)
            {
                case FileCrawlerPeriodType.Minute:
                    {
                        return (time, i) => { return time.AddMinutes(i); };
                    }
                case FileCrawlerPeriodType.Hour:
                    {
                        return (time, i) => { return time.AddHours(i); };
                    }
                case FileCrawlerPeriodType.Day:
                    {
                        return (time, i) => { return time.AddDays(i); };
                    }
                default:
                    {
                        return (time, i) => { return time; };
                    }
            }
        }

        protected Func<TimeSpan, int> GetGetOffsetFunc(FileCrawlerPeriodType periodType)
        {
            switch (periodType)
            {
                case FileCrawlerPeriodType.Minute:
                    {
                        return (timeSpan) => { return (int)timeSpan.TotalMinutes; };
                    }
                case FileCrawlerPeriodType.Hour:
                    {
                        return (timeSpan) => { return (int)timeSpan.TotalHours; };
                    }
                case FileCrawlerPeriodType.Day:
                    {
                        return (timeSpan) => { return (int)timeSpan.TotalDays; };
                    }
                default:
                    {
                        return (timeSpan) => { return 0; };
                    }
            }
        }

        protected async Task EnqueueAsync(Guid fileCrawlerRecordId, DateTime sourceTime, int delaySeconds)
        {
            DateTime nextTryTime = sourceTime.AddSeconds(delaySeconds);
            if (nextTryTime <= Clock.Now)
            {
                await BackgroundJobManager.EnqueueAsync(new FileCrawlerRecordDownloadingArgs(fileCrawlerRecordId));
            }
            else
            {
                await BackgroundJobManager.EnqueueAsync(new FileCrawlerRecordDownloadingArgs(fileCrawlerRecordId), BackgroundJobPriority.Normal, nextTryTime - Clock.Now);
            }
        }
    }
}
