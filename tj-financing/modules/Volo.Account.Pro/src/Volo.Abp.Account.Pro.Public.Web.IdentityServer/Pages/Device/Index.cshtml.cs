﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Account.Public.Web.Pages.Account;

namespace Volo.Abp.Account.Web.Pages.Device;

[Authorize]
public class IndexModel : AccountPageModel
{
    [Required]
    [BindProperty]
    public string UserCode { get; set; }

    public ConsentInputModel ConsentInput { get; set; }

    public ClientInfoModel ClientInfo { get; set; }

    public bool ShowSuccess { get; set; }

    protected IDeviceFlowInteractionService Interaction { get; }
    protected IClientStore ClientStore { get; }
    protected IResourceStore ResourceStore { get; }
    protected IEventService Events { get; }


    public IndexModel(
        IDeviceFlowInteractionService interaction,
        IClientStore clientStore,
        IResourceStore resourceStore,
        IEventService events)
    {
        Interaction = interaction;
        ClientStore = clientStore;
        ResourceStore = resourceStore;
        Events = events;
    }

    public virtual Task<IActionResult> OnGetAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var request = await Interaction.GetAuthorizationContextAsync(UserCode);
        if (request == null)
        {
            throw new UserFriendlyException(L["UserCodeInvalid"]);
        }

        var client = await ClientStore.FindEnabledClientByIdAsync(request.Client.ClientId);
        if (client == null)
        {
            throw new UserFriendlyException($"Invalid client id: {request.Client.ClientId}");
        }

        var resources = await ResourceStore.FindEnabledResourcesByScopeAsync(request.ValidatedResources.RawScopeValues);
        if (resources == null || (!resources.IdentityResources.Any() && !resources.ApiResources.Any()))
        {
            throw new UserFriendlyException($"No scopes matching: {request.ValidatedResources.RawScopeValues.Aggregate((x, y) => x + ", " + y)}");
        }

        ClientInfo = new ClientInfoModel(client);
        ConsentInput = new ConsentInputModel
        {
            RememberConsent = true,
            IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, true)).ToList()
        };

        var apiScopes = new List<ScopeViewModel>();
        foreach (var parsedScope in request.ValidatedResources.ParsedScopes)
        {
            var apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName);
            if (apiScope != null)
            {
                var scopeVm = CreateScopeViewModel(parsedScope, apiScope, true);
                apiScopes.Add(scopeVm);
            }
        }

        if (resources.OfflineAccess)
        {
            apiScopes.Add(GetOfflineAccessScope(true));
        }

        ConsentInput.ApiScopes = apiScopes;

        return Page();
    }

    public virtual async Task<IActionResult> OnPostConfirmAsync()
    {
        ConsentInput = new ConsentInputModel();
        await TryUpdateModelAsync(ConsentInput, "ConsentInput");

        var request = await Interaction.GetAuthorizationContextAsync(UserCode);
        if (request == null)
        {
            throw new UserFriendlyException($"UserCode invalid");
        }

        if (ConsentInput.UserDecision == "no")
        {
            // emit event
            await Events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues));
            await Interaction.HandleRequestAsync(UserCode, new ConsentResponse
            {
                Error = AuthorizationError.AccessDenied
            });

            return Redirect("~/");
        }

        if (ConsentInput.IdentityScopes.IsNullOrEmpty() && ConsentInput.ApiScopes.IsNullOrEmpty())
        {
            throw new UserFriendlyException("You must pick at least one permission");
        }

        await Events.RaiseAsync(new ConsentGrantedEvent(
            User.GetSubjectId(),
            request.Client.ClientId,
            request.ValidatedResources.RawScopeValues,
            ConsentInput.GetAllowedScopeNames(), ConsentInput.RememberConsent));
        var deviceFlowInteractionResult = await Interaction.HandleRequestAsync(UserCode, new ConsentResponse
        {
            RememberConsent = ConsentInput.RememberConsent,
            ScopesValuesConsented = ConsentInput.GetAllowedScopeNames()
        });

        if (!deviceFlowInteractionResult.IsError)
        {
            ShowSuccess = true;
        }
        else
        {
            throw new UserFriendlyException(deviceFlowInteractionResult.ErrorDescription);
        }
        return Page();
    }

    protected virtual ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
    {
        return new ScopeViewModel
        {
            Name = identity.Name,
            DisplayName = identity.DisplayName,
            Description = identity.Description,
            Emphasize = identity.Emphasize,
            Required = identity.Required,
            Checked = check || identity.Required
        };
    }

    protected virtual ScopeViewModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check)
    {
        var displayName = apiScope.DisplayName ?? apiScope.Name;
        if (!string.IsNullOrWhiteSpace(parsedScopeValue.ParsedParameter))
        {
            displayName += ":" + parsedScopeValue.ParsedParameter;
        }

        return new ScopeViewModel
        {
            Name = parsedScopeValue.RawValue,
            DisplayName = displayName,
            Description = apiScope.Description,
            Emphasize = apiScope.Emphasize,
            Required = apiScope.Required,
            Checked = check || apiScope.Required
        };
    }

    protected virtual ScopeViewModel GetOfflineAccessScope(bool check)
    {
        return new ScopeViewModel
        {
            Name = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
            DisplayName = "Offline Access", //TODO: Localize
            Description = "Access to your applications and resources, even when you are offline",
            Emphasize = true,
            Checked = check
        };
    }

    public class ConsentInputModel
    {
        public List<ScopeViewModel> IdentityScopes { get; set; }

        public List<ScopeViewModel> ApiScopes { get; set; }

        public string UserDecision { get; set; }

        public bool RememberConsent { get; set; }

        public List<string> GetAllowedScopeNames()
        {
            var identityScopes = IdentityScopes ?? new List<ScopeViewModel>();
            var apiScopes = ApiScopes ?? new List<ScopeViewModel>();
            return identityScopes.Union(apiScopes).Where(s => s.Checked || s.Required).Select(s => s.Name).ToList();
        }
    }

    public class ScopeViewModel
    {
        [Required]
        [HiddenInput]
        public string Name { get; set; }

        public bool Checked { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Emphasize { get; set; }

        public bool Required { get; set; }
    }

    public class ClientInfoModel
    {
        public string ClientName { get; set; }

        public string ClientUrl { get; set; }

        public string ClientLogoUrl { get; set; }

        public bool AllowRememberConsent { get; set; }

        public ClientInfoModel(Client client)
        {
            //TODO: Automap
            ClientName = client.ClientId;
            ClientUrl = client.ClientUri;
            ClientLogoUrl = client.LogoUri;
            AllowRememberConsent = client.AllowRememberConsent;
        }
    }
}
