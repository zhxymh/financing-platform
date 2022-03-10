using Tank.Financing;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tank.Financing.Applies
{
    public class ApplyCreateDto
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
        public ApplyStatus ApplyStatus { get; set; } = ((ApplyStatus[])Enum.GetValues(typeof(ApplyStatus)))[0];
        public GuaranteeMethod GuaranteeMethod { get; set; } = ((GuaranteeMethod[])Enum.GetValues(typeof(GuaranteeMethod)))[0];
        [Required]
        public long ApplyTime { get; set; }
        public long PassedTime { get; set; }
    }
}