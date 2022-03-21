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

        var enterprisePermission = myGroup.AddPermission(FinancingPermissions.Enterprises.Default, L("Permission:Enterprises"));
        enterprisePermission.AddChild(FinancingPermissions.Enterprises.Create, L("Permission:Create"));
        enterprisePermission.AddChild(FinancingPermissions.Enterprises.Edit, L("Permission:Edit"));
        enterprisePermission.AddChild(FinancingPermissions.Enterprises.Delete, L("Permission:Delete"));

        var financialProductPermission = myGroup.AddPermission(FinancingPermissions.FinancialProducts.Default, L("Permission:FinancialProducts"));
        financialProductPermission.AddChild(FinancingPermissions.FinancialProducts.Create, L("Permission:Create"));
        financialProductPermission.AddChild(FinancingPermissions.FinancialProducts.Edit, L("Permission:Edit"));
        financialProductPermission.AddChild(FinancingPermissions.FinancialProducts.Delete, L("Permission:Delete"));

        var applyPermission = myGroup.AddPermission(FinancingPermissions.Applies.Default, L("Permission:Applies"));
        applyPermission.AddChild(FinancingPermissions.Applies.Create, L("Permission:Create"));
        applyPermission.AddChild(FinancingPermissions.Applies.Edit, L("Permission:Edit"));
        applyPermission.AddChild(FinancingPermissions.Applies.Delete, L("Permission:Delete"));

        var enterpriseDetailPermission = myGroup.AddPermission(FinancingPermissions.EnterpriseDetails.Default, L("Permission:EnterpriseDetails"));
        enterpriseDetailPermission.AddChild(FinancingPermissions.EnterpriseDetails.Create, L("Permission:Create"));
        enterpriseDetailPermission.AddChild(FinancingPermissions.EnterpriseDetails.Edit, L("Permission:Edit"));
        enterpriseDetailPermission.AddChild(FinancingPermissions.EnterpriseDetails.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FinancingResource>(name);
    }
}