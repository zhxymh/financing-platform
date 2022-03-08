using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Tank.Financing.Applies
{
    public class Apply : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string EnterpriceName { get; set; }

        [NotNull]
        public virtual string Organization { get; set; }

        [NotNull]
        public virtual string ProductName { get; set; }

        [CanBeNull]
        public virtual string Allowance { get; set; }

        [CanBeNull]
        public virtual string APY { get; set; }

        [CanBeNull]
        public virtual string Period { get; set; }

        [NotNull]
        public virtual string ApplyStatus { get; set; }

        [CanBeNull]
        public virtual string GuaranteeMethod { get; set; }

        [NotNull]
        public virtual string ApplyTime { get; set; }

        [CanBeNull]
        public virtual string PassedTime { get; set; }

        public Apply()
        {

        }

        public Apply(Guid id, string enterpriceName, string organization, string productName, string allowance, string aPY, string period, string applyStatus, string guaranteeMethod, string applyTime, string passedTime)
        {
            Id = id;
            Check.NotNull(enterpriceName, nameof(enterpriceName));
            Check.NotNull(organization, nameof(organization));
            Check.NotNull(productName, nameof(productName));
            Check.NotNull(applyStatus, nameof(applyStatus));
            Check.NotNull(applyTime, nameof(applyTime));
            EnterpriceName = enterpriceName;
            Organization = organization;
            ProductName = productName;
            Allowance = allowance;
            APY = aPY;
            Period = period;
            ApplyStatus = applyStatus;
            GuaranteeMethod = guaranteeMethod;
            ApplyTime = applyTime;
            PassedTime = passedTime;
        }
    }
}