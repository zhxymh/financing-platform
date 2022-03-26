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

        [CanBeNull]
        public virtual string TotalAssets { get; set; }

        [CanBeNull]
        public virtual string Income { get; set; }

        //public virtual string EnterpriseType { get; set; }

        //public virtual int StaffNumber { get; set; }

        [CanBeNull]
        public virtual string Industry { get; set; }

        //public virtual string Location { get; set; }

        [CanBeNull]
        public virtual string RegisteredAddress { get; set; }

        [CanBeNull]
        public virtual string BusinessAddress { get; set; }

        //public virtual string BusinessScope { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        [CanBeNull]
        public virtual string CompleteTxId { get; set; }

        [CanBeNull]
        public virtual string CommitUserName { get; set; }
        [CanBeNull]
        public virtual string ExtraInfoHash { get; set; }
        
        public virtual string EnterpriseType { get; set; }
        public virtual int StaffNumber { get; set; }
        public virtual string Location { get; set; }
        public virtual string BusinessScope { get; set; }
        public virtual string RegisteredAssets { get; set; }
        public virtual string PaidAssets { get; set; }
        public virtual string IncomePreYear { get; set; }
        public virtual string ProfitPreYear { get; set; }
        public virtual string NetprofitPreYear { get; set; }
        public virtual string TaxPreYear { get; set; }
        public virtual string LiabilityPreYear { get; set; }
        public virtual int HasExGuarant { get; set; }
        public virtual string VatShouldpayPreYear { get; set; }
        public virtual string VatPaidPerYear { get; set; }
        public virtual string IncomeTaxPreYear { get; set; }
        public virtual string IncomePaidTaxPreYear { get; set; }
        public virtual int SocialsecurityNumber { get; set; }
        public virtual string HousefundPaidPreYear { get; set; }
        public virtual int EnvCreditLevel { get; set; }
        public virtual string EnvCreditScore { get; set; }
        public virtual int PatentNumber { get; set; }
        public virtual int SoftbindNumber { get; set; }
        
        #region evaluate
        public virtual string MarketScore { get; set; }
        public virtual string MarketDes { get; set; }
        public virtual string ManageScore { get; set; }
        public virtual string ManageDes { get; set; }
        public virtual string ProfitScore { get; set; }
        public virtual string ProfitDes { get; set; }
        public virtual string FinanceScore { get; set; }
        public virtual string FinanceDes { get; set; }
        public virtual string InnovateScore { get; set; }
        public virtual string InnovateDes { get; set; }
        public virtual string CreditScore { get; set; }
        public virtual string CreditDes { get; set; }
        public virtual string CompreScore { get; set; }
        public virtual string CompreDes { get; set; }
        public virtual string HasEvaluate { get; set; }
        #endregion

        public EnterpriseDetail()
        {

        }

        //public EnterpriseDetail(Guid id, string enterpriseName, string totalAssets, string income, string enterpriseType, int staffNumber, string industry, string location, string registeredAddress, string businessAddress, string businessScope, string description, string completeTxId, string commitUserName)
        public EnterpriseDetail(Guid id, string EnterpriseName, 
            string EnterpriseType, 
            int StaffNumber, 
            string Location, 
            string BusinessScope,
            string IncomePreYear, 
            string ProfitPreYear, 
            string NetprofitPreYear, 
            string TaxPreYear, 
            string LiabilityPreYear, 
            int HasExGuarant, 
            string VatShouldpayPreYear, 
            string VatPaidPerYear, 
            string IncomeTaxPreYear, 
            string IncomePaidTaxPreYear, 
            int SocialsecurityNumber, 
            string HousefundPaidPreYear, 
            int EnvCreditLevel, 
            string EnvCreditScore, 
            int PatentNumber, 
            int SoftbindNumber 
            )
        {
            Id = id;
            /*
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
            Check.NotNull(completeTxId, nameof(completeTxId));
            
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
            CommitUserName = commitUserName;
            */
            Check.NotNull(EnterpriseName, nameof(EnterpriseName));
            Check.NotNull(EnterpriseType, nameof(EnterpriseType));
            Check.NotNull(Location, nameof(Location));
            Check.NotNull(BusinessScope, nameof(BusinessScope));
            Check.NotNull(IncomePreYear, nameof(IncomePreYear));
            Check.NotNull(ProfitPreYear, nameof(ProfitPreYear));
            Check.NotNull(NetprofitPreYear, nameof(NetprofitPreYear));
            Check.NotNull(TaxPreYear, nameof(TaxPreYear));
            Check.NotNull(LiabilityPreYear, nameof(LiabilityPreYear));
            Check.NotNull(VatShouldpayPreYear, nameof(VatShouldpayPreYear));
            Check.NotNull(VatPaidPerYear, nameof(VatPaidPerYear));
            Check.NotNull(IncomeTaxPreYear, nameof(IncomeTaxPreYear));
            Check.NotNull(IncomePaidTaxPreYear, nameof(IncomePaidTaxPreYear));
            Check.NotNull(HousefundPaidPreYear, nameof(HousefundPaidPreYear));
            Check.NotNull(EnvCreditScore, nameof(EnvCreditScore));

            this.EnterpriseName = EnterpriseName;
            this.EnterpriseType = EnterpriseType;
            this.StaffNumber = StaffNumber;
            this.Location = Location;
            this.BusinessScope = BusinessScope;
            this.IncomePreYear = IncomePreYear;
            this.ProfitPreYear = ProfitPreYear;
            this.NetprofitPreYear = NetprofitPreYear;
            this.TaxPreYear = TaxPreYear;
            this.LiabilityPreYear = LiabilityPreYear;
            this.HasExGuarant = HasExGuarant;
            this.VatShouldpayPreYear = VatShouldpayPreYear;
            this.VatPaidPerYear = VatPaidPerYear;
            this.IncomeTaxPreYear = IncomeTaxPreYear;
            this.IncomePaidTaxPreYear = IncomePaidTaxPreYear;
            this.SocialsecurityNumber = SocialsecurityNumber;
            this.HousefundPaidPreYear = HousefundPaidPreYear;
            this.EnvCreditLevel = EnvCreditLevel;
            this.EnvCreditScore = EnvCreditScore;
            this.PatentNumber = PatentNumber;
            this.SoftbindNumber = SoftbindNumber;
        }
    }
}