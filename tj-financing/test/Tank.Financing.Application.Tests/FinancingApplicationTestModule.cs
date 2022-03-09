using Volo.Abp.Modularity;

namespace Tank.Financing;

[DependsOn(
    typeof(FinancingApplicationModule),
    typeof(FinancingDomainTestModule)
    )]
public class FinancingApplicationTestModule : AbpModule
{

}
