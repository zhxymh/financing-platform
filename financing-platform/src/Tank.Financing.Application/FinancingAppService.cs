using Tank.Financing.Localization;
using Volo.Abp.Application.Services;

namespace Tank.Financing;

/* Inherit your application services from this class.
 */
public abstract class FinancingAppService : ApplicationService
{
    protected FinancingAppService()
    {
        LocalizationResource = typeof(FinancingResource);
    }
}
