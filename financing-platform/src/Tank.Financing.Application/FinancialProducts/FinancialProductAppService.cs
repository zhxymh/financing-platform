using Tank.Financing.Shared;
using Tank.Financing.FinancialProducts;
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

        public FinancialProductsAppService(IFinancialProductRepository financialProductRepository)
        {
            _financialProductRepository = financialProductRepository;
        }

        public virtual async Task<PagedResultDto<FinancialProductWithNavigationPropertiesDto>> GetListAsync(GetFinancialProductsInput input)
        {
            var totalCount = await _financialProductRepository.GetCountAsync(input.FilterText, input.AvailableDistricts, input.TimeLimitMin, input.TimeLimitMax, input.GuaranteeMethod, input.CreditCeilingMin, input.CreditCeilingMax, input.Organization, input.AppliedNumberMin, input.AppliedNumberMax, input.APRMin, input.APRMax, input.RatingMin, input.RatingMax, input.Name, input.FinancialProductId);
            var items = await _financialProductRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AvailableDistricts, input.TimeLimitMin, input.TimeLimitMax, input.GuaranteeMethod, input.CreditCeilingMin, input.CreditCeilingMax, input.Organization, input.AppliedNumberMin, input.AppliedNumberMax, input.APRMin, input.APRMax, input.RatingMin, input.RatingMax, input.Name, input.FinancialProductId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<FinancialProductWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<FinancialProductWithNavigationProperties>, List<FinancialProductWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<FinancialProductWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<FinancialProductWithNavigationProperties, FinancialProductWithNavigationPropertiesDto>
                (await _financialProductRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<FinancialProductDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<FinancialProduct, FinancialProductDto>(await _financialProductRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetFinancialProductLookupAsync(LookupRequestDto input)
        {
            var query = (await _financialProductRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<FinancialProduct>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<FinancialProduct>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(FinancingPermissions.FinancialProducts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _financialProductRepository.DeleteAsync(id);
        }

        [Authorize(FinancingPermissions.FinancialProducts.Create)]
        public virtual async Task<FinancialProductDto> CreateAsync(FinancialProductCreateDto input)
        {

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