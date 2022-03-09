using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Tank.Financing.Pages;

public class Index_Tests : FinancingWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
