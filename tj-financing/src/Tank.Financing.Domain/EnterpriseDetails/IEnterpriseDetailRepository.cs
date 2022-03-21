using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tank.Financing.EnterpriseDetails
{
    public interface IEnterpriseDetailRepository : IRepository<EnterpriseDetail, Guid>
    {
        Task<List<EnterpriseDetail>> GetListAsync(
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
            string completeTxId = null,
            string commitUserName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            string completeTxId = null,
            string commitUserName = null,
            CancellationToken cancellationToken = default);
    }
}