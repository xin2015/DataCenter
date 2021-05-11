using AutoMapper;
using DataCenter.FileCrawlers;
using Volo.Abp.Guids;

namespace DataCenter
{
    public class DataCenterApplicationAutoMapperProfile : Profile
    {
        public DataCenterApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<FileCrawler, FileCrawlerDto>();
            CreateMap<CreateFileCrawlerDto, FileCrawler>();
        }
    }
}
