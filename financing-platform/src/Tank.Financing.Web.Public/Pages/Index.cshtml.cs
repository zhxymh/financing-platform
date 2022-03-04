using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Tank.Financing.Web.Public.Pages;

public class IndexModel : FinancingPublicPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
