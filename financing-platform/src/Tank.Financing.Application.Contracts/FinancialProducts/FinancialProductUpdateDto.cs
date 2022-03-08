using Tank.Financing;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductUpdateDto
    {
        [Required]
        [Range(FinancialProductConsts.TimeLimitMinLength, FinancialProductConsts.TimeLimitMaxLength)]
        public int TimeLimit { get; set; }
        [Required]
        public GuaranteeMethod GuaranteeMethod { get; set; }
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