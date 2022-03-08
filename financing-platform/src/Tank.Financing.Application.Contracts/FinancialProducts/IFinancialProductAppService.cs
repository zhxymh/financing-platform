using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tank.Financing.FinancialProducts
{
    public interface IFinancialProductsAppService : IApplicationService
    {
        Task<PagedResultDto<FinancialProductDto>> GetListAsync(GetFinancialProductsInput input);

        Task<FinancialProductDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<FinancialProductDto> CreateAsync(FinancialProductCreateDto input);

        Task<FinancialProductDto> UpdateAsync(Guid id, FinancialProductUpdateDto input);
    }
}