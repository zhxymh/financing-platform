using Tank.Financing;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tank.Financing.FinancialProducts
{
    public interface IFinancialProductRepository : IRepository<FinancialProduct, Guid>
    {
        Task<List<FinancialProduct>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}