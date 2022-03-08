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

namespace Tank.Financing.EnterpriseDetails
{
    public class EfCoreEnterpriseDetailRepository : EfCoreRepository<FinancingDbContext, EnterpriseDetail, Guid>, IEnterpriseDetailRepository
    {
        public EfCoreEnterpriseDetailRepository(IDbContextProvider<FinancingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<EnterpriseDetail>> GetListAsync(
            string filterText = null,
            string enterpriseName = null,
            string totalAssets = null,
            string income = null,
            string enterpriseType = null,
            int? staffNumberMin = null,
            int? staffNumberMax = null,
            string industry = null,
            string location = null,
            string registeredAddress = null,
            string businessAddress = null,
            string businessScope = null,
            string description = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, enterpriseName, totalAssets, income, enterpriseType, staffNumberMin, staffNumberMax, industry, location, registeredAddress, businessAddress, businessScope, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EnterpriseDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string enterpriseName = null,
            string totalAssets = null,
            string income = null,
            string enterpriseType = null,
            int? staffNumberMin = null,
            int? staffNumberMax = null,
            string industry = null,
            string location = null,
            string registeredAddress = null,
            string businessAddress = null,
            string businessScope = null,
            string description = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, enterpriseName, totalAssets, income, enterpriseType, staffNumberMin, staffNumberMax, industry, location, registeredAddress, businessAddress, businessScope, description);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<EnterpriseDetail> ApplyFilter(
            IQueryable<EnterpriseDetail> query,
            string filterText,
            string enterpriseName = null,
            string totalAssets = null,
            string income = null,
            string enterpriseType = null,
            int? staffNumberMin = null,
            int? staffNumberMax = null,
            string industry = null,
            string location = null,
            string registeredAddress = null,
            string businessAddress = null,
            string businessScope = null,
            string description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EnterpriseName.Contains(filterText) || e.TotalAssets.Contains(filterText) || e.Income.Contains(filterText) || e.EnterpriseType.Contains(filterText) || e.Industry.Contains(filterText) || e.Location.Contains(filterText) || e.RegisteredAddress.Contains(filterText) || e.BusinessAddress.Contains(filterText) || e.BusinessScope.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(enterpriseName), e => e.EnterpriseName.Contains(enterpriseName))
                    .WhereIf(!string.IsNullOrWhiteSpace(totalAssets), e => e.TotalAssets.Contains(totalAssets))
                    .WhereIf(!string.IsNullOrWhiteSpace(income), e => e.Income.Contains(income))
                    .WhereIf(!string.IsNullOrWhiteSpace(enterpriseType), e => e.EnterpriseType.Contains(enterpriseType))
                    .WhereIf(staffNumberMin.HasValue, e => e.StaffNumber >= staffNumberMin.Value)
                    .WhereIf(staffNumberMax.HasValue, e => e.StaffNumber <= staffNumberMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(industry), e => e.Industry.Contains(industry))
                    .WhereIf(!string.IsNullOrWhiteSpace(location), e => e.Location.Contains(location))
                    .WhereIf(!string.IsNullOrWhiteSpace(registeredAddress), e => e.RegisteredAddress.Contains(registeredAddress))
                    .WhereIf(!string.IsNullOrWhiteSpace(businessAddress), e => e.BusinessAddress.Contains(businessAddress))
                    .WhereIf(!string.IsNullOrWhiteSpace(businessScope), e => e.BusinessScope.Contains(businessScope))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}