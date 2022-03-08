namespace Tank.Financing.Enterprises
{
    public static class EnterpriseConsts
    {
        private const string DefaultSorting = "{0}EnterpriseName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Enterprise." : string.Empty);
        }

    }
}