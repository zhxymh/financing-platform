using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tank.Financing;

public class FinancingWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<FinancingWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
