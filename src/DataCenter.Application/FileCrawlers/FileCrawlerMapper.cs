using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerMapper : IObjectMapper<CreateFileCrawlerDto, FileCrawler>, ITransientDependency
    {
        protected IGuidGenerator GuidGenerator { get; set; }

        public FileCrawlerMapper(IGuidGenerator guidGenerator)
        {
            GuidGenerator = guidGenerator;
        }

        public FileCrawler Map(CreateFileCrawlerDto source)
        {
            FileCrawler destination = new FileCrawler(GuidGenerator.Create(), source.Code, source.Name, source.Type);
            destination.DelaySeconds = source.DelaySeconds;
            destination.UrlDateTimeKind = source.UrlDateTimeKind;
            destination.UrlFormat = source.UrlFormat;
            destination.FileNameFormat = source.FileNameFormat;
            destination.StampFormat = source.StampFormat;
            destination.Periods = source.Periods;
            destination.Parameters = source.Parameters;
            return destination;
        }

        public FileCrawler Map(CreateFileCrawlerDto source, FileCrawler destination)
        {
            destination.DelaySeconds = source.DelaySeconds;
            destination.UrlDateTimeKind = source.UrlDateTimeKind;
            destination.UrlFormat = source.UrlFormat;
            destination.FileNameFormat = source.FileNameFormat;
            destination.StampFormat = source.StampFormat;
            destination.Periods = source.Periods;
            destination.Parameters = source.Parameters;
            return destination;
        }
    }
}
