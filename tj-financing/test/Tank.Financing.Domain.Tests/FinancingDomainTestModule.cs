using Tank.Financing.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tank.Financing;

[DependsOn(
    typeof(FinancingEntityFrameworkCoreTestModule)
    )]
public class FinancingDomainTestModule : AbpModule
{

}
