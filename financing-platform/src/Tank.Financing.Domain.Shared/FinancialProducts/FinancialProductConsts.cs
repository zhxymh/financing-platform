namespace Tank.Financing.FinancialProducts
{
    public static class FinancialProductConsts
    {
        private const string DefaultSorting = "{0}Name asc,{0}Organization asc,{0}Period asc,{0}GuaranteeMethod asc,{0}CreditCeiling asc,{0}APR asc,{0}AppliedNumber asc,{0}Rating asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "FinancialProduct." : string.Empty);
        }

        public const int PeriodMinLength = 1;
        public const int PeriodMaxLength = 3600;
        public const int OrganizationMinLength = 1;
        public const int OrganizationMaxLength = 100;
    }
}