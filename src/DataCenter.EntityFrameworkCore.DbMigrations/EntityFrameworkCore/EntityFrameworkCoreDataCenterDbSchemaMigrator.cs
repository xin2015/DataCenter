using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataCenter.Data;
using Volo.Abp.DependencyInjection;

namespace DataCenter.EntityFrameworkCore
{
    public class EntityFrameworkCoreDataCenterDbSchemaMigrator
        : IDataCenterDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreDataCenterDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the DataCenterMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<DataCenterMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}