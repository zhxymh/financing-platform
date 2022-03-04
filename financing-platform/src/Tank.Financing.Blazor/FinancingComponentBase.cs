using Tank.Financing.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Tank.Financing.Blazor;

public abstract class FinancingComponentBase : AbpComponentBase
{
    protected FinancingComponentBase()
    {
        LocalizationResource = typeof(FinancingResource);
    }
}
