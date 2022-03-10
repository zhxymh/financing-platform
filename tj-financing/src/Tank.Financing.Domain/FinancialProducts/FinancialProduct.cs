using Tank.Financing;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProduct : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string ProductName { get; set; }

        [NotNull]
        public virtual string Organization { get; set; }

        public virtual int? Period { get; set; }

        public virtual GuaranteeMethod GuaranteeMethod { get; set; }

        public virtual int AppliedNumber { get; set; }

        [CanBeNull]
        public virtual string APR { get; set; }

        [CanBeNull]
        public virtual string Rating { get; set; }

        public virtual long CreditCeiling { get; set; }

        [CanBeNull]
        public virtual string AddFinancingProductTxId { get; set; }

        public FinancialProduct()
        {

        }

        public FinancialProduct(Guid id, string productName, string organization, GuaranteeMethod guaranteeMethod, int appliedNumber, string aPR, string rating, long creditCeiling, string addFinancingProductTxId, int? period = null)
        {
            Id = id;
            Check.NotNull(productName, nameof(productName));
            Check.NotNull(organization, nameof(organization));
            ProductName = productName;
            Organization = organization;
            GuaranteeMethod = guaranteeMethod;
            AppliedNumber = appliedNumber;
            APR = aPR;
            Rating = rating;
            CreditCeiling = creditCeiling;
            AddFinancingProductTxId = addFinancingProductTxId;
            Period = period;
        }
    }
}