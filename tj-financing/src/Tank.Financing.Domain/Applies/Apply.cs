using Tank.Financing;
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
        public virtual string EnterpriseName { get; set; }

        [NotNull]
        public virtual string Organization { get; set; }

        [NotNull]
        public virtual string ProductName { get; set; }

        [CanBeNull]
        public virtual string Allowance { get; set; }

        [CanBeNull]
        public virtual string APR { get; set; }

        [NotNull]
        public virtual string Period { get; set; }

        public virtual ApplyStatus ApplyStatus { get; set; }

        public virtual GuaranteeMethod GuaranteeMethod { get; set; }

        public virtual long ApplyTime { get; set; }

        public virtual long PassedTime { get; set; }

        [CanBeNull]
        public virtual string ApplyTxId { get; set; }

        [CanBeNull]
        public virtual string OnlineApproveTxId { get; set; }

        [CanBeNull]
        public virtual string OfflineApproveTxId { get; set; }

        [CanBeNull]
        public virtual string ApproveAllowanceTxId { get; set; }

        [CanBeNull]
        public virtual string SetAllowanceTxId { get; set; }

        public Apply()
        {

        }

        public Apply(Guid id, string enterpriseName, string organization, string productName, string allowance, string aPR, string period, ApplyStatus applyStatus, GuaranteeMethod guaranteeMethod, long applyTime, long passedTime, string applyTxId, string onlineApproveTxId, string offlineApproveTxId, string approveAllowanceTxId, string setAllowanceTxId)
        {
            Id = id;
            Check.NotNull(enterpriseName, nameof(enterpriseName));
            Check.NotNull(organization, nameof(organization));
            Check.NotNull(productName, nameof(productName));
            Check.NotNull(period, nameof(period));
            EnterpriseName = enterpriseName;
            Organization = organization;
            ProductName = productName;
            Allowance = allowance;
            APR = aPR;
            Period = period;
            ApplyStatus = applyStatus;
            GuaranteeMethod = guaranteeMethod;
            ApplyTime = applyTime;
            PassedTime = passedTime;
            ApplyTxId = applyTxId;
            OnlineApproveTxId = onlineApproveTxId;
            OfflineApproveTxId = offlineApproveTxId;
            ApproveAllowanceTxId = approveAllowanceTxId;
            SetAllowanceTxId = setAllowanceTxId;
        }
    }
}