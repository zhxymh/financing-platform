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

        public async Task<FinancialProductWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryForNavigationPropertiesAsync())
                .FirstOrDefaultAsync(e => e.FinancialProduct.Id == id, GetCancellationToken(cancellationToken));
        }

        public async Task<List<FinancialProductWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            TJDistrict? availableDistricts = null,
            int? timeLimitMin = null,
            int? timeLimitMax = null,
            GuaranteeMethod? guaranteeMethod = null,
            decimal? creditCeilingMin = null,
            decimal? creditCeilingMax = null,
            string organization = null,
            int? appliedNumberMin = null,
            int? appliedNumberMax = null,
            decimal? aPRMin = null,
            decimal? aPRMax = null,
            int? ratingMin = null,
            int? ratingMax = null,
            string name = null,
            Guid? financialProductId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, availableDistricts, timeLimitMin, timeLimitMax, guaranteeMethod, creditCeilingMin, creditCeilingMax, organization, appliedNumberMin, appliedNumberMax, aPRMin, aPRMax, ratingMin, ratingMax, name, financialProductId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? FinancialProductConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<FinancialProductWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from financialProduct in (await GetDbSetAsync())
                   join financialProduct1 in (await GetDbContextAsync()).FinancialProducts on financialProduct.FinancialProductId equals financialProduct1.Id into financialProducts1
                   from financialProduct1 in financialProducts1.DefaultIfEmpty()

                   select new FinancialProductWithNavigationProperties
                   {
                       FinancialProduct = financialProduct,
                       FinancialProduct1 = financialProduct1
                   };
        }

        protected virtual IQueryable<FinancialProductWithNavigationProperties> ApplyFilter(
            IQueryable<FinancialProductWithNavigationProperties> query,
            string filterText,
            TJDistrict? availableDistricts = null,
            int? timeLimitMin = null,
            int? timeLimitMax = null,
            GuaranteeMethod? guaranteeMethod = null,
            decimal? creditCeilingMin = null,
            decimal? creditCeilingMax = null,
            string organization = null,
            int? appliedNumberMin = null,
            int? appliedNumberMax = null,
            decimal? aPRMin = null,
            decimal? aPRMax = null,
            int? ratingMin = null,
            int? ratingMax = null,
            string name = null,
            Guid? financialProductId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FinancialProduct.Organization.Contains(filterText) || e.FinancialProduct.Name.Contains(filterText))
                    .WhereIf(availableDistricts.HasValue, e => e.FinancialProduct.AvailableDistricts == availableDistricts)
                    .WhereIf(timeLimitMin.HasValue, e => e.FinancialProduct.TimeLimit >= timeLimitMin.Value)
                    .WhereIf(timeLimitMax.HasValue, e => e.FinancialProduct.TimeLimit <= timeLimitMax.Value)
                    .WhereIf(guaranteeMethod.HasValue, e => e.FinancialProduct.GuaranteeMethod == guaranteeMethod)
                    .WhereIf(creditCeilingMin.HasValue, e => e.FinancialProduct.CreditCeiling >= creditCeilingMin.Value)
                    .WhereIf(creditCeilingMax.HasValue, e => e.FinancialProduct.CreditCeiling <= creditCeilingMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(organization), e => e.FinancialProduct.Organization.Contains(organization))
                    .WhereIf(appliedNumberMin.HasValue, e => e.FinancialProduct.AppliedNumber >= appliedNumberMin.Value)
                    .WhereIf(appliedNumberMax.HasValue, e => e.FinancialProduct.AppliedNumber <= appliedNumberMax.Value)
                    .WhereIf(aPRMin.HasValue, e => e.FinancialProduct.APR >= aPRMin.Value)
                    .WhereIf(aPRMax.HasValue, e => e.FinancialProduct.APR <= aPRMax.Value)
                    .WhereIf(ratingMin.HasValue, e => e.FinancialProduct.Rating >= ratingMin.Value)
                    .WhereIf(ratingMax.HasValue, e => e.FinancialProduct.Rating <= ratingMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.FinancialProduct.Name.Contains(name))
                    .WhereIf(financialProductId != null && financialProductId != Guid.Empty, e => e.FinancialProduct != null && e.FinancialProduct.Id == financialProductId);
        }

        public async Task<List<FinancialProduct>> GetListAsync(
            string filterText = null,
            TJDistrict? availableDistricts = null,
            int? timeLimitMin = null,
            int? timeLimitMax = null,
            GuaranteeMethod? guaranteeMethod = null,
            decimal? creditCeilingMin = null,
            decimal? creditCeilingMax = null,
            string organization = null,
            int? appliedNumberMin = null,
            int? appliedNumberMax = null,
            decimal? aPRMin = null,
            decimal? aPRMax = null,
            int? ratingMin = null,
            int? ratingMax = null,
            string name = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, availableDistricts, timeLimitMin, timeLimitMax, guaranteeMethod, creditCeilingMin, creditCeilingMax, organization, appliedNumberMin, appliedNumberMax, aPRMin, aPRMax, ratingMin, ratingMax, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? FinancialProductConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            TJDistrict? availableDistricts = null,
            int? timeLimitMin = null,
            int? timeLimitMax = null,
            GuaranteeMethod? guaranteeMethod = null,
            decimal? creditCeilingMin = null,
            decimal? creditCeilingMax = null,
            string organization = null,
            int? appliedNumberMin = null,
            int? appliedNumberMax = null,
            decimal? aPRMin = null,
            decimal? aPRMax = null,
            int? ratingMin = null,
            int? ratingMax = null,
            string name = null,
            Guid? financialProductId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, availableDistricts, timeLimitMin, timeLimitMax, guaranteeMethod, creditCeilingMin, creditCeilingMax, organization, appliedNumberMin, appliedNumberMax, aPRMin, aPRMax, ratingMin, ratingMax, name, financialProductId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<FinancialProduct> ApplyFilter(
            IQueryable<FinancialProduct> query,
            string filterText,
            TJDistrict? availableDistricts = null,
            int? timeLimitMin = null,
            int? timeLimitMax = null,
            GuaranteeMethod? guaranteeMethod = null,
            decimal? creditCeilingMin = null,
            decimal? creditCeilingMax = null,
            string organization = null,
            int? appliedNumberMin = null,
            int? appliedNumberMax = null,
            decimal? aPRMin = null,
            decimal? aPRMax = null,
            int? ratingMin = null,
            int? ratingMax = null,
            string name = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Organization.Contains(filterText) || e.Name.Contains(filterText))
                    .WhereIf(availableDistricts.HasValue, e => e.AvailableDistricts == availableDistricts)
                    .WhereIf(timeLimitMin.HasValue, e => e.TimeLimit >= timeLimitMin.Value)
                    .WhereIf(timeLimitMax.HasValue, e => e.TimeLimit <= timeLimitMax.Value)
                    .WhereIf(guaranteeMethod.HasValue, e => e.GuaranteeMethod == guaranteeMethod)
                    .WhereIf(creditCeilingMin.HasValue, e => e.CreditCeiling >= creditCeilingMin.Value)
                    .WhereIf(creditCeilingMax.HasValue, e => e.CreditCeiling <= creditCeilingMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(organization), e => e.Organization.Contains(organization))
                    .WhereIf(appliedNumberMin.HasValue, e => e.AppliedNumber >= appliedNumberMin.Value)
                    .WhereIf(appliedNumberMax.HasValue, e => e.AppliedNumber <= appliedNumberMax.Value)
                    .WhereIf(aPRMin.HasValue, e => e.APR >= aPRMin.Value)
                    .WhereIf(aPRMax.HasValue, e => e.APR <= aPRMax.Value)
                    .WhereIf(ratingMin.HasValue, e => e.Rating >= ratingMin.Value)
                    .WhereIf(ratingMax.HasValue, e => e.Rating <= ratingMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name));
        }
    }
}