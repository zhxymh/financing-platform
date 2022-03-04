using System.Threading.Tasks;

namespace Tank.Financing.Data;

public interface IFinancingDbSchemaMigrator
{
    Task MigrateAsync();
}
