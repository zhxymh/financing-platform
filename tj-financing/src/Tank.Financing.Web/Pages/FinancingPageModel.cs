using Tank.Financing.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tank.Financing.Web.Pages;

public abstract class FinancingPageModel : AbpPageModel
{
    protected FinancingPageModel()
    {
        LocalizationResourceType = typeof(FinancingResource);
    }
}
