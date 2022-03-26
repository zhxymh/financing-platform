using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetailCreateDto
    {
        [Required]
        public string EnterpriseName { get; set; }
        //[Required]
        public string TotalAssets { get; set; }
        //[Required]
        public string Income { get; set; }
        //[Required]
        //public string EnterpriseType { get; set; }
        //[Required]
        //public int StaffNumber { get; set; }
        //[Required]
        public string Industry { get; set; }
        //[Required]
        //public string Location { get; set; }
        //[Required]
        public string RegisteredAddress { get; set; }
        //[Required]
        public string BusinessAddress { get; set; }
        //[Required]
        //public string BusinessScope { get; set; }
        //[Required]
        public string Description { get; set; }
        public string CompleteTxId { get; set; }
        public string CommitUserName { get; set; }
        
        
        [Required]
        public string EnterpriseType { get; set; }
        [Required]
        public int StaffNumber { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string BusinessScope { get; set; }
        [Required]
        public string RegisteredAssets { get; set; }
        [Required]
        public string PaidAssets { get; set; }
        [Required]
        public string IncomePreYear { get; set; }
        [Required]
        public string ProfitPreYear { get; set; }
        [Required]
        public string NetprofitPreYear { get; set; }
        [Required]
        public string TaxPreYear { get; set; }
        [Required]
        public string LiabilityPreYear { get; set; }
        [Required]
        public int HasExGuarant { get; set; }
        [Required]
        public string VatShouldpayPreYear { get; set; }
        [Required]
        public string VatPaidPerYear { get; set; }
        [Required]
        public string IncomeTaxPreYear { get; set; }
        [Required]
        public string IncomePaidTaxPreYear { get; set; }
        [Required]
        public int SocialsecurityNumber { get; set; }
        [Required]
        public string HousefundPaidPreYear { get; set; }
        [Required]
        public int EnvCreditLevel { get; set; }
        [Required]
        public string EnvCreditScore { get; set; }
        [Required]
        public int PatentNumber { get; set; }
        [Required]
        public int SoftbindNumber { get; set; }
        
        public IFormFile ExtraInfoFile { get; set; }

        
    }
}