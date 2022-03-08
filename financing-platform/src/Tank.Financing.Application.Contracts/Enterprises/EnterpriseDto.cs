using Tank.Financing;
using System;
using Volo.Abp.Application.Dtos;

namespace Tank.Financing.Enterprises
{
    public class EnterpriseDto : FullAuditedEntityDto<Guid>
    {
        public string EnterpriseName { get; set; }
        public string ArtificialPerson { get; set; }
        public long EstablishedTime { get; set; }
        public long DueTime { get; set; }
        public string CreditCode { get; set; }
        public string ArtificialPersonId { get; set; }
        public string RegisteredCapital { get; set; }
        public string PhoneNumber { get; set; }
        public string CertPhotoPath { get; set; }
        public string IdPhotoPath1 { get; set; }
        public string IdPhotoPath2 { get; set; }
        public CertificateStatus CertificateStatus { get; set; }
    }
}