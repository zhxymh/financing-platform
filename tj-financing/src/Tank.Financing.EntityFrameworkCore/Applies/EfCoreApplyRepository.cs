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
            string aPR = null,
            string period = null,
            ApplyStatus? applyStatus = null,
            GuaranteeMethod? guaranteeMethod = null,
            long? applyTimeMin = null,
            long? applyTimeMax = null,
            long? passedTimeMin = null,
            long? passedTimeMax = null,
            string applyTxId = null,
            string onlineApproveTxId = null,
            string offlineApproveTxId = null,
            string approveAllowanceTxId = null,
            string setAllowanceTxId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, enterpriseName, organization, productName, allowance, aPR, period, applyStatus, guaranteeMethod, applyTimeMin, applyTimeMax, passedTimeMin, passedTimeMax, applyTxId, onlineApproveTxId, offlineApproveTxId, approveAllowanceTxId, setAllowanceTxId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ApplyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string enterpriseName = null,
            string organization = null,
            string productName = null,
            string allowance = null,
            string aPR = null,
            string period = null,
            ApplyStatus? applyStatus = null,
            GuaranteeMethod? guaranteeMethod = null,
            long? applyTimeMin = null,
            long? applyTimeMax = null,
            long? passedTimeMin = null,
            long? passedTimeMax = null,
            string applyTxId = null,
            string onlineApproveTxId = null,
            string offlineApproveTxId = null,
            string approveAllowanceTxId = null,
            string setAllowanceTxId = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, enterpriseName, organization, productName, allowance, aPR, period, applyStatus, guaranteeMethod, applyTimeMin, applyTimeMax, passedTimeMin, passedTimeMax, applyTxId, onlineApproveTxId, offlineApproveTxId, approveAllowanceTxId, setAllowanceTxId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Apply> ApplyFilter(
            IQueryable<Apply> query,
            string filterText,
            string enterpriseName = null,
            string organization = null,
            string productName = null,
            string allowance = null,
            string aPR = null,
            string period = null,
            ApplyStatus? applyStatus = null,
            GuaranteeMethod? guaranteeMethod = null,
            long? applyTimeMin = null,
            long? applyTimeMax = null,
            long? passedTimeMin = null,
            long? passedTimeMax = null,
            string applyTxId = null,
            string onlineApproveTxId = null,
            string offlineApproveTxId = null,
            string approveAllowanceTxId = null,
            string setAllowanceTxId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EnterpriseName.Contains(filterText) || e.Organization.Contains(filterText) || e.ProductName.Contains(filterText) || e.Allowance.Contains(filterText) || e.APR.Contains(filterText) || e.Period.Contains(filterText) || e.ApplyTxId.Contains(filterText) || e.OnlineApproveTxId.Contains(filterText) || e.OfflineApproveTxId.Contains(filterText) || e.ApproveAllowanceTxId.Contains(filterText) || e.SetAllowanceTxId.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(enterpriseName), e => e.EnterpriseName.Contains(enterpriseName))
                    .WhereIf(!string.IsNullOrWhiteSpace(organization), e => e.Organization.Contains(organization))
                    .WhereIf(!string.IsNullOrWhiteSpace(productName), e => e.ProductName.Contains(productName))
                    .WhereIf(!string.IsNullOrWhiteSpace(allowance), e => e.Allowance.Contains(allowance))
                    .WhereIf(!string.IsNullOrWhiteSpace(aPR), e => e.APR.Contains(aPR))
                    .WhereIf(!string.IsNullOrWhiteSpace(period), e => e.Period.Contains(period))
                    .WhereIf(applyStatus.HasValue, e => e.ApplyStatus == applyStatus)
                    .WhereIf(guaranteeMethod.HasValue, e => e.GuaranteeMethod == guaranteeMethod)
                    .WhereIf(applyTimeMin.HasValue, e => e.ApplyTime >= applyTimeMin.Value)
                    .WhereIf(applyTimeMax.HasValue, e => e.ApplyTime <= applyTimeMax.Value)
                    .WhereIf(passedTimeMin.HasValue, e => e.PassedTime >= passedTimeMin.Value)
                    .WhereIf(passedTimeMax.HasValue, e => e.PassedTime <= passedTimeMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(applyTxId), e => e.ApplyTxId.Contains(applyTxId))
                    .WhereIf(!string.IsNullOrWhiteSpace(onlineApproveTxId), e => e.OnlineApproveTxId.Contains(onlineApproveTxId))
                    .WhereIf(!string.IsNullOrWhiteSpace(offlineApproveTxId), e => e.OfflineApproveTxId.Contains(offlineApproveTxId))
                    .WhereIf(!string.IsNullOrWhiteSpace(approveAllowanceTxId), e => e.ApproveAllowanceTxId.Contains(approveAllowanceTxId))
                    .WhereIf(!string.IsNullOrWhiteSpace(setAllowanceTxId), e => e.SetAllowanceTxId.Contains(setAllowanceTxId));
        }
    }
}