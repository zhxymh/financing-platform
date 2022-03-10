using Tank.Financing;
using Volo.Abp.Application.Dtos;
using System;

namespace Tank.Financing.Applies
{
    public class GetAppliesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string EnterpriseName { get; set; }
        public string Organization { get; set; }
        public string ProductName { get; set; }
        public string Allowance { get; set; }
        public string APR { get; set; }
        public string Period { get; set; }
        public ApplyStatus? ApplyStatus { get; set; }
        public GuaranteeMethod? GuaranteeMethod { get; set; }
        public long? ApplyTimeMin { get; set; }
        public long? ApplyTimeMax { get; set; }
        public long? PassedTimeMin { get; set; }
        public long? PassedTimeMax { get; set; }
        public string ApplyTxId { get; set; }
        public string OnlineApproveTxId { get; set; }
        public string OfflineApproveTxId { get; set; }
        public string ApproveAllowanceTxId { get; set; }
        public string SetAllowanceTxId { get; set; }

        public GetAppliesInput()
        {

        }
    }
}