using Tank.Financing;
using Volo.Abp.Application.Dtos;
using System;

namespace Tank.Financing.Enterprises
{
    public class GetEnterprisesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string EnterpriseName { get; set; }
        public string ArtificialPerson { get; set; }
        public long? EstablishedTimeMin { get; set; }
        public long? EstablishedTimeMax { get; set; }
        public long? DueTimeMin { get; set; }
        public long? DueTimeMax { get; set; }
        public string CreditCode { get; set; }
        public string ArtificialPersonId { get; set; }
        public string RegisteredCapital { get; set; }
        public string PhoneNumber { get; set; }
        public string CertPhotoPath { get; set; }
        public string IdPhotoPath1 { get; set; }
        public string IdPhotoPath2 { get; set; }
        public CertificateStatus? CertificateStatus { get; set; }
        public string CertificateTxId { get; set; }
        public string ConfirmCertificateTxId { get; set; }
        public string CommitUserName { get; set; }

        public GetEnterprisesInput()
        {

        }
    }
}