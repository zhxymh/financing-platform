using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Owl.reCAPTCHA;
using Volo.Abp.Account.ExternalProviders;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Public.Web.Pages.Account;
using Volo.Abp.Account.Public.Web.Security.Recaptcha;
using Volo.Abp.Account.Security.Recaptcha;
using Volo.Abp.Account.Settings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace Volo.Abp.Account.Web.Pages.Account;

[ExposeServices(typeof(LoginModel))]
public class IdentityServerSupportedLoginModel : LoginModel
{
    protected IIdentityServerInteractionService Interaction { get; }
    protected IClientStore ClientStore { get; }
    protected IEventService IdentityServerEvents { get; }

    public ILogger<IdentityServerSupportedLoginModel> Logger { get; set; }

    public IdentityServerSupportedLoginModel(
        IAuthenticationSchemeProvider schemeProvider,
        IOptions<AbpAccountOptions> accountOptions,
        IAccountExternalProviderAppService accountExternalProviderAppService,
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        IEventService identityServerEvents,
        ICurrentPrincipalAccessor currentPrincipalAccessor,
        IAbpRecaptchaValidatorFactory recaptchaValidatorFactory,
        IOptions<IdentityOptions> identityOptions,
        IOptionsSnapshot<reCAPTCHAOptions> reCaptchaOptions)
        : base(
            schemeProvider,
            accountOptions,
            recaptchaValidatorFactory,
            accountExternalProviderAppService,
            currentPrincipalAccessor,
            identityOptions,
            reCaptchaOptions)
    {
        Interaction = interaction;
        ClientStore = clientStore;
        IdentityServerEvents = identityServerEvents;
        Logger = NullLogger<IdentityServerSupportedLoginModel>.Instance;
    }

    public override async Task<IActionResult> OnGetAsync()
    {
        LoginInput = new LoginInputModel();

        var context = await Interaction.GetAuthorizationContextAsync(ReturnUrl);

        if (context != null)
        {
            ShowCancelButton = true;

            LoginInput.UserNameOrEmailAddress = context.LoginHint;

            //TODO: Reference AspNetCore MultiTenancy module and use options to get the tenant key!
            var tenant = context.Parameters[TenantResolverConsts.DefaultTenantKey];
            if (!string.IsNullOrEmpty(tenant))
            {
                CurrentTenant.Change(Guid.Parse(tenant));
                Response.Cookies.Append(TenantResolverConsts.DefaultTenantKey, tenant, new CookieOptions
                {
                    SameSite = SameSiteMode.None,
                    Secure = true
                });
            }
        }

        if (context?.IdP != null)
        {
            LoginInput.UserNameOrEmailAddress = context.LoginHint;
            ExternalProviders = new[] { new ExternalProviderModel { AuthenticationScheme = context.IdP } };
            return Page();
        }

        var providers = await GetExternalProviders();
        ExternalProviders = providers.ToList();

        EnableLocalLogin = await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin);

