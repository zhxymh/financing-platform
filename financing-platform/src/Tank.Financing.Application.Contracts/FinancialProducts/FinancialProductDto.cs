using Tank.Financing;
using System;
using Volo.Abp.Application.Dtos;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductDto : EntityDto<Guid>
    {
        public TJDistrict AvailableDistricts { get; set; }
        public int TimeLimit { get; set; }
        public GuaranteeMethod GuaranteeMethod { get; set; }
        public decimal CreditCeiling { get; set; }
        public string Organization { get; set; }
        public int? AppliedNumber { get; set; }
        public decimal APR { get; set; }
        public int? Rating { get; set; }
        public string Name { get; set; }
        public Guid? FinancialProductId { get; set; }
    }
}