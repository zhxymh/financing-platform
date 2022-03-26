using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetailCreateDto
    {
        [Required]
        public string EnterpriseName { get; set; }
        [Required]
        public string TotalAssets { get; set; }
        [Required]
        public string Income { get; set; }
        [Required]
        public string EnterpriseType { get; set; }
        public int StaffNumber { get; set; }
        [Required]
        public string Industry { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string RegisteredAddress { get; set; }
        [Required]
        public string BusinessAddress { get; set; }
        [Required]
        public string BusinessScope { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CompleteTxId { get; set; }
        public string CommitUserName { get; set; }
        
        public IFormFile ExtraInfoFile { get; set; }
    }
}