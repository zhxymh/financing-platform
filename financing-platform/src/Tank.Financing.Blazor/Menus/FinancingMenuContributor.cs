using Tank.Financing.Permissions;
using System.Threading.Tasks;
using Tank.Financing.Localization;
using Volo.Abp.AuditLogging.Blazor.Menus;
using Volo.Abp.Identity.Pro.Blazor.Navigation;
using Volo.Abp.IdentityServer.Blazor.Navigation;
using Volo.Abp.LanguageManagement.Blazor.Menus;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TextTemplateManagement.Blazor.Menus;
using Volo.Abp.UI.Navigation;
using Volo.CmsKit.Pro.Admin.Web.Menus;
using Volo.Saas.Host.Blazor.Navigation;

namespace Tank.Financing.Blazor.Menus;

public class FinancingMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<FinancingResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                FinancingMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 1
            )
        );

        context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                FinancingMenus.FinancialProducts,
                l["Menu:FinancialProducts"],
                url: "/financial-products",
                order: 2,
                requiredPermissionName: FinancingPermissions.FinancialProducts.Default)
        );

        context.Menu.SetSubItemOrder(SaasHostMenus.GroupName, 2);
        //CMS
        context.Menu.SetSubItemOrder(CmsKitProAdminMenus.GroupName, 3);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 4;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityProMenus.GroupName, 1);

        //Administration->Identity Server
        administration.SetSubItemOrder(AbpIdentityServerMenuNames.GroupName, 2);

        //Administration->Language Management
        administration.SetSubItemOrder(LanguageManagementMenus.GroupName, 3);

        //Administration->Text Template Management
        administration.SetSubItemOrder(TextTemplateManagementMenus.GroupName, 4);

        //Administration->Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMenus.GroupName, 5);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 6);

        return Task.CompletedTask;
    }
}