using Tank.Financing;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Tank.Financing.Enterprises
{
    public class Enterprise : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string EnterpriseName { get; set; }

        [NotNull]
        public virtual string ArtificialPerson { get; set; }

        public virtual long? EstablishedTime { get; set; }

        public virtual long DueTime { get; set; }

        [NotNull]
        public virtual string CreditCode { get; set; }

        [NotNull]
        public virtual string ArtificialPersonId { get; set; }

        [NotNull]
        public virtual string RegisteredCapital { get; set; }

        [NotNull]
        public virtual string PhoneNumber { get; set; }

        [NotNull]
        public virtual string CertPhotoPath { get; set; }

        [NotNull]
        public virtual string IdPhotoPath1 { get; set; }

        [NotNull]
        public virtual string IdPhotoPath2 { get; set; }

        public virtual CertificateStatus CertificateStatus { get; set; }

        public Enterprise()
        {

        }

        public Enterprise(Guid id, string enterpriseName, string artificialPerson, long dueTime, string creditCode, string artificialPersonId, string registeredCapital, string phoneNumber, string certPhotoPath, string idPhotoPath1, string idPhotoPath2, CertificateStatus certificateStatus, long? establishedTime = null)
        {
            Id = id;
            Check.NotNull(enterpriseName, nameof(enterpriseName));
            Check.NotNull(artificialPerson, nameof(artificialPerson));
            Check.NotNull(creditCode, nameof(creditCode));
            Check.NotNull(artificialPersonId, nameof(artificialPersonId));
            Check.NotNull(registeredCapital, nameof(registeredCapital));
            Check.NotNull(phoneNumber, nameof(phoneNumber));
            Check.NotNull(certPhotoPath, nameof(certPhotoPath));
            Check.NotNull(idPhotoPath1, nameof(idPhotoPath1));
            Check.NotNull(idPhotoPath2, nameof(idPhotoPath2));
            EnterpriseName = enterpriseName;
            ArtificialPerson = artificialPerson;
            DueTime = dueTime;
            CreditCode = creditCode;
            ArtificialPersonId = artificialPersonId;
            RegisteredCapital = registeredCapital;
            PhoneNumber = phoneNumber;
            CertPhotoPath = certPhotoPath;
            IdPhotoPath1 = idPhotoPath1;
            IdPhotoPath2 = idPhotoPath2;
            CertificateStatus = certificateStatus;
            EstablishedTime = establishedTime;
        }
    }
}