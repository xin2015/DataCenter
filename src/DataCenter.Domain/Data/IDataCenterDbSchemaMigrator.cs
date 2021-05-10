using System.Threading.Tasks;

namespace DataCenter.Data
{
    public interface IDataCenterDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
