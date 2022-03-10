using Tank.Financing;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tank.Financing.Enterprises
{
    public class EnterpriseCreateDto
    {
        [Required]
        public string EnterpriseName { get; set; }
        [Required]
        public string ArtificialPerson { get; set; }
        public long? EstablishedTime { get; set; }
        public long DueTime { get; set; }
        [Required]
        public string CreditCode { get; set; }
        [Required]
        public string ArtificialPersonId { get; set; }
        [Required]
        public string RegisteredCapital { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CertPhotoPath { get; set; }
        [Required]
        public string IdPhotoPath1 { get; set; }
        [Required]
        public string IdPhotoPath2 { get; set; }
        [Required]
        public CertificateStatus CertificateStatus { get; set; } = ((CertificateStatus[])Enum.GetValues(typeof(CertificateStatus)))[0];
    }
}