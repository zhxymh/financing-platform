using Tank.Financing;
using Volo.Abp.Application.Dtos;
using System;

namespace Tank.Financing.FinancialProducts
{
    public class GetFinancialProductsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public TJDistrict? AvailableDistricts { get; set; }
        public int? TimeLimitMin { get; set; }
        public int? TimeLimitMax { get; set; }
        public GuaranteeMethod? GuaranteeMethod { get; set; }
        public decimal? CreditCeilingMin { get; set; }
        public decimal? CreditCeilingMax { get; set; }
        public string Organization { get; set; }
        public int? AppliedNumberMin { get; set; }
        public int? AppliedNumberMax { get; set; }
        public decimal? APRMin { get; set; }
        public decimal? APRMax { get; set; }
        public int? RatingMin { get; set; }
        public int? RatingMax { get; set; }
        public string Name { get; set; }
        public Guid? FinancialProductId { get; set; }

        public GetFinancialProductsInput()
        {

        }
    }
}