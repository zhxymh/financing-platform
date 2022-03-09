using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tank.Financing.Data;
using Volo.Abp.DependencyInjection;

namespace Tank.Financing.EntityFrameworkCore;

public class EntityFrameworkCoreFinancingDbSchemaMigrator
    : IFinancingDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreFinancingDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the FinancingDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<FinancingDbContext>()
            .Database
            .MigrateAsync();
    }
}
