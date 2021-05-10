using DataCenter.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DataCenter
{
    [DependsOn(
        typeof(DataCenterEntityFrameworkCoreTestModule)
        )]
    public class DataCenterDomainTestModule : AbpModule
    {

    }
}