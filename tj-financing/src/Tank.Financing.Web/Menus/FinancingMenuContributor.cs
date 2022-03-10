using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Tank.Financing.Localization;
using Tank.Financing.Permissions;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.IdentityServer.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;

namespace Tank.Financing.Web.Menus;

public class FinancingMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<FinancingResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                FinancingMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //HostDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                FinancingMenus.HostDashboard,
                l["Menu:Dashboard"],
                "~/HostDashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(FinancingPermissions.Dashboard.Host)
        );

        //TenantDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                FinancingMenus.TenantDashboard,
                l["Menu:Dashboard"],
                "~/Dashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(FinancingPermissions.Dashboard.Tenant)
        );

        context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 3);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration->Identity Server
        administration.SetSubItemOrder(AbpIdentityServerMenuNames.GroupName, 2);

        //Administration->Language Management
        administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 3);

        //Administration->Text Template Management
        administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 4);

        //Administration->Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 5);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 6);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                FinancingMenus.Enterprises,
                l["Menu:Enterprises"],
                url: "/Enterprises",
                icon: "fa fa-file-alt",
                requiredPermissionName: FinancingPermissions.Enterprises.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                FinancingMenus.FinancialProducts,
                l["Menu:FinancialProducts"],
                url: "/FinancialProducts",
                icon: "fa fa-file-alt",
                requiredPermissionName: FinancingPermissions.FinancialProducts.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                FinancingMenus.Applies,
                l["Menu:Applies"],
                url: "/Applies",
                icon: "fa fa-file-alt",
                requiredPermissionName: FinancingPermissions.Applies.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                FinancingMenus.EnterpriseDetails,
                l["Menu:EnterpriseDetails"],
                url: "/EnterpriseDetails",
                icon: "fa fa-file-alt",
                requiredPermissionName: FinancingPermissions.EnterpriseDetails.Default)
        );
        return Task.CompletedTask;
    }
}