        IsSelfRegistrationEnabled = await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled);
        if (context?.Client.ClientId != null)
        {
            var client = await ClientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
            if (client != null)
            {
                EnableLocalLogin = client.EnableLocalLogin;

                if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                {
                    providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                }
            }
        }

        UseCaptcha = await UseCaptchaOnLoginAsync();

        ExternalProviders = providers.ToArray();

        if (IsExternalLoginOnly)
        {
            //return await ExternalLogin(vm.ExternalLoginScheme, returnUrl);
            throw new NotImplementedException();
        }

        IsLinkLogin = await VerifyLinkTokenAsync();
        if (IsLinkLogin)
        {
            if (CurrentUser.IsAuthenticated)
            {
                await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
                {
                    Identity = IdentitySecurityLogIdentityConsts.Identity,
                    Action = IdentitySecurityLogActionConsts.Logout
                });

                await SignInManager.SignOutAsync();

                return Redirect(HttpContext.Request.GetDisplayUrl());
            }
        }

        return Page();
    }

    [UnitOfWork] //TODO: Will be removed when we implement action filter
    public override async Task<IActionResult> OnPostAsync(string action)
    {
        Logger.LogInformation($"[Login]{action}");
        ExternalProviders = await GetExternalProviders();
        EnableLocalLogin = await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin);
        IsSelfRegistrationEnabled = await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled);

        var context = await Interaction.GetAuthorizationContextAsync(ReturnUrl);
        if (action == "Cancel")
        {
            if (context == null)
            {
                return Redirect("~/");
            }

            await Interaction.GrantConsentAsync(context, new ConsentResponse()
            {
                Error = AuthorizationError.AccessDenied
            });

            return Redirect(ReturnUrl);
        }

        try
        {
            await ReCaptchaVerification();
        }
        catch (UserFriendlyException e)
        {
            if (e is ScoreBelowThresholdException)
            {
                var onScoreBelowThresholdResult = OnRecaptchaScoreBelowThreshold();
                if (onScoreBelowThresholdResult != null)
                {
                    return await onScoreBelowThresholdResult;
                }
            }

            Alerts.Danger(GetLocalizeExceptionMessage(e));
            return Page();
        }

        ValidateModel();

        await IdentityOptions.SetAsync();

        await ReplaceEmailToUsernameOfInputIfNeeds();

        IsLinkLogin = await VerifyLinkTokenAsync();
        Logger.LogInformation($"[Login]{IsLinkLogin}");

        var result = await SignInManager.PasswordSignInAsync(
            LoginInput.UserNameOrEmailAddress,
            LoginInput.Password,
            LoginInput.RememberMe,
            true
        );

        Logger.LogInformation($"[Login]{result}");

        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
        {
            Identity = IdentitySecurityLogIdentityConsts.Identity,
            Action = result.ToIdentitySecurityLogAction(),
            UserName = LoginInput.UserNameOrEmailAddress,
            ClientId = context?.Client?.ClientId
        });

        if (result.RequiresTwoFactor)
        {
            return RedirectToPage("./SendSecurityCode", new {
                returnUrl = ReturnUrl,
                returnUrlHash = ReturnUrlHash,
                rememberMe = LoginInput.RememberMe,
                linkUserId = LinkUserId,
                linkTenantId = LinkTenantId,
                linkToken = LinkToken
            });
        }

        if (result.IsLockedOut)
        {
            return RedirectToPage("./LockedOut", new {
                returnUrl = ReturnUrl,
                returnUrlHash = ReturnUrlHash
            });
        }

        if (result.IsNotAllowed)
        {
            var notAllowedUser = await GetIdentityUser(LoginInput.UserNameOrEmailAddress);
            if (notAllowedUser.IsActive && await UserManager.CheckPasswordAsync(notAllowedUser, LoginInput.Password))
            {
                await StoreConfirmUser(notAllowedUser);
                return RedirectToPage("./ConfirmUser", new {
                    returnUrl = ReturnUrl,
                    returnUrlHash = ReturnUrlHash
                });
            }

            Alerts.Danger(L["LoginIsNotAllowed"]);
            return Page();
        }

        if (!result.Succeeded)
        {
            Alerts.Danger(L["InvalidUserNameOrPassword"]);
            return Page();
        }

        var user = await GetIdentityUser(LoginInput.UserNameOrEmailAddress);

        Logger.LogInformation($"[Login]{user.Name}");

        await IdentityServerEvents.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName)); //TODO: Use user's name once implemented

        if (IsLinkLogin)
        {
            using (CurrentPrincipalAccessor.Change(await SignInManager.CreateUserPrincipalAsync(user)))
            {
                await IdentityLinkUserAppService.LinkAsync(new LinkUserInput
                {
                    UserId = LinkUserId.Value,
                    TenantId = LinkTenantId,
                    Token = LinkToken
                });

                await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
                {
                    Identity = IdentitySecurityLogIdentityConsts.Identity,
                    Action = IdentityProSecurityLogActionConsts.LinkUser,
                    UserName = user.UserName,
                    ClientId = context?.Client?.ClientId,
                    ExtraProperties =
                        {
                            { IdentityProSecurityLogActionConsts.LinkTargetTenantId, LinkTenantId },
                            { IdentityProSecurityLogActionConsts.LinkTargetUserId, LinkUserId }
                        }
                });

                using (CurrentTenant.Change(LinkTenantId))
                {
                    var targetUser = await UserManager.GetByIdAsync(LinkUserId.Value);
                    Logger.LogInformation($"[Login]{targetUser}");
                    using (CurrentPrincipalAccessor.Change(await SignInManager.CreateUserPrincipalAsync(targetUser)))
                    {
                        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
                        {
                            Identity = IdentitySecurityLogIdentityConsts.Identity,
                            Action = IdentityProSecurityLogActionConsts.LinkUser,
                            UserName = targetUser.UserName,
                            ClientId = context?.Client?.ClientId,
                            ExtraProperties =
                                {
                                    { IdentityProSecurityLogActionConsts.LinkTargetTenantId, targetUser.TenantId },
                                    { IdentityProSecurityLogActionConsts.LinkTargetUserId, targetUser.Id }
                                }
                        });
                    }
                }

                return RedirectToPage("./LinkLogged", new {
                    returnUrl = ReturnUrl,
                    returnUrlHash = ReturnUrlHash,
                    TargetLinkUserId = LinkUserId,
                    TargetLinkTenantId = LinkTenantId
                });
            }
        }

        Logger.LogInformation($"[Login]RedirectSafely");

        return RedirectSafely(ReturnUrl, ReturnUrlHash);
    }

    [UnitOfWork]
    public override async Task<IActionResult> OnPostExternalLogin(string provider)
    {
        if (AccountOptions.WindowsAuthenticationSchemeName == provider)
        {
            return await ProcessWindowsLoginAsync();
        }

        return await base.OnPostExternalLogin(provider);
    }

    protected virtual async Task<IActionResult> ProcessWindowsLoginAsync()
    {
        var result = await HttpContext.AuthenticateAsync(AccountOptions.WindowsAuthenticationSchemeName);
        if (result.Succeeded)
        {
            var props = new AuthenticationProperties()
            {
                RedirectUri = Url.Page("./Login", pageHandler: "ExternalLoginCallback", values: new { ReturnUrl, ReturnUrlHash }),
                Items =
                    {
                        {
                            "LoginProvider", AccountOptions.WindowsAuthenticationSchemeName
                        },
                    }
            };

            var id = new ClaimsIdentity(AccountOptions.WindowsAuthenticationSchemeName);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Principal.FindFirstValue(ClaimTypes.PrimarySid)));
            id.AddClaim(new Claim(ClaimTypes.Name, result.Principal.FindFirstValue(ClaimTypes.Name)));

            await HttpContext.SignInAsync(IdentityConstants.ExternalScheme, new ClaimsPrincipal(id), props);

            return Redirect(props.RedirectUri);
        }

        return Challenge(AccountOptions.WindowsAuthenticationSchemeName);
    }
}
