﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.Localization;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Volo.Abp.Account.Web.ExtensionGrantValidators;

public class LinkLoginExtensionGrantValidator : IExtensionGrantValidator
{
    public const string ExtensionGrantType = "LinkLogin";

    public string GrantType => ExtensionGrantType;

    protected ITokenValidator TokenValidator { get; }
    protected IdentityLinkUserManager IdentityLinkUserManager { get; }
    protected ICurrentTenant CurrentTenant { get; }
    protected ICurrentUser CurrentUser { get; }
    protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }
    protected IdentityUserManager UserManager { get; }
    protected IdentitySecurityLogManager IdentitySecurityLogManager { get; }
    protected ILogger<LinkLoginExtensionGrantValidator> Logger { get; }
    protected IStringLocalizer<AccountResource> Localizer { get; }
    protected IStringLocalizer<AbpIdentityServerResource> IdentityServerLocalizer { get; }
    protected AbpAccountIdentityServerOptions AccountIdentityServerOptions { get; }
    protected ITenantStore TenantStore { get; }
    protected IHttpContextAccessor HttpContextAccessor { get; }

    public LinkLoginExtensionGrantValidator(
        ITokenValidator tokenValidator,
        IdentityLinkUserManager identityLinkUserManager,
        ICurrentTenant currentTenant,
        ICurrentUser currentUser,
        IdentityUserManager userManager,
        ICurrentPrincipalAccessor currentPrincipalAccessor,
        IdentitySecurityLogManager identitySecurityLogManager,
        ILogger<LinkLoginExtensionGrantValidator> logger,
        IStringLocalizer<AccountResource> localizer,
        IStringLocalizer<AbpIdentityServerResource> identityServerLocalizer,
        IOptions<AbpAccountIdentityServerOptions> abpAccountIdentityServerOptions,
        ITenantStore tenantStore, IHttpContextAccessor httpContextAccessor)
    {
        TokenValidator = tokenValidator;
        IdentityLinkUserManager = identityLinkUserManager;
        CurrentTenant = currentTenant;
        CurrentUser = currentUser;
        UserManager = userManager;
        CurrentPrincipalAccessor = currentPrincipalAccessor;
        IdentitySecurityLogManager = identitySecurityLogManager;
        Logger = logger;
        Localizer = localizer;
        IdentityServerLocalizer = identityServerLocalizer;
        AccountIdentityServerOptions = abpAccountIdentityServerOptions.Value;
        TenantStore = tenantStore;
        HttpContextAccessor = httpContextAccessor;
    }

    public virtual async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var accessToken = context.Request.Raw["access_token"];
        if (accessToken.IsNullOrWhiteSpace())
        {
            context.Result = new GrantValidationResult
            {
                IsError = true,
                Error = "invalid_access_token"
            };
            return;
        }

        var result = await TokenValidator.ValidateAccessTokenAsync(accessToken);
        if (result.IsError)
        {
            context.Result = new GrantValidationResult
            {
                IsError = true,
                Error = result.Error,
                ErrorDescription = result.ErrorDescription
            };
            return;
        }

        using (CurrentPrincipalAccessor.Change(result.Claims))
        {
            if (!Guid.TryParse(context.Request.Raw["LinkUserId"], out var linkUserId))
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = "invalid_link_user_id"
                };
                return;
            }

            Guid? linkTenantId = null;
            if (!context.Request.Raw["LinkTenantId"].IsNullOrWhiteSpace())
            {
                if (!Guid.TryParse(context.Request.Raw["LinkTenantId"], out var parsedGuid))
                {
                    context.Result = new GrantValidationResult
                    {
                        IsError = true,
                        Error = "invalid_link_tenant_id"
                    };
                    return;
                }

                linkTenantId = parsedGuid;
            }

            var isLinked = await IdentityLinkUserManager.IsLinkedAsync(
                new IdentityLinkUserInfo(CurrentUser.GetId(), CurrentTenant.Id),
                new IdentityLinkUserInfo(linkUserId, linkTenantId),
                true);

            if (isLinked)
            {
                using (CurrentTenant.Change(linkTenantId))
                {
                    var user = await UserManager.GetByIdAsync(linkUserId);
                    var sub = await UserManager.GetUserIdAsync(user);

                    var additionalClaims = new List<Claim>();
                    await AddCustomClaimsAsync(additionalClaims, user, context);

                    context.Result = new GrantValidationResult(
                        sub,
                        GrantType,
                        additionalClaims.ToArray()
                    );

                    if (AccountIdentityServerOptions.IsTenantMultiDomain)
                    {
                        var tenantInfo = new BasicTenantInfo(null, null);
                        if (linkTenantId != null)
                        {
                            var tenantConfiguration = await TenantStore.FindAsync(linkTenantId.Value);
                            tenantInfo = new BasicTenantInfo(tenantConfiguration.Id, tenantConfiguration.Name);
                        }

                        var tenantDomain = await AccountIdentityServerOptions.GetTenantDomain(HttpContextAccessor.HttpContext, context, tenantInfo);
                        context.Result.CustomResponse = new Dictionary<string, object>()
                            {
                                { "tenant_domain", tenantDomain }
                            };
                    }
                }
            }
            else
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = IdentityServerLocalizer["TheTargetUserIsNotLinkedToYou"]
                };
            }
        }
    }

    protected virtual Task AddCustomClaimsAsync(List<Claim> customClaims, IdentityUser user, ExtensionGrantValidationContext context)
    {
        if (user.TenantId.HasValue)
        {
            customClaims.Add(new Claim(AbpClaimTypes.TenantId, user.TenantId?.ToString()));
        }

        return Task.CompletedTask;
    }
}
