using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
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
    typeof(FinancingDomainSharedModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(SaasHostApplicationContractsModule),
    typeof(AbpAuditLoggingApplicationContractsModule),
    typeof(AbpIdentityServerApplicationContractsModule),
    typeof(AbpAccountPublicApplicationContractsModule),
    typeof(AbpAccountAdminApplicationContractsModule),
    typeof(LanguageManagementApplicationContractsModule),
    typeof(LeptonThemeManagementApplicationContractsModule),
    typeof(TextTemplateManagementApplicationContractsModule)
)]
[DependsOn(typeof(AbpAccountSharedApplicationContractsModule))]
    [DependsOn(typeof(FileManagementApplicationContractsModule))]
    public class FinancingApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FinancingDtoExtensions.Configure();
    }
}
