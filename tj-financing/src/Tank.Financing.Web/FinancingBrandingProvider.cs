using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Tank.Financing.Web;

[Dependency(ReplaceServices = true)]
public class FinancingBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Financing";
}
