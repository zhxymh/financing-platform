using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using AElf.Contracts.Delegator;
using AElf.EventHandler;
using AElf.Types;
using Google.Protobuf;
using Microsoft.AspNetCore.Authorization;
using Tank.Contracts.Financing;
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
        
        private readonly NodeManager _nodeManager = new(FinancingConsts.DefaultNodeUrl,
            FinancingConsts.DefaultSenderAddress,
            FinancingConsts.DefaultSenderPassword);

        public EnterpriseDetailsAppService(IEnterpriseDetailRepository enterpriseDetailRepository)
        {
            _enterpriseDetailRepository = enterpriseDetailRepository;
        }

        public virtual async Task<PagedResultDto<EnterpriseDetailDto>> GetListAsync(GetEnterpriseDetailsInput input)
        {
            var totalCount = await _enterpriseDetailRepository.GetCountAsync(input.FilterText, input.EnterpriseName, input.TotalAssets, input.Income, input.EnterpriseType, input.StaffNumberMin, input.StaffNumberMax, input.Industry, input.Location, input.RegisteredAddress, input.BusinessAddress, input.BusinessScope, input.Description);
            var items = await _enterpriseDetailRepository.GetListAsync(input.FilterText, input.EnterpriseName, input.TotalAssets, input.Income, input.EnterpriseType, input.StaffNumberMin, input.StaffNumberMax, input.Industry, input.Location, input.RegisteredAddress, input.BusinessAddress, input.BusinessScope, input.Description, input.Sorting, input.MaxResultCount, input.SkipCount);

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
            Complete(input);
            var enterpriseDetail = ObjectMapper.Map<EnterpriseDetailCreateDto, EnterpriseDetail>(input);
            enterpriseDetail = await _enterpriseDetailRepository.InsertAsync(enterpriseDetail, autoSave: true);
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(enterpriseDetail);
        }

        private void Complete(EnterpriseDetailCreateDto input)
        {
            ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForEnterprise, "Complete",
                new EnterpriseFurtherInfo
                {
                    Name = input.EnterpriseName,
                    BusinessAddress = input.BusinessAddress,
                    BusinessScope = input.BusinessScope,
                    Description = input.Description,
                    StaffNumber = input.StaffNumber,
                    EnterpriseType = input.EnterpriseType,
                    Income = input.Income,
                    Industry = input.Industry,
                    RegisteredAddress = input.RegisteredAddress,
                    TotalAssets = input.TotalAssets
                });
        }

        [Authorize(FinancingPermissions.EnterpriseDetails.Edit)]
        public virtual async Task<EnterpriseDetailDto> UpdateAsync(Guid id, EnterpriseDetailUpdateDto input)
        {
            var enterpriseDetail = await _enterpriseDetailRepository.GetAsync(id);
            ObjectMapper.Map(input, enterpriseDetail);
            enterpriseDetail = await _enterpriseDetailRepository.UpdateAsync(enterpriseDetail, autoSave: true);
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(enterpriseDetail);
        }
        
        private void ForwardContract(string fromId, string scopeId, string methodName,
            IMessage param)
        {
            var txId = _nodeManager.SendTransaction(FinancingConsts.DefaultSenderAddress,
                FinancingConsts.DefaultDelegatorContractAddress,
                "Forward",
                new ForwardInput
                {
                    FromId = fromId,
                    ToAddress = Address.FromBase58(FinancingConsts.DefaultFinancingContractAddress),
                    MethodName = methodName,
                    Parameter = param.ToByteString(),
                    ScopeId = scopeId
                });
            var result = _nodeManager.CheckTransactionResult(txId);
            if (result.Status != "MINED")
            {
                throw new TransactionFailedException($"Transaction execution failed: {result.Error}");
            }
        }
    }
}