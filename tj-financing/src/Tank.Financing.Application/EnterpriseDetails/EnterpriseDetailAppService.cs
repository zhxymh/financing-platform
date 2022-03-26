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
using Tank.Financing.EnterpriseDetails;

namespace Tank.Financing.EnterpriseDetails
{

    [Authorize(FinancingPermissions.EnterpriseDetails.Default)]
    public class EnterpriseDetailsAppService : ApplicationService, IEnterpriseDetailsAppService
    {
        private readonly IEnterpriseDetailRepository _enterpriseDetailRepository;
        private readonly IBlockchainAppService _blockchainAppService;

        public EnterpriseDetailsAppService(IEnterpriseDetailRepository enterpriseDetailRepository, IBlockchainAppService blockchainAppService)
        {
            _enterpriseDetailRepository = enterpriseDetailRepository;
            _blockchainAppService = blockchainAppService;
        }

        public virtual async Task<PagedResultDto<EnterpriseDetailDto>> GetListAsync(GetEnterpriseDetailsInput input)
        {
            var totalCount = await _enterpriseDetailRepository.GetCountAsync(input.FilterText, input.EnterpriseName,
                input.TotalAssets, input.Income, input.EnterpriseType, input.StaffNumberMin, input.StaffNumberMax,
                input.Industry, input.Location, input.RegisteredAddress, input.BusinessAddress, input.BusinessScope,
                input.Description, input.CompleteTxId, input.CommitUserName);
            var items = await _enterpriseDetailRepository.GetListAsync(input.FilterText, input.EnterpriseName,
                input.TotalAssets, input.Income, input.EnterpriseType, input.StaffNumberMin, input.StaffNumberMax,
                input.Industry, input.Location, input.RegisteredAddress, input.BusinessAddress, input.BusinessScope,
                input.Description, input.CompleteTxId, input.CommitUserName, input.Sorting, input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<EnterpriseDetailDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EnterpriseDetail>, List<EnterpriseDetailDto>>(items)
            };
        }

        public virtual async Task<EnterpriseDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(await _enterpriseDetailRepository.GetAsync(id));
        }

        [Authorize(FinancingPermissions.EnterpriseDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _enterpriseDetailRepository.DeleteAsync(id);
        }

        [Authorize(FinancingPermissions.EnterpriseDetails.Create)]
        public virtual async Task<EnterpriseDetailDto> CreateAsync(EnterpriseDetailCreateDto input)
        {
            // Enterprise cannot dup.
            var maybeSameEnterpriseNameList = await GetListAsync(new GetEnterpriseDetailsInput
            {
                EnterpriseName = input.EnterpriseName
            });
            if (maybeSameEnterpriseNameList.TotalCount > 0)
            {
                throw new UserFriendlyException("企业名称已经存在");
            }

            //input.CompleteTxId = _blockchainAppService.Complete(input);
            var enterpriseDetail = ObjectMapper.Map<EnterpriseDetailCreateDto, EnterpriseDetail>(input);
        
           
            enterpriseDetail = await _enterpriseDetailRepository.InsertAsync(enterpriseDetail, autoSave: true);
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(enterpriseDetail);
        }

        [Authorize(FinancingPermissions.EnterpriseDetails.Edit)]
        public virtual async Task<EnterpriseDetailDto> UpdateAsync(Guid id, EnterpriseDetailUpdateDto input)
        {
            input.CompleteTxId = _blockchainAppService.Complete(new EnterpriseDetailCreateDto
            {
                /*
                BusinessAddress = input.BusinessAddress,
                BusinessScope = input.BusinessScope,
                Description = input.Description,
                EnterpriseName = input.EnterpriseName,
                EnterpriseType = input.EnterpriseType,
                Income = input.Income,
                Industry = input.Industry,
                Location = input.Location,
                RegisteredAddress = input.RegisteredAddress,
                StaffNumber = input.StaffNumber,
                TotalAssets = input.TotalAssets,
                CommitUserName = input.CommitUserName
                */
            });
            var enterpriseDetail = await _enterpriseDetailRepository.GetAsync(id);
            ObjectMapper.Map(input, enterpriseDetail);
            enterpriseDetail = await _enterpriseDetailRepository.UpdateAsync(enterpriseDetail, autoSave: true);
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(enterpriseDetail);
        }
    }
}