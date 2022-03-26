using Tank.Financing.EnterpriseDetails;
using Tank.Financing.Applies;
using Tank.Financing.FinancialProducts;
using Tank.Financing.Enterprises;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Payment.EntityFrameworkCore;
using Volo.FileManagement.EntityFrameworkCore;

namespace Tank.Financing.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class FinancingDbContext :
    AbpDbContext<FinancingDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<EnterpriseDetail> EnterpriseDetails { get; set; }
    public DbSet<Apply> Applies { get; set; }
    public DbSet<FinancialProduct> FinancialProducts { get; set; }
    public DbSet<Enterprise> Enterprises { get; set; }
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public FinancingDbContext(DbContextOptions<FinancingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureIdentityServer();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigurePayment();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(FinancingConsts.DbTablePrefix + "YourEntities", FinancingConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        builder.ConfigureFileManagement();
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Apply>(b =>
{
    b.ToTable(FinancingConsts.DbTablePrefix + "Applies", FinancingConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.EnterpriseName).HasColumnName(nameof(Apply.EnterpriseName)).IsRequired();
    b.Property(x => x.Organization).HasColumnName(nameof(Apply.Organization)).IsRequired();
    b.Property(x => x.ProductName).HasColumnName(nameof(Apply.ProductName)).IsRequired();
    b.Property(x => x.Allowance).HasColumnName(nameof(Apply.Allowance));
    b.Property(x => x.APR).HasColumnName(nameof(Apply.APR));
    b.Property(x => x.Period).HasColumnName(nameof(Apply.Period)).IsRequired();
    b.Property(x => x.ApplyStatus).HasColumnName(nameof(Apply.ApplyStatus)).IsRequired();
    b.Property(x => x.GuaranteeMethod).HasColumnName(nameof(Apply.GuaranteeMethod));
    b.Property(x => x.ApplyTime).HasColumnName(nameof(Apply.ApplyTime)).IsRequired();
    b.Property(x => x.PassedTime).HasColumnName(nameof(Apply.PassedTime));
    b.Property(x => x.ApplyTxId).HasColumnName(nameof(Apply.ApplyTxId)).IsRequired();
    b.Property(x => x.OnlineApproveTxId).HasColumnName(nameof(Apply.OnlineApproveTxId));
    b.Property(x => x.OfflineApproveTxId).HasColumnName(nameof(Apply.OfflineApproveTxId));
    b.Property(x => x.ApproveAllowanceTxId).HasColumnName(nameof(Apply.ApproveAllowanceTxId));
    b.Property(x => x.SetAllowanceTxId).HasColumnName(nameof(Apply.SetAllowanceTxId));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Enterprise>(b =>
{
    b.ToTable(FinancingConsts.DbTablePrefix + "Enterprises", FinancingConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.EnterpriseName).HasColumnName(nameof(Enterprise.EnterpriseName)).IsRequired();
    b.Property(x => x.ArtificialPerson).HasColumnName(nameof(Enterprise.ArtificialPerson)).IsRequired();
    b.Property(x => x.EstablishedTime).HasColumnName(nameof(Enterprise.EstablishedTime));
    b.Property(x => x.DueTime).HasColumnName(nameof(Enterprise.DueTime));
    b.Property(x => x.CreditCode).HasColumnName(nameof(Enterprise.CreditCode)).IsRequired();
    b.Property(x => x.ArtificialPersonId).HasColumnName(nameof(Enterprise.ArtificialPersonId)).IsRequired();
    b.Property(x => x.RegisteredCapital).HasColumnName(nameof(Enterprise.RegisteredCapital)).IsRequired();
    b.Property(x => x.PhoneNumber).HasColumnName(nameof(Enterprise.PhoneNumber)).IsRequired();
    b.Property(x => x.CertPhotoPath).HasColumnName(nameof(Enterprise.CertPhotoPath)).IsRequired();
    b.Property(x => x.IdPhotoPath1).HasColumnName(nameof(Enterprise.IdPhotoPath1)).IsRequired();
    b.Property(x => x.IdPhotoPath2).HasColumnName(nameof(Enterprise.IdPhotoPath2)).IsRequired();
    b.Property(x => x.CertificateStatus).HasColumnName(nameof(Enterprise.CertificateStatus)).IsRequired();
    b.Property(x => x.CertificateTxId).HasColumnName(nameof(Enterprise.CertificateTxId)).IsRequired();
    b.Property(x => x.ConfirmCertificateTxId).HasColumnName(nameof(Enterprise.ConfirmCertificateTxId));
    b.Property(x => x.CommitUserName).HasColumnName(nameof(Enterprise.CommitUserName)).IsRequired();
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<FinancialProduct>(b =>
{
    b.ToTable(FinancingConsts.DbTablePrefix + "FinancialProducts", FinancingConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.ProductName).HasColumnName(nameof(FinancialProduct.ProductName)).IsRequired();
    b.Property(x => x.Organization).HasColumnName(nameof(FinancialProduct.Organization)).IsRequired();
    b.Property(x => x.Period).HasColumnName(nameof(FinancialProduct.Period));
    b.Property(x => x.GuaranteeMethod).HasColumnName(nameof(FinancialProduct.GuaranteeMethod));
    b.Property(x => x.AppliedNumber).HasColumnName(nameof(FinancialProduct.AppliedNumber));
    b.Property(x => x.APR).HasColumnName(nameof(FinancialProduct.APR));
    b.Property(x => x.Rating).HasColumnName(nameof(FinancialProduct.Rating));
    b.Property(x => x.CreditCeiling).HasColumnName(nameof(FinancialProduct.CreditCeiling));
    b.Property(x => x.AddFinancingProductTxId).HasColumnName(nameof(FinancialProduct.AddFinancingProductTxId)).IsRequired();
    b.Property(x => x.url_logo1).HasColumnName(nameof(FinancialProduct.url_logo1));
    b.Property(x => x.url_logo2).HasColumnName(nameof(FinancialProduct.url_logo2));
    b.Property(x => x.url_logo3).HasColumnName(nameof(FinancialProduct.url_logo3));
    b.Property(x => x.url_logo4).HasColumnName(nameof(FinancialProduct.url_logo4));
    b.Property(x => x.url_logo5).HasColumnName(nameof(FinancialProduct.url_logo5));
    b.Property(x => x.features).HasColumnName(nameof(FinancialProduct.features));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<EnterpriseDetail>(b =>
            {
                b.ToTable(FinancingConsts.DbTablePrefix + "EnterpriseDetails", FinancingConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.EnterpriseName).HasColumnName(nameof(EnterpriseDetail.EnterpriseName)).IsRequired();
                b.Property(x => x.TotalAssets).HasColumnName(nameof(EnterpriseDetail.TotalAssets));//.IsRequired();
                b.Property(x => x.Income).HasColumnName(nameof(EnterpriseDetail.Income));//.IsRequired();
                //b.Property(x => x.EnterpriseType).HasColumnName(nameof(EnterpriseDetail.EnterpriseType));//.IsRequired();
                //b.Property(x => x.StaffNumber).HasColumnName(nameof(EnterpriseDetail.StaffNumber));
                b.Property(x => x.Industry).HasColumnName(nameof(EnterpriseDetail.Industry));//.IsRequired();
                //b.Property(x => x.Location).HasColumnName(nameof(EnterpriseDetail.Location));//.IsRequired();
                b.Property(x => x.RegisteredAddress).HasColumnName(nameof(EnterpriseDetail.RegisteredAddress));//.IsRequired();
                b.Property(x => x.BusinessAddress).HasColumnName(nameof(EnterpriseDetail.BusinessAddress));//.IsRequired();
                //b.Property(x => x.BusinessScope).HasColumnName(nameof(EnterpriseDetail.BusinessScope));//.IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(EnterpriseDetail.Description));//.IsRequired();
                b.Property(x => x.CompleteTxId).HasColumnName(nameof(EnterpriseDetail.CompleteTxId));//.IsRequired();
                b.Property(x => x.CommitUserName).HasColumnName(nameof(EnterpriseDetail.CommitUserName));
                
                
                b.Property(x => x.EnterpriseType).HasColumnName(nameof(EnterpriseDetail.EnterpriseType)).IsRequired();
                b.Property(x => x.StaffNumber).HasColumnName(nameof(EnterpriseDetail.StaffNumber)).IsRequired();
                b.Property(x => x.Location).HasColumnName(nameof(EnterpriseDetail.Location)).IsRequired();
                b.Property(x => x.BusinessScope).HasColumnName(nameof(EnterpriseDetail.BusinessScope)).IsRequired();
                b.Property(x => x.RegisteredAssets).HasColumnName(nameof(EnterpriseDetail.RegisteredAssets)).IsRequired();
                b.Property(x => x.PaidAssets).HasColumnName(nameof(EnterpriseDetail.PaidAssets)).IsRequired();
                b.Property(x => x.IncomePreYear).HasColumnName(nameof(EnterpriseDetail.IncomePreYear)).IsRequired();
                b.Property(x => x.ProfitPreYear).HasColumnName(nameof(EnterpriseDetail.ProfitPreYear)).IsRequired();
                b.Property(x => x.NetprofitPreYear).HasColumnName(nameof(EnterpriseDetail.NetprofitPreYear)).IsRequired();
                b.Property(x => x.TaxPreYear).HasColumnName(nameof(EnterpriseDetail.TaxPreYear)).IsRequired();
                b.Property(x => x.LiabilityPreYear).HasColumnName(nameof(EnterpriseDetail.LiabilityPreYear)).IsRequired();
                b.Property(x => x.HasExGuarant).HasColumnName(nameof(EnterpriseDetail.HasExGuarant)).IsRequired();
                b.Property(x => x.VatShouldpayPreYear).HasColumnName(nameof(EnterpriseDetail.VatShouldpayPreYear)).IsRequired();
                b.Property(x => x.VatPaidPerYear).HasColumnName(nameof(EnterpriseDetail.VatPaidPerYear)).IsRequired();
                b.Property(x => x.IncomeTaxPreYear).HasColumnName(nameof(EnterpriseDetail.IncomeTaxPreYear)).IsRequired();
                b.Property(x => x.IncomePaidTaxPreYear).HasColumnName(nameof(EnterpriseDetail.IncomePaidTaxPreYear)).IsRequired();
                b.Property(x => x.SocialsecurityNumber).HasColumnName(nameof(EnterpriseDetail.SocialsecurityNumber)).IsRequired();
                b.Property(x => x.HousefundPaidPreYear).HasColumnName(nameof(EnterpriseDetail.HousefundPaidPreYear)).IsRequired();
                b.Property(x => x.EnvCreditLevel).HasColumnName(nameof(EnterpriseDetail.EnvCreditLevel)).IsRequired();
                b.Property(x => x.EnvCreditScore).HasColumnName(nameof(EnterpriseDetail.EnvCreditScore)).IsRequired();
                b.Property(x => x.PatentNumber).HasColumnName(nameof(EnterpriseDetail.PatentNumber)).IsRequired();
                b.Property(x => x.SoftbindNumber).HasColumnName(nameof(EnterpriseDetail.SoftbindNumber)).IsRequired();
                
                #region evaluate
                b.Property(x => x.MarketScore).HasColumnName(nameof(EnterpriseDetail.MarketScore));
                b.Property(x => x.MarketDes).HasColumnName(nameof(EnterpriseDetail.MarketDes));
                b.Property(x => x.ManageScore).HasColumnName(nameof(EnterpriseDetail.ManageScore));
                b.Property(x => x.ManageDes).HasColumnName(nameof(EnterpriseDetail.ManageDes));
                b.Property(x => x.ProfitScore).HasColumnName(nameof(EnterpriseDetail.ProfitScore));
                b.Property(x => x.ProfitDes).HasColumnName(nameof(EnterpriseDetail.ProfitDes));
                b.Property(x => x.FinanceScore).HasColumnName(nameof(EnterpriseDetail.FinanceScore));
                b.Property(x => x.FinanceDes).HasColumnName(nameof(EnterpriseDetail.FinanceDes));
                b.Property(x => x.InnovateScore).HasColumnName(nameof(EnterpriseDetail.InnovateScore));
                b.Property(x => x.InnovateDes).HasColumnName(nameof(EnterpriseDetail.InnovateDes));
                b.Property(x => x.CreditScore).HasColumnName(nameof(EnterpriseDetail.CreditScore));
                b.Property(x => x.CreditDes).HasColumnName(nameof(EnterpriseDetail.CreditDes));
                b.Property(x => x.CompreScore).HasColumnName(nameof(EnterpriseDetail.CompreScore));
                b.Property(x => x.CompreDes).HasColumnName(nameof(EnterpriseDetail.CompreDes));
                b.Property(x => x.HasEvaluate).HasColumnName(nameof(EnterpriseDetail.HasEvaluate));
                #endregion
                
                
                b.Property(x => x.ExtraInfoHash).HasColumnName(nameof(EnterpriseDetail.ExtraInfoHash));
            });
        }
        
    }
}