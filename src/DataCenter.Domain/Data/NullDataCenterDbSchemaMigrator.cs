using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace DataCenter.Data
{
    /* This is used if database provider does't define
     * IDataCenterDbSchemaMigrator implementation.
     */
    public class NullDataCenterDbSchemaMigrator : IDataCenterDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}