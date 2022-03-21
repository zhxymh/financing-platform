using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tank.Financing.FinancialProducts;

public class QueryFinancialProductAppService : ApplicationService, IQueryFinancialProductAppService
{
    private readonly IFinancialProductRepository _financialProductRepository;

    public QueryFinancialProductAppService(IFinancialProductRepository financialProductRepository)
    {
        _financialProductRepository = financialProductRepository;
    }

    public virtual async Task<PagedResultDto<FinancialProductDto>> GetListAsync(GetFinancialProductsInput input)
    {
        var totalCount = await _financialProductRepository.GetCountAsync(input.FilterText, input.ProductName,
            input.Organization, input.PeriodMin, input.PeriodMax, input.GuaranteeMethod, input.AppliedNumberMin,
            input.AppliedNumberMax, input.APR, input.Rating, input.CreditCeilingMin, input.CreditCeilingMax,
            input.AddFinancingProductTxId, input.url_logo1, input.url_logo2, input.url_logo3, input.url_logo4,
            input.url_logo5, input.features);
        var items = await _financialProductRepository.GetListAsync(input.FilterText, input.ProductName,
            input.Organization, input.PeriodMin, input.PeriodMax, input.GuaranteeMethod, input.AppliedNumberMin,
            input.AppliedNumberMax, input.APR, input.Rating, input.CreditCeilingMin, input.CreditCeilingMax,
            input.AddFinancingProductTxId, input.url_logo1, input.url_logo2, input.url_logo3, input.url_logo4,
            input.url_logo5, input.features, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<FinancialProductDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<FinancialProduct>, List<FinancialProductDto>>(items)
        };
    }

    public virtual async Task<FinancialProductDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<FinancialProduct, FinancialProductDto>(
            await _financialProductRepository.GetAsync(id));
    }
}