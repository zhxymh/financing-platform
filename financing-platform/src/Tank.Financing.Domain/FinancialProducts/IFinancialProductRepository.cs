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
        Task<FinancialProductWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<FinancialProductWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<FinancialProduct>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}