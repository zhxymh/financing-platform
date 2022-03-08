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

namespace Tank.Financing.Applies
{
    public class EfCoreApplyRepository : EfCoreRepository<FinancingDbContext, Apply, Guid>, IApplyRepository
    {
        public EfCoreApplyRepository(IDbContextProvider<FinancingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Apply>> GetListAsync(
            string filterText = null,
            string enterpriseName = null,
            string organization = null,
            string productName = null,
            string allowance = null,
            string aPY = null,
            string period = null,
            ApplyStatus? applyStatus = null,
            GuaranteeMethod? guaranteeMethod = null,
            string applyTime = null,
            string passedTime = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, enterpriseName, organization, productName, allowance, aPY, period, applyStatus, guaranteeMethod, applyTime, passedTime);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ApplyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string enterpriseName = null,
            string organization = null,
            string productName = null,
            string allowance = null,
            string aPY = null,
            string period = null,
            ApplyStatus? applyStatus = null,
            GuaranteeMethod? guaranteeMethod = null,
            string applyTime = null,
            string passedTime = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, enterpriseName, organization, productName, allowance, aPY, period, applyStatus, guaranteeMethod, applyTime, passedTime);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Apply> ApplyFilter(
            IQueryable<Apply> query,
            string filterText,
            string enterpriseName = null,
            string organization = null,
            string productName = null,
            string allowance = null,
            string aPY = null,
            string period = null,
            ApplyStatus? applyStatus = null,
            GuaranteeMethod? guaranteeMethod = null,
            string applyTime = null,
            string passedTime = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EnterpriseName.Contains(filterText) || e.Organization.Contains(filterText) || e.ProductName.Contains(filterText) || e.Allowance.Contains(filterText) || e.APY.Contains(filterText) || e.Period.Contains(filterText) || e.ApplyTime.Contains(filterText) || e.PassedTime.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(enterpriseName), e => e.EnterpriseName.Contains(enterpriseName))
                    .WhereIf(!string.IsNullOrWhiteSpace(organization), e => e.Organization.Contains(organization))
                    .WhereIf(!string.IsNullOrWhiteSpace(productName), e => e.ProductName.Contains(productName))
                    .WhereIf(!string.IsNullOrWhiteSpace(allowance), e => e.Allowance.Contains(allowance))
                    .WhereIf(!string.IsNullOrWhiteSpace(aPY), e => e.APY.Contains(aPY))
                    .WhereIf(!string.IsNullOrWhiteSpace(period), e => e.Period.Contains(period))
                    .WhereIf(applyStatus.HasValue, e => e.ApplyStatus == applyStatus)
                    .WhereIf(guaranteeMethod.HasValue, e => e.GuaranteeMethod == guaranteeMethod)
                    .WhereIf(!string.IsNullOrWhiteSpace(applyTime), e => e.ApplyTime.Contains(applyTime))
                    .WhereIf(!string.IsNullOrWhiteSpace(passedTime), e => e.PassedTime.Contains(passedTime));
        }
    }
}