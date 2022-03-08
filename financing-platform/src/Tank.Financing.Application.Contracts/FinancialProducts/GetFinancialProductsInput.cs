using Tank.Financing;
using Volo.Abp.Application.Dtos;
using System;

namespace Tank.Financing.FinancialProducts
{
    public class GetFinancialProductsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? PeriodMin { get; set; }
        public int? PeriodMax { get; set; }
        public GuaranteeMethod? GuaranteeMethod { get; set; }
        public string CreditCeiling { get; set; }
        public string Organization { get; set; }
        public int? AppliedNumberMin { get; set; }
        public int? AppliedNumberMax { get; set; }
        public string APR { get; set; }
        public string Rating { get; set; }
        public string Name { get; set; }

        public GetFinancialProductsInput()
        {

        }
    }
}