using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AElf.Contracts.Delegator;
using AElf.EventHandler;
using AElf.Types;
using Google.Protobuf;
using Microsoft.AspNetCore.Authorization;
using Tank.Contracts.Financing;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Tank.Financing.Permissions;

namespace Tank.Financing.FinancialProducts
{

    [Authorize(FinancingPermissions.FinancialProducts.Default)]
    public class FinancialProductsAppService : ApplicationService, IFinancialProductsAppService
    {
        private readonly IFinancialProductRepository _financialProductRepository;

        private readonly NodeManager _nodeManager = new(FinancingConsts.DefaultNodeUrl,
            FinancingConsts.DefaultSenderAddress,
            FinancingConsts.DefaultSenderPassword);

        public FinancialProductsAppService(IFinancialProductRepository financialProductRepository)
        {
            _financialProductRepository = financialProductRepository;
        }

        public virtual async Task<PagedResultDto<FinancialProductDto>> GetListAsync(GetFinancialProductsInput input)
        {
            var totalCount = await _financialProductRepository.GetCountAsync(input.FilterText, input.TimeLimitMin,
                input.TimeLimitMax, input.GuaranteeMethod, input.CreditCeiling, input.Organization,
                input.AppliedNumberMin, input.AppliedNumberMax, input.APR, input.Rating, input.Name);
            var items = await _financialProductRepository.GetListAsync(input.FilterText, input.TimeLimitMin,
                input.TimeLimitMax, input.GuaranteeMethod, input.CreditCeiling, input.Organization,
                input.AppliedNumberMin, input.AppliedNumberMax, input.APR, input.Rating, input.Name, input.Sorting,
                input.MaxResultCount, input.SkipCount);

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

        [Authorize(FinancingPermissions.FinancialProducts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _financialProductRepository.DeleteAsync(id);
        }

        [Authorize(FinancingPermissions.FinancialProducts.Create)]
        public virtual async Task<FinancialProductDto> CreateAsync(FinancialProductCreateDto input)
        {
            var owner = Address.FromBase58(FinancingConsts.DefaultSenderAddress);

            //InitializeFinancingContract(owner);

            ForwardContract(input.Organization, FinancingConsts.ScopeIdForAdmin,
                "AddFinancingProduct",
                new AddFinancingProductInput
                {
                    ProductName = input.Name,
                    Organization = input.Organization,
                    Url = "https://test.com"
                });

            var financialProduct = ObjectMapper.Map<FinancialProductCreateDto, FinancialProduct>(input);
            financialProduct = await _financialProductRepository.InsertAsync(financialProduct, true);
            return ObjectMapper.Map<FinancialProduct, FinancialProductDto>(financialProduct);
        }

        private void InitializeFinancingContract(Address owner)
        {
            var txId = _nodeManager.SendTransaction(FinancingConsts.DefaultSenderAddress,
                FinancingConsts.DefaultFinancingContractAddress,
                "Initialize",
                new InitializeInput
                {
                    DelegatorContractAddress = Address.FromBase58(FinancingConsts.DefaultDelegatorContractAddress),
                    Owner = owner,
                    Admins = { owner },
                    FinancingOrganizations = { owner },
                    Enterprises = { owner },
                });
            var result = _nodeManager.CheckTransactionResult(txId);
            if (result.Status != "MINED")
            {
                throw new TransactionFailedException($"Transaction execution failed: {result.Error}");
            }
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