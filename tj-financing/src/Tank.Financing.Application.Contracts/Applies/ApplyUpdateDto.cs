using Tank.Financing;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tank.Financing.Applies
{
    public class ApplyUpdateDto
    {
        [Required]
        public string EnterpriseName { get; set; }
        [Required]
        public string Organization { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Allowance { get; set; }
        public string APR { get; set; }
        [Required]
        public string Period { get; set; }
        [Required]
        public ApplyStatus ApplyStatus { get; set; }
        public GuaranteeMethod GuaranteeMethod { get; set; }
        [Required]
        public long ApplyTime { get; set; }
        public long PassedTime { get; set; }
        public string ApplyTxId { get; set; }
        public string OnlineApproveTxId { get; set; }
        public string OfflineApproveTxId { get; set; }
        public string ApproveAllowanceTxId { get; set; }
        public string SetAllowanceTxId { get; set; }
    }
}