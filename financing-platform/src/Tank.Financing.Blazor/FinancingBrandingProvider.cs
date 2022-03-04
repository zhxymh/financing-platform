using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Tank.Financing.Blazor;

[Dependency(ReplaceServices = true)]
public class FinancingBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Financing";
}
