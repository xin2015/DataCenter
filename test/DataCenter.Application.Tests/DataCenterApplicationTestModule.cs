using Volo.Abp.Modularity;

namespace DataCenter
{
    [DependsOn(
        typeof(DataCenterApplicationModule),
        typeof(DataCenterDomainTestModule)
        )]
    public class DataCenterApplicationTestModule : AbpModule
    {

    }
}