using Tank.Financing.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tank.Financing.Web.Public.Pages;

/* Inherit your Page Model classes from this class.
 */
public abstract class FinancingPublicPageModel : AbpPageModel
{
    protected FinancingPublicPageModel()
    {
        LocalizationResourceType = typeof(FinancingResource);
    }
}
