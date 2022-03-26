using System;
using System.Collections.Generic;
using AElf;
using AElf.EventHandler;
using AElf.Types;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using AElf.Contracts.Delegator;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Tank.Contracts.Financing;
using Tank.Financing.Applies;
using Tank.Financing.EnterpriseDetails;
using Tank.Financing.Enterprises;
using Tank.Financing.FinancialProducts;
using Volo.Abp.ObjectMapping;

namespace Tank.Financing;

public interface IBlockchainAppService
{
    string AddFinancingProduct(FinancialProductCreateDto input);
    string Certificate(EnterpriseCreateDto input);
    string ConfirmCertificate(EnterpriseUpdateDto input);
    string Complete(EnterpriseDetail detail, Dictionary<string,string> extraInfo);
    string Apply(ApplyCreateDto input);
    string AdvanceSetAllowance(ApplyCreateDto input);
    string SetAllowance(ApplyUpdateDto input);
    string OnlineApprove(ApplyUpdateDto input);
    string OfflineApprove(ApplyUpdateDto input);
    string ApproveAllowance(ApplyUpdateDto input);
}

public class BlockchainAppService : IBlockchainAppService, ITransientDependency
{
    private readonly NodeManager _nodeManager;

    private readonly IObjectMapper _objectMapper;

    public BlockchainOptions Options { get; }

    public ILogger<BlockchainAppService> Logger { get; set; }

    public BlockchainAppService(IOptionsSnapshot<BlockchainOptions> options, IObjectMapper objectMapper)
    {
        _objectMapper = objectMapper;
        Logger = NullLogger<BlockchainAppService>.Instance;
        Options = options.Value;
        try
        {
            _nodeManager = new NodeManager(Options.Endpoint,
                Options.OwnerAddress,
                Options.OwnerPassword);
        }
        catch (Exception e)
        {
            Logger.LogError($"Failed to connect to blockchain node. {e.Message}");
        }

        //InitializeFinancingContract(Address.FromBase58(Options.OwnerAddress));
    }

    private void InitializeFinancingContract(Address owner)
    {
        var txId = _nodeManager.SendTransaction(Options.OwnerAddress,
            Options.FinancingContractAddress,
            "Initialize",
            new InitializeInput
            {
                DelegatorContractAddress = Address.FromBase58(Options.DelegatorContractAddress),
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

    public string AddFinancingProduct(FinancialProductCreateDto input)
    {
        return ForwardContract(input.Organization, FinancingConsts.ScopeIdForAdmin,
            nameof(AddFinancingProduct),
            new AddFinancingProductInput
            {
                ProductName = input.ProductName,
                Organization = input.Organization,
                Url = "https://test.com"
            });
    }

    public string ConfirmCertificate(EnterpriseUpdateDto input)
    {
        return ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForAdmin, nameof(ConfirmCertificate),
            new ConfirmCertificateInput
            {
                EnterpriseName = input.EnterpriseName,
                IsConfirm = true
            });
    }

    public string Certificate(EnterpriseCreateDto input)
    {
        return ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForEnterprise, nameof(Certificate),
            new EnterpriseBasicInfo
            {
                Name = input.EnterpriseName,
                ArtificialPerson = input.ArtificialPerson,
                ArtificialPersonId = HashHelper.ComputeFrom(input.ArtificialPersonId),
                CreditCode = input.CreditCode,
                EstablishedTime = input.EstablishedTime ?? 0,
                DueTime = input.DueTime,
                PhoneNumber = HashHelper.ComputeFrom(input.PhoneNumber),
                RegisteredCapital = input.RegisteredCapital,
            });
    }

    public string Complete(EnterpriseDetail detail, Dictionary<string, string> extraInfo)
    {
        var enterpriseFurtherInfo = _objectMapper.Map<EnterpriseDetail, EnterpriseFurtherInfo>(detail);
        enterpriseFurtherInfo.ExtraInfo =
            extraInfo != null ? HashHelper.ComputeFrom(JsonConvert.SerializeObject(extraInfo)) : null;
        return ForwardContract(detail.EnterpriseName, FinancingConsts.ScopeIdForEnterprise, "Complete",
            enterpriseFurtherInfo);
    }

    public string Apply(ApplyCreateDto input)
    {
        return ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForEnterprise, nameof(Apply),
            new ApplyInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization
            });
    }

    public string AdvanceSetAllowance(ApplyCreateDto input)
    {
        return ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(SetAllowance),
            new SetAllowanceInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization,
                Allowance = input.Allowance,
                Apr = input.APR,
                GuaranteeMethod = input.GuaranteeMethod.ToString(),
                Period = input.Period
            });
    }

    public string SetAllowance(ApplyUpdateDto input)
    {
        return ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(SetAllowance),
            new SetAllowanceInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization,
                Allowance = input.Allowance,
                Apr = input.APR,
                GuaranteeMethod = input.GuaranteeMethod.ToString(),
                Period = input.Period
            });
    }

    public string OnlineApprove(ApplyUpdateDto input)
    {
        return ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(OnlineApprove),
            new ApproveInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization
            });
    }

    public string OfflineApprove(ApplyUpdateDto input)
    {
        return ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(OfflineApprove),
            new ApproveInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization
            });
    }

    public string ApproveAllowance(ApplyUpdateDto input)
    {
        return ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(ApproveAllowance),
            new ApproveAllowanceInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization,
                Allowance = input.Allowance,
                Apr = input.APR,
                GuaranteeMethod = input.GuaranteeMethod.ToString(),
                Period = input.Period
            });
    }

    private string ForwardContract(string fromId, string scopeId, string methodName,
        IMessage param)
    {
        var txId = _nodeManager.SendTransaction(Options.OwnerAddress,
            Options.DelegatorContractAddress,
            "Forward",
            new ForwardInput
            {
                FromId = fromId,
                ToAddress = Address.FromBase58(Options.FinancingContractAddress),
                MethodName = methodName,
                Parameter = param.ToByteString(),
                ScopeId = scopeId
            });
        var result = _nodeManager.CheckTransactionResult(txId);
        Logger.LogInformation($"[Forward]{methodName}: {txId}");
        if (result.Status != "MINED")
        {
            throw new TransactionFailedException($"转发的 {methodName} 交易执行失败: {result.Error}");
        }

        return result.TransactionId;
    }
}