using Tank.Financing.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tank.Financing.FinancialProducts
{
    public interface IFinancialProductsAppService : IApplicationService
    {
        Task<PagedResultDto<FinancialProductWithNavigationPropertiesDto>> GetListAsync(GetFinancialProductsInput input);

        Task<FinancialProductWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<FinancialProductDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetFinancialProductLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<FinancialProductDto> CreateAsync(FinancialProductCreateDto input);

        Task<FinancialProductDto> UpdateAsync(Guid id, FinancialProductUpdateDto input);
    }
}