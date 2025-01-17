﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace Volo.Abp.Account.Public.Web.Pages.Account;

[Authorize]
[IgnoreAntiforgeryToken]
public class ImpersonateUserModel : AccountPageModel
{
    [BindProperty(SupportsGet = true)]
    [Required]
    public Guid UserId { get; set; }

    protected AbpAccountOptions AccountOptions { get; }
    protected IPermissionChecker PermissionChecker { get; }
    protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }

    public ImpersonateUserModel(
        IOptions<AbpAccountOptions> accountOptions,
        IPermissionChecker permissionChecker,
        ICurrentPrincipalAccessor currentPrincipalAccessor)
    {
        AccountOptions = accountOptions.Value;
        PermissionChecker = permissionChecker;
        CurrentPrincipalAccessor = currentPrincipalAccessor;
    }

    public virtual Task<IActionResult> OnGetAsync()
    {
        return Task.FromResult<IActionResult>(NotFound());
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (CurrentUser.FindImpersonatorUserId() != null)
        {
            throw new BusinessException("Volo.Account:NestedImpersonationIsNotAllowed");
        }

        if (UserId == CurrentUser.Id)
        {
            throw new BusinessException("Volo.Account:YouCanNotImpersonateYourself");
        }

        if (AccountOptions.ImpersonationUserPermission.IsNullOrWhiteSpace() ||
            !await PermissionChecker.IsGrantedAsync(AccountOptions.ImpersonationUserPermission))
        {
            throw new BusinessException("Volo.Account:RequirePermissionToImpersonateUser")
                .WithData("PermissionName", AccountOptions.ImpersonationUserPermission);
        }

        var user = await UserManager.FindByIdAsync(UserId.ToString());
        if (user != null)
        {
            var isPersistent = (await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme))?.Properties?.IsPersistent ?? false;
            await SignInManager.SignOutAsync();

            var additionalClaims = new List<Claim>();
            if (CurrentUser.Id?.ToString() != CurrentUser.FindClaim(AbpClaimTypes.ImpersonatorUserId)?.Value)
            {
                additionalClaims.Add(new Claim(AbpClaimTypes.ImpersonatorUserId, CurrentUser.Id.ToString()));
                additionalClaims.Add(new Claim(AbpClaimTypes.ImpersonatorUserName, CurrentUser.UserName));
                if (CurrentTenant.IsAvailable)
                {
                    additionalClaims.Add(new Claim(AbpClaimTypes.ImpersonatorTenantId, CurrentTenant.Id.ToString()));
                    additionalClaims.Add(new Claim(AbpClaimTypes.ImpersonatorTenantName, CurrentTenant.Name));
                }
            }

            await SignInManager.SignInWithClaimsAsync(user, new AuthenticationProperties
            {
                IsPersistent = isPersistent
            }, additionalClaims);

            //save security log to admin user.
            var userPrincipal = await SignInManager.CreateUserPrincipalAsync(user);
            userPrincipal.Identities.First().AddClaims(additionalClaims);
            using (CurrentPrincipalAccessor.Change(userPrincipal))
            {
                await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
                {
                    Identity = IdentitySecurityLogIdentityConsts.Identity,
                    Action = "ImpersonateUser"
                });
            }

            return Redirect("~/");
        }

        throw new BusinessException("Volo.Account:ThereIsNoUserWithUsernameInTheTenant")
            .WithData("UserId", UserId);
    }
}
