using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetail : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string EnterpriseName { get; set; }

        [NotNull]
        public virtual string TotalAssets { get; set; }

        [NotNull]
        public virtual string Income { get; set; }

        [NotNull]
        public virtual string EnterpriseType { get; set; }

        public virtual int StaffNumber { get; set; }

        [NotNull]
        public virtual string Industry { get; set; }

        [NotNull]
        public virtual string Location { get; set; }

        [NotNull]
        public virtual string RegisteredAddress { get; set; }

        [NotNull]
        public virtual string BusinessAddress { get; set; }

        [NotNull]
        public virtual string BusinessScope { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        [CanBeNull]
        public virtual string CompleteTxId { get; set; }

        public EnterpriseDetail()
        {

        }

        public EnterpriseDetail(Guid id, string enterpriseName, string totalAssets, string income, string enterpriseType, int staffNumber, string industry, string location, string registeredAddress, string businessAddress, string businessScope, string description, string completeTxId)
        {
            Id = id;
            Check.NotNull(enterpriseName, nameof(enterpriseName));
            Check.NotNull(totalAssets, nameof(totalAssets));
            Check.NotNull(income, nameof(income));
            Check.NotNull(enterpriseType, nameof(enterpriseType));
            Check.NotNull(industry, nameof(industry));
            Check.NotNull(location, nameof(location));
            Check.NotNull(registeredAddress, nameof(registeredAddress));
            Check.NotNull(businessAddress, nameof(businessAddress));
            Check.NotNull(businessScope, nameof(businessScope));
            Check.NotNull(description, nameof(description));
            EnterpriseName = enterpriseName;
            TotalAssets = totalAssets;
            Income = income;
            EnterpriseType = enterpriseType;
            StaffNumber = staffNumber;
            Industry = industry;
            Location = location;
            RegisteredAddress = registeredAddress;
            BusinessAddress = businessAddress;
            BusinessScope = businessScope;
            Description = description;
            CompleteTxId = completeTxId;
        }
    }
}