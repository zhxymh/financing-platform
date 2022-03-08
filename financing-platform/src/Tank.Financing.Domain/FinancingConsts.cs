using Volo.Abp.Identity;

namespace Tank.Financing;

public static class FinancingConsts
{
    public const string DbTablePrefix = "App";
    public const string DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;
    public const string DefaultNodeUrl = "http://localhost:1235/";
    public const string DefaultSenderAddress = "4zT74bCjganXgwFhcnW8DNLVt3Lebq2speF362oQoAqR4S7WX";
    public const string DefaultDelegatorContractAddress = "2WHXRoLRjbUTDQsuqR5CntygVfnDb125qdJkudev4kVNbLhTdG";
    public const string DefaultFinancingContractAddress = "2LUmicHyH4RXrMjG4beDwuDsiWJESyLkgkwPdGTR8kahRzq5XS";
    public const string DefaultSenderPassword = "123456789";
    public const string ScopeIdForEnterprise = "Enterprise";
    public const string ScopeIdForFinancingOrganization = "FinancingOrganization";
    public const string ScopeIdForAdmin = "Admin";
}