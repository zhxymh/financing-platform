namespace Tank.Financing.EnterpriseDetails
{
    public static class EnterpriseDetailConsts
    {
        private const string DefaultSorting = "{0}EnterpriseName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EnterpriseDetail." : string.Empty);
        }

    }
}