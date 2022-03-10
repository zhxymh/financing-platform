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
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, productName, organization, periodMin, periodMax, guaranteeMethod, appliedNumberMin, appliedNumberMax, aPR, rating, creditCeilingMin, creditCeilingMax);
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, productName, organization, periodMin, periodMax, guaranteeMethod, appliedNumberMin, appliedNumberMax, aPR, rating, creditCeilingMin, creditCeilingMax);
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
            long? creditCeilingMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ProductName.Contains(filterText) || e.Organization.Contains(filterText) || e.APR.Contains(filterText) || e.Rating.Contains(filterText))
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
                    .WhereIf(creditCeilingMax.HasValue, e => e.CreditCeiling <= creditCeilingMax.Value);
        }
    }
}