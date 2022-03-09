using Tank.Financing.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tank.Financing.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class FinancingController : AbpControllerBase
{
    protected FinancingController()
    {
        LocalizationResource = typeof(FinancingResource);
    }
}
