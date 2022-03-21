using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tank.Financing.FinancialProducts;

public interface IQueryFinancialProductAppService
{
    Task<PagedResultDto<FinancialProductDto>> GetListAsync(GetFinancialProductsInput input);

    Task<FinancialProductDto> GetAsync(Guid id);
}