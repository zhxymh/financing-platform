using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using AElf;
using AElf.Contracts.Delegator;
using AElf.EventHandler;
using AElf.Kernel;
using AElf.Types;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Tank.Contracts.Financing;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Tank.Financing.Permissions;
using Tank.Financing.Enterprises;

namespace Tank.Financing.Enterprises
{

    [Authorize(FinancingPermissions.Enterprises.Default)]
    public class EnterprisesAppService : ApplicationService, IEnterprisesAppService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;

        private readonly NodeManager _nodeManager = new(FinancingConsts.DefaultNodeUrl,
            FinancingConsts.DefaultSenderAddress,
            FinancingConsts.DefaultSenderPassword);

        public EnterprisesAppService(IEnterpriseRepository enterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository;
        }

        public virtual async Task<PagedResultDto<EnterpriseDto>> GetListAsync(GetEnterprisesInput input)
        {
            var totalCount = await _enterpriseRepository.GetCountAsync(input.FilterText, input.EnterpriseName, input.ArtificialPerson, input.EstablishedTime, input.DueTime, input.CreditCode, input.ArtificialPersonId, input.RegisteredCapital, input.PhoneNumber, input.CertPhotoPath, input.IdPhotoPath1, input.IdPhotoPath2, input.CertificateStatus);
            var items = await _enterpriseRepository.GetListAsync(input.FilterText, input.EnterpriseName, input.ArtificialPerson, input.EstablishedTime, input.DueTime, input.CreditCode, input.ArtificialPersonId, input.RegisteredCapital, input.PhoneNumber, input.CertPhotoPath, input.IdPhotoPath1, input.IdPhotoPath2, input.CertificateStatus, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EnterpriseDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Enterprise>, List<EnterpriseDto>>(items)
            };
        }

        public virtual async Task<EnterpriseDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Enterprise, EnterpriseDto>(await _enterpriseRepository.GetAsync(id));
        }

        [Authorize(FinancingPermissions.Enterprises.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _enterpriseRepository.DeleteAsync(id);
        }

        [Authorize(FinancingPermissions.Enterprises.Create)]
        public virtual async Task<EnterpriseDto> CreateAsync(EnterpriseCreateDto input)
        {
            Certificate(input);
            var enterprise = ObjectMapper.Map<EnterpriseCreateDto, Enterprise>(input);
            enterprise = await _enterpriseRepository.InsertAsync(enterprise, autoSave: true);
            return ObjectMapper.Map<Enterprise, EnterpriseDto>(enterprise);
        }

        private void Certificate(EnterpriseCreateDto input)
        {
            ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForEnterprise, nameof(Certificate),
                new EnterpriseBasicInfo
                {
                    Name = input.EnterpriseName,
                    ArtificialPerson = input.ArtificialPerson,
                    ArtificialPersonId = HashHelper.ComputeFrom(input.ArtificialPersonId),
                    CreditCode = input.CreditCode,
                    EstablishedTime = "2020-02-02",
                    PhoneNumber = HashHelper.ComputeFrom(input.PhoneNumber),
                    RegisteredCapital = input.RegisteredCapital,
                });
        }

        [Authorize(FinancingPermissions.Enterprises.Edit)]
        public virtual async Task<EnterpriseDto> UpdateAsync(Guid id, EnterpriseUpdateDto input)
        {
            if (input.CertificateStatus == CertificateStatus.通过)
            {
                ConfirmCertificate(input);
            }
            var enterprise = await _enterpriseRepository.GetAsync(id);
            ObjectMapper.Map(input, enterprise);
            enterprise = await _enterpriseRepository.UpdateAsync(enterprise, autoSave: true);
            return ObjectMapper.Map<Enterprise, EnterpriseDto>(enterprise);
        }

        private void ConfirmCertificate(EnterpriseUpdateDto input)
        {
            ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForAdmin, nameof(ConfirmCertificate),
                new ConfirmCertificateInput
                {
                    EnterpriseName = input.EnterpriseName,
                    IsConfirm = true
                });
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