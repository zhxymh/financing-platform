﻿using System.Linq;
using System.Threading.Tasks;
using LdapForNet;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ldap;

namespace Volo.Abp.Account.Public.Web.Ldap;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(OpenLdapManager), typeof(ILdapManager), typeof(LdapManager))]
public class OpenLdapManager : LdapManager
{
    public OpenLdapManager(ILdapSettingProvider ldapSettingProvider)
        : base(ldapSettingProvider)
    {

    }

    public virtual async Task<string> GetUserEmailAsync(string userName)
    {
        using (var conn = await CreateLdapConnectionAsync())
        {
            await AuthenticateLdapConnectionAsync(conn, await NormalizeUserNameAsync(await LdapSettingProvider.GetUserNameAsync()), await LdapSettingProvider.GetPasswordAsync());

            var searchResults = await conn.SearchAsync(await GetBaseDnAsync(), await GetUserFilterAsync(userName));
            try
            {
                var userEntry = searchResults.First();
                return await GetUserEmailAsync(userEntry);
            }
            catch (LdapException e)
            {
                Logger.LogException(e);
            }

            return null;
        }
    }

    protected async override Task ConnectAsync(ILdapConnection ldapConnection)
    {
        ldapConnection.Connect(await LdapSettingProvider.GetServerHostAsync(), await LdapSettingProvider.GetServerPortAsync());
    }

    protected virtual async Task<string> NormalizeUserNameAsync(string userName)
    {
        return $"cn={userName},{await LdapSettingProvider.GetBaseDcAsync()}";
    }

    protected virtual Task<string> GetUserEmailAsync(LdapEntry ldapEntry)
    {
        return Task.FromResult(ldapEntry.ToDirectoryEntry().GetAttribute("mail")?.GetValue<string>());
    }

    protected virtual async Task<string> GetBaseDnAsync()
    {
        return await LdapSettingProvider.GetBaseDcAsync();
    }

    protected virtual Task<string> GetUserFilterAsync(string userName)
    {
        return Task.FromResult($"(&(uid={userName}))");
    }
}
