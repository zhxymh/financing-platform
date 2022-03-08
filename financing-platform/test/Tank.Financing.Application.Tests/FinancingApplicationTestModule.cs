using IdentityServer4.Models;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Tank.Financing;

[DependsOn(
    typeof(FinancingApplicationModule),
    typeof(FinancingDomainTestModule)
    )]
public class FinancingApplicationTestModule : AbpModule
{
    [Fact]
    public void Test()
    {
        var foo = "1q2w3e*".Sha256();
        foo.ShouldBeEmpty();
    }
}
