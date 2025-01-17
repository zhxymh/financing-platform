﻿using System.Security.Principal;
using System.Threading.Tasks;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account.Localization;
using Volo.Abp.Security.Claims;
using Volo.Abp.UI.Navigation;

namespace Volo.Abp.Account.Public.Web;

public class AbpAccountUserMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.User)
        {
            return Task.CompletedTask;
        }

        var uiResource = context.GetLocalizer<AbpUiResource>();
        var accountResource = context.GetLocalizer<AccountResource>();

        context.Menu.AddItem(new ApplicationMenuItem("Account.LinkedAccounts", accountResource["LinkedAccounts"], url: "javascript:void(0)", icon: "fa fa-link", cssClass: "d-none"));
        context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountResource["MyAccount"], url: "~/Account/Manage", icon: "fa fa-cog"));
        context.Menu.AddItem(new ApplicationMenuItem("Account.SecurityLogs", accountResource["MySecurityLogs"], url: "~/Account/SecurityLogs", icon: "fa fa-cog"));
        context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", uiResource["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000));

        return Task.CompletedTask;
    }
}
