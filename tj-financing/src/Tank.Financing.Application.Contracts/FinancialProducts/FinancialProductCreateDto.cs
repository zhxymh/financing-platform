using Tank.Financing;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductCreateDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Organization { get; set; }
        public int? Period { get; set; }
        public GuaranteeMethod GuaranteeMethod { get; set; } = ((GuaranteeMethod[])Enum.GetValues(typeof(GuaranteeMethod)))[0];
        public int AppliedNumber { get; set; }
        public string APR { get; set; }
        public string Rating { get; set; }
        public long CreditCeiling { get; set; }
        public string AddFinancingProductTxId { get; set; }
    }
}