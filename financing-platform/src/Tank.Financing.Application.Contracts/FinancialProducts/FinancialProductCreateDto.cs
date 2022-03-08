using Tank.Financing;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductCreateDto
    {
        [Required]
        [Range(FinancialProductConsts.PeriodMinLength, FinancialProductConsts.PeriodMaxLength)]
        public int Period { get; set; }
        [Required]
        public GuaranteeMethod GuaranteeMethod { get; set; } = ((GuaranteeMethod[])Enum.GetValues(typeof(GuaranteeMethod)))[0];
        [Required]
        public string CreditCeiling { get; set; }
        [Required]
        [StringLength(FinancialProductConsts.OrganizationMaxLength, MinimumLength = FinancialProductConsts.OrganizationMinLength)]
        public string Organization { get; set; }
        public int? AppliedNumber { get; set; }
        [Required]
        public string APR { get; set; }
        public string Rating { get; set; }
        [Required]
        public string Name { get; set; }
    }
}