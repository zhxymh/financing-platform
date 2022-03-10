using Tank.Financing;
using System;
using Volo.Abp.Application.Dtos;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductDto : FullAuditedEntityDto<Guid>
    {
        public string ProductName { get; set; }
        public string Organization { get; set; }
        public int? Period { get; set; }
        public GuaranteeMethod GuaranteeMethod { get; set; }
        public int AppliedNumber { get; set; }
        public string APR { get; set; }
        public string Rating { get; set; }
        public long CreditCeiling { get; set; }
        public string AddFinancingProductTxId { get; set; }
    }
}