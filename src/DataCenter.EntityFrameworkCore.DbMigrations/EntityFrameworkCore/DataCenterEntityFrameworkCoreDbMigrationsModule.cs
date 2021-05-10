using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace DataCenter.EntityFrameworkCore
{
    [DependsOn(
        typeof(DataCenterEntityFrameworkCoreModule)
        )]
    public class DataCenterEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DataCenterMigrationsDbContext>();
        }
    }
}
