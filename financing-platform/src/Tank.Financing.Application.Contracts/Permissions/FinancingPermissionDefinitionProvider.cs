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

        var financialProductPermission = myGroup.AddPermission(FinancingPermissions.FinancialProducts.Default, L("Permission:FinancialProducts"));
        financialProductPermission.AddChild(FinancingPermissions.FinancialProducts.Create, L("Permission:Create"));
        financialProductPermission.AddChild(FinancingPermissions.FinancialProducts.Edit, L("Permission:Edit"));
        financialProductPermission.AddChild(FinancingPermissions.FinancialProducts.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FinancingResource>(name);
    }
}