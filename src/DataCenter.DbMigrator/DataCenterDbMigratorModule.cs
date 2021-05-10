using DataCenter.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace DataCenter.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(DataCenterEntityFrameworkCoreDbMigrationsModule),
        typeof(DataCenterApplicationContractsModule)
        )]
    public class DataCenterDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
