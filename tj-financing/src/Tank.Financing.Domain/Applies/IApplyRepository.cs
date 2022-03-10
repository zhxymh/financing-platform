using Tank.Financing;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tank.Financing.Applies
{
    public interface IApplyRepository : IRepository<Apply, Guid>
    {
        Task<List<Apply>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}