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
        public virtual int TimeLimit { get; set; }

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

        public FinancialProduct(Guid id, int timeLimit, GuaranteeMethod guaranteeMethod, string creditCeiling, string organization, string aPR, string name, int? appliedNumber = null, string rating = null)
        {
            Id = id;
            if (timeLimit < FinancialProductConsts.TimeLimitMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(timeLimit), timeLimit, "The value of 'timeLimit' cannot be lower than " + FinancialProductConsts.TimeLimitMinLength);
            }

            if (timeLimit > FinancialProductConsts.TimeLimitMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(timeLimit), timeLimit, "The value of 'timeLimit' cannot be greater than " + FinancialProductConsts.TimeLimitMaxLength);
            }

            Check.NotNull(creditCeiling, nameof(creditCeiling));
            Check.NotNull(organization, nameof(organization));
            Check.Length(organization, nameof(organization), FinancialProductConsts.OrganizationMaxLength, FinancialProductConsts.OrganizationMinLength);
            Check.NotNull(aPR, nameof(aPR));
            Check.NotNull(name, nameof(name));
            TimeLimit = timeLimit;
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