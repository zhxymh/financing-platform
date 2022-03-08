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
        public string APY { get; set; }
        public string Period { get; set; }
        [Required]
        public string ApplyStatus { get; set; }
        public string GuaranteeMethod { get; set; }
        [Required]
        public string ApplyTime { get; set; }
        public string PassedTime { get; set; }
    }
}