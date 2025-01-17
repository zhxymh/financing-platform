﻿using Volo.Abp.Identity;

namespace Tank.Financing;

public static class FinancingConsts
{
    public const string DbTablePrefix = "App";
    public const string DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;
    public const string ScopeIdForEnterprise = "Enterprise";
    public const string ScopeIdForFinancingOrganization = "FinancingOrganization";
    public const string ScopeIdForAdmin = "Admin";
}
