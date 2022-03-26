using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tank.Financing.EnterpriseDetails;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas.Host;
using Volo.FileManagement;

namespace Tank.Financing;

[DependsOn(
    typeof(FinancingDomainModule),
    typeof(FinancingApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(SaasHostApplicationModule),
    typeof(AbpAuditLoggingApplicationModule),
    typeof(AbpIdentityServerApplicationModule),
    typeof(AbpAccountPublicApplicationModule),
    typeof(AbpAccountAdminApplicationModule),
    typeof(LanguageManagementApplicationModule),
    typeof(LeptonThemeManagementApplicationModule),
    typeof(TextTemplateManagementApplicationModule)
)]
[DependsOn(typeof(AbpAccountSharedApplicationModule))]
[DependsOn(typeof(FileManagementApplicationModule))]
    public class FinancingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<FinancingApplicationModule>(); });

        Configure<SmsOptions>(context.Services.GetConfiguration().GetSection("Sms"));
        Configure<BlockchainOptions>(context.Services.GetConfiguration().GetSection("Blockchain"));
        Configure<EnterpriseDetailExtraInfoOptions>(context.Services.GetConfiguration().GetSection("EnterpriseDetailExtraInfo"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }
}