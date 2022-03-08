namespace Tank.Financing.Applies
{
    public static class ApplyConsts
    {
        private const string DefaultSorting = "{0}EnterpriceName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Apply." : string.Empty);
        }

    }
}