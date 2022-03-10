using Tank.Financing.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Tank.Financing.Permissions;

public class FinancingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FinancingPermissions.GroupName);

        myGroup.AddPermission(FinancingPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(FinancingPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(FinancingPermissions.MyPermission1, L("Permission:MyPermission1"));

        var enterprisePermission = myGroup.AddPermission(FinancingPermissions.Enterprises.Default, L("Permission:Enterprises"));
        enterprisePermission.AddChild(FinancingPermissions.Enterprises.Create, L("Permission:Create"));
        enterprisePermission.AddChild(FinancingPermissions.Enterprises.Edit, L("Permission:Edit"));
        enterprisePermission.AddChild(FinancingPermissions.Enterprises.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FinancingResource>(name);
    }
}