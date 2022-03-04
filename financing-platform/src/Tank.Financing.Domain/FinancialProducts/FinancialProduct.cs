using Tank.Financing;
using Tank.Financing.FinancialProducts;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProduct : AggregateRoot<Guid>
    {
        public virtual TJDistrict AvailableDistricts { get; set; }

        public virtual int TimeLimit { get; set; }

        public virtual GuaranteeMethod GuaranteeMethod { get; set; }

        public virtual decimal CreditCeiling { get; set; }

        [NotNull]
        public virtual string Organization { get; set; }

        public virtual int? AppliedNumber { get; set; }

        public virtual decimal APR { get; set; }

        public virtual int? Rating { get; set; }

        [NotNull]
        public virtual string Name { get; set; }
        public Guid? FinancialProductId { get; set; }

        public FinancialProduct()
        {

        }

        public FinancialProduct(Guid id, TJDistrict availableDistricts, int timeLimit, GuaranteeMethod guaranteeMethod, decimal creditCeiling, string organization, decimal aPR, string name, int? appliedNumber = null, int? rating = null)
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

            Check.NotNull(organization, nameof(organization));
            Check.Length(organization, nameof(organization), FinancialProductConsts.OrganizationMaxLength, FinancialProductConsts.OrganizationMinLength);
            Check.NotNull(name, nameof(name));
            AvailableDistricts = availableDistricts;
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