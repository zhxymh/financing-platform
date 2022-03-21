using Tank.Financing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Tank.Financing.EntityFrameworkCore;

namespace Tank.Financing.FinancialProducts
{
    public class EfCoreFinancialProductRepository : EfCoreRepository<FinancingDbContext, FinancialProduct, Guid>, IFinancialProductRepository
    {
        public EfCoreFinancialProductRepository(IDbContextProvider<FinancingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<FinancialProduct>> GetListAsync(
            string filterText = null,
            string productName = null,
            string organization = null,
            int? periodMin = null,
            int? periodMax = null,
            GuaranteeMethod? guaranteeMethod = null,
            int? appliedNumberMin = null,
            int? appliedNumberMax = null,
            string aPR = null,
            string rating = null,
            long? creditCeilingMin = null,
            long? creditCeilingMax = null,
            string addFinancingProductTxId = null,
            string url_logo1 = null,
            string url_logo2 = null,
            string url_logo3 = null,
            string url_logo4 = null,
            string url_logo5 = null,
            string features = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, productName, organization, periodMin, periodMax, guaranteeMethod, appliedNumberMin, appliedNumberMax, aPR, rating, creditCeilingMin, creditCeilingMax, addFinancingProductTxId, url_logo1, url_logo2, url_logo3, url_logo4, url_logo5, features);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? FinancialProductConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string productName = null,
            string organization = null,
            int? periodMin = null,
            int? periodMax = null,
            GuaranteeMethod? guaranteeMethod = null,
            int? appliedNumberMin = null,
            int? appliedNumberMax = null,
            string aPR = null,
            string rating = null,
            long? creditCeilingMin = null,
            long? creditCeilingMax = null,
            string addFinancingProductTxId = null,
            string url_logo1 = null,
            string url_logo2 = null,
            string url_logo3 = null,
            string url_logo4 = null,
            string url_logo5 = null,
            string features = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, productName, organization, periodMin, periodMax, guaranteeMethod, appliedNumberMin, appliedNumberMax, aPR, rating, creditCeilingMin, creditCeilingMax, addFinancingProductTxId, url_logo1, url_logo2, url_logo3, url_logo4, url_logo5, features);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<FinancialProduct> ApplyFilter(
            IQueryable<FinancialProduct> query,
            string filterText,
            string productName = null,
            string organization = null,
            int? periodMin = null,
            int? periodMax = null,
            GuaranteeMethod? guaranteeMethod = null,
            int? appliedNumberMin = null,
            int? appliedNumberMax = null,
            string aPR = null,
            string rating = null,
            long? creditCeilingMin = null,
            long? creditCeilingMax = null,
            string addFinancingProductTxId = null,
            string url_logo1 = null,
            string url_logo2 = null,
            string url_logo3 = null,
            string url_logo4 = null,
            string url_logo5 = null,
            string features = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ProductName.Contains(filterText) || e.Organization.Contains(filterText) || e.APR.Contains(filterText) || e.Rating.Contains(filterText) || e.AddFinancingProductTxId.Contains(filterText) || e.url_logo1.Contains(filterText) || e.url_logo2.Contains(filterText) || e.url_logo3.Contains(filterText) || e.url_logo4.Contains(filterText) || e.url_logo5.Contains(filterText) || e.features.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(productName), e => e.ProductName.Contains(productName))
                    .WhereIf(!string.IsNullOrWhiteSpace(organization), e => e.Organization.Contains(organization))
                    .WhereIf(periodMin.HasValue, e => e.Period >= periodMin.Value)
                    .WhereIf(periodMax.HasValue, e => e.Period <= periodMax.Value)
                    .WhereIf(guaranteeMethod.HasValue, e => e.GuaranteeMethod == guaranteeMethod)
                    .WhereIf(appliedNumberMin.HasValue, e => e.AppliedNumber >= appliedNumberMin.Value)
                    .WhereIf(appliedNumberMax.HasValue, e => e.AppliedNumber <= appliedNumberMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(aPR), e => e.APR.Contains(aPR))
                    .WhereIf(!string.IsNullOrWhiteSpace(rating), e => e.Rating.Contains(rating))
                    .WhereIf(creditCeilingMin.HasValue, e => e.CreditCeiling >= creditCeilingMin.Value)
                    .WhereIf(creditCeilingMax.HasValue, e => e.CreditCeiling <= creditCeilingMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(addFinancingProductTxId), e => e.AddFinancingProductTxId.Contains(addFinancingProductTxId))
                    .WhereIf(!string.IsNullOrWhiteSpace(url_logo1), e => e.url_logo1.Contains(url_logo1))
                    .WhereIf(!string.IsNullOrWhiteSpace(url_logo2), e => e.url_logo2.Contains(url_logo2))
                    .WhereIf(!string.IsNullOrWhiteSpace(url_logo3), e => e.url_logo3.Contains(url_logo3))
                    .WhereIf(!string.IsNullOrWhiteSpace(url_logo4), e => e.url_logo4.Contains(url_logo4))
                    .WhereIf(!string.IsNullOrWhiteSpace(url_logo5), e => e.url_logo5.Contains(url_logo5))
                    .WhereIf(!string.IsNullOrWhiteSpace(features), e => e.features.Contains(features));
        }
    }
}