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
using Tank.Financing.Applies;

namespace Tank.Financing.Applies
{

    [Authorize(FinancingPermissions.Applies.Default)]
    public class AppliesAppService : ApplicationService, IAppliesAppService
    {
        private readonly IApplyRepository _applyRepository;
        private readonly IBlockchainAppService _blockchainAppService;

        public AppliesAppService(IApplyRepository applyRepository, IBlockchainAppService blockchainAppService)
        {
            _applyRepository = applyRepository;
            _blockchainAppService = blockchainAppService;
        }

        public virtual async Task<PagedResultDto<ApplyDto>> GetListAsync(GetAppliesInput input)
        {
            var totalCount = await _applyRepository.GetCountAsync(input.FilterText, input.EnterpriseName, input.Organization, input.ProductName, input.Allowance, input.APR, input.Period, input.ApplyStatus, input.GuaranteeMethod, input.ApplyTimeMin, input.ApplyTimeMax, input.PassedTimeMin, input.PassedTimeMax);
            var items = await _applyRepository.GetListAsync(input.FilterText, input.EnterpriseName, input.Organization, input.ProductName, input.Allowance, input.APR, input.Period, input.ApplyStatus, input.GuaranteeMethod, input.ApplyTimeMin, input.ApplyTimeMax, input.PassedTimeMin, input.PassedTimeMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ApplyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Apply>, List<ApplyDto>>(items)
            };
        }

        public virtual async Task<ApplyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Apply, ApplyDto>(await _applyRepository.GetAsync(id));
        }

        [Authorize(FinancingPermissions.Applies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _applyRepository.DeleteAsync(id);
        }

        [Authorize(FinancingPermissions.Applies.Create)]
        public virtual async Task<ApplyDto> CreateAsync(ApplyCreateDto input)
        {
            _blockchainAppService.Apply(input);
            if (!string.IsNullOrEmpty(input.Allowance))
            {
                _blockchainAppService.AdvanceSetAllowance(input);
            }
            var apply = ObjectMapper.Map<ApplyCreateDto, Apply>(input);

            apply = await _applyRepository.InsertAsync(apply, autoSave: true);
            return ObjectMapper.Map<Apply, ApplyDto>(apply);
        }

        [Authorize(FinancingPermissions.Applies.Edit)]
        public virtual async Task<ApplyDto> UpdateAsync(Guid id, ApplyUpdateDto input)
        {
            if (input.ApplyStatus == ApplyStatus.线上初审通过)
            {
                _blockchainAppService.OnlineApprove(input);
            }

            if (input.ApplyStatus == ApplyStatus.线下审核通过)
            {
                _blockchainAppService.OfflineApprove(input);
            }

            if (input.ApplyStatus == ApplyStatus.完成)
            {
                _blockchainAppService.ApproveAllowance(input);
            }

            var apply = await _applyRepository.GetAsync(id);
            ObjectMapper.Map(input, apply);
            apply = await _applyRepository.UpdateAsync(apply, autoSave: true);
            return ObjectMapper.Map<Apply, ApplyDto>(apply);
        }
    }
}