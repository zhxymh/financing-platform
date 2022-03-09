using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tank.Financing.Data;

/* This is used if database provider does't define
 * IFinancingDbSchemaMigrator implementation.
 */
public class NullFinancingDbSchemaMigrator : IFinancingDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
