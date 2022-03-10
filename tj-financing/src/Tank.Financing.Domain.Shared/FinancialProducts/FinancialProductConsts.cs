namespace Tank.Financing.FinancialProducts
{
    public static class FinancialProductConsts
    {
        private const string DefaultSorting = "{0}ProductName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "FinancialProduct." : string.Empty);
        }

    }
}