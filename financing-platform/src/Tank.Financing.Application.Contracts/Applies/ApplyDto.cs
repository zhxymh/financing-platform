using Tank.Financing;
using System;
using Volo.Abp.Application.Dtos;

namespace Tank.Financing.Applies
{
    public class ApplyDto : FullAuditedEntityDto<Guid>
    {
        public string EnterpriseName { get; set; }
        public string Organization { get; set; }
        public string ProductName { get; set; }
        public string Allowance { get; set; }
        public string APY { get; set; }
        public string Period { get; set; }
        public ApplyStatus ApplyStatus { get; set; }
        public GuaranteeMethod GuaranteeMethod { get; set; }
        public string ApplyTime { get; set; }
        public string PassedTime { get; set; }
    }
}