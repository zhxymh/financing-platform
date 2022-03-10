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
        [Required]
        public string Allowance { get; set; }
        [Required]
        public string APR { get; set; }
        [Required]
        public string Period { get; set; }
        [Required]
        public ApplyStatus ApplyStatus { get; set; }
        public GuaranteeMethod GuaranteeMethod { get; set; }
        [Required]
        public long ApplyTime { get; set; }
        public long PassedTime { get; set; }
    }
}