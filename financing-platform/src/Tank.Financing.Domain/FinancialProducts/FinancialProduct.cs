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
        public virtual int Period { get; set; }

        public virtual GuaranteeMethod GuaranteeMethod { get; set; }

        [NotNull]
        public virtual string CreditCeiling { get; set; }

        [NotNull]
        public virtual string Organization { get; set; }

        public virtual int? AppliedNumber { get; set; }

        [NotNull]
        public virtual string APR { get; set; }

        [CanBeNull]
        public virtual string Rating { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public FinancialProduct()
        {

        }

        public FinancialProduct(Guid id, int period, GuaranteeMethod guaranteeMethod, string creditCeiling, string organization, string aPR, string name, int? appliedNumber = null, string rating = null)
        {
            Id = id;
            if (period < FinancialProductConsts.PeriodMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(period), period, "The value of 'period' cannot be lower than " + FinancialProductConsts.PeriodMinLength);
            }

            if (period > FinancialProductConsts.PeriodMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(period), period, "The value of 'period' cannot be greater than " + FinancialProductConsts.PeriodMaxLength);
            }

            Check.NotNull(creditCeiling, nameof(creditCeiling));
            Check.NotNull(organization, nameof(organization));
            Check.Length(organization, nameof(organization), FinancialProductConsts.OrganizationMaxLength, FinancialProductConsts.OrganizationMinLength);
            Check.NotNull(aPR, nameof(aPR));
            Check.NotNull(name, nameof(name));
            Period = period;
            GuaranteeMethod = guaranteeMethod;
            CreditCeiling = creditCeiling;
            Organization = organization;
            APR = aPR;
            Name = name;
            AppliedNumber = appliedNumber;
            Rating = rating;
        }
    }
}