using Tank.Financing.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Tank.Financing.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FinancingEntityFrameworkCoreModule),
    typeof(FinancingApplicationContractsModule)
)]
public class FinancingDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
    }
}
