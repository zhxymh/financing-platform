using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Tank.Financing.Permissions;
using Tank.Financing.FinancialProducts;

namespace Tank.Financing.FinancialProducts
{

    [Authorize(FinancingPermissions.FinancialProducts.Default)]
    public class FinancialProductsAppService : ApplicationService, IFinancialProductsAppService
    {
        private readonly IFinancialProductRepository _financialProductRepository;
        private readonly IBlockchainAppService _blockchainAppService;

        public FinancialProductsAppService(IFinancialProductRepository financialProductRepository,
            IBlockchainAppService blockchainAppService)
        {
            _financialProductRepository = financialProductRepository;
            _blockchainAppService = blockchainAppService;
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
            return ObjectMapper.Map<FinancialProduct, FinancialProductDto>(await _financialProductRepository.GetAsync(id));
        }

        [Authorize(FinancingPermissions.FinancialProducts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _financialProductRepository.DeleteAsync(id);
        }

        [Authorize(FinancingPermissions.FinancialProducts.Create)]
        public virtual async Task<FinancialProductDto> CreateAsync(FinancialProductCreateDto input)
        {
            input.AddFinancingProductTxId = _blockchainAppService.AddFinancingProduct(input);
            var financialProduct = ObjectMapper.Map<FinancialProductCreateDto, FinancialProduct>(input);
            financialProduct = await _financialProductRepository.InsertAsync(financialProduct, autoSave: true);
            return ObjectMapper.Map<FinancialProduct, FinancialProductDto>(financialProduct);
        }

        [Authorize(FinancingPermissions.FinancialProducts.Edit)]
        public virtual async Task<FinancialProductDto> UpdateAsync(Guid id, FinancialProductUpdateDto input)
        {
            var financialProduct = await _financialProductRepository.GetAsync(id);
            ObjectMapper.Map(input, financialProduct);
            financialProduct = await _financialProductRepository.UpdateAsync(financialProduct, autoSave: true);
            return ObjectMapper.Map<FinancialProduct, FinancialProductDto>(financialProduct);
        }
    }
}