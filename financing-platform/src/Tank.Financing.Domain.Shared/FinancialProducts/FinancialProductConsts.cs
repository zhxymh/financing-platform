namespace Tank.Financing.FinancialProducts
{
    public static class FinancialProductConsts
    {
        private const string DefaultSorting = "{0}AvailableDistricts asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "FinancialProduct." : string.Empty);
        }

        public const int TimeLimitMinLength = 3;
        public const int TimeLimitMaxLength = 3600;
        public const int OrganizationMinLength = 1;
        public const int OrganizationMaxLength = 100;
    }
}