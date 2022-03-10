using System;
using AElf;
using AElf.EventHandler;
using AElf.Types;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using AElf.Contracts.Delegator;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tank.Contracts.Financing;
using Tank.Financing.Applies;
using Tank.Financing.EnterpriseDetails;
using Tank.Financing.Enterprises;
using Tank.Financing.FinancialProducts;

namespace Tank.Financing;

public interface IBlockchainAppService
{
    void AddFinancingProduct(FinancialProductCreateDto input);
    void Certificate(EnterpriseCreateDto input);
    void ConfirmCertificate(EnterpriseUpdateDto input);
    void Complete(EnterpriseDetailCreateDto input);
    void Apply(ApplyCreateDto input);
    void AdvanceSetAllowance(ApplyCreateDto input);
    void SetAllowance(ApplyUpdateDto input);
    void OnlineApprove(ApplyUpdateDto input);
    void OfflineApprove(ApplyUpdateDto input);
    void ApproveAllowance(ApplyUpdateDto input);
}

public class BlockchainAppService : IBlockchainAppService, ITransientDependency
{
    private readonly NodeManager _nodeManager;

    public BlockchainOptions Options { get; }

    public ILogger<BlockchainAppService> Logger { get; set; }

    public BlockchainAppService(IOptionsSnapshot<BlockchainOptions> options)
    {
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

        InitializeFinancingContract(Address.FromBase58(Options.OwnerAddress));
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

    public void AddFinancingProduct(FinancialProductCreateDto input)
    {
        ForwardContract(input.Organization, FinancingConsts.ScopeIdForAdmin,
            nameof(AddFinancingProduct),
            new AddFinancingProductInput
            {
                ProductName = input.ProductName,
                Organization = input.Organization,
                Url = "https://test.com"
            });
    }

    public void ConfirmCertificate(EnterpriseUpdateDto input)
    {
        ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForAdmin, nameof(ConfirmCertificate),
            new ConfirmCertificateInput
            {
                EnterpriseName = input.EnterpriseName,
                IsConfirm = true
            });
    }

    public void Certificate(EnterpriseCreateDto input)
    {
        ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForEnterprise, nameof(Certificate),
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

    public void Complete(EnterpriseDetailCreateDto input)
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

    public void Apply(ApplyCreateDto input)
    {
        ForwardContract(input.EnterpriseName, FinancingConsts.ScopeIdForEnterprise, nameof(Apply),
            new ApplyInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization
            });
    }

    public void AdvanceSetAllowance(ApplyCreateDto input)
    {
        ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(SetAllowance),
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

    public void SetAllowance(ApplyUpdateDto input)
    {
        ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(SetAllowance),
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

    public void OnlineApprove(ApplyUpdateDto input)
    {
        ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(OnlineApprove),
            new ApproveInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization
            });
    }

    public void OfflineApprove(ApplyUpdateDto input)
    {
        ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(OfflineApprove),
            new ApproveInput
            {
                EnterpriseName = input.EnterpriseName,
                ProductName = input.ProductName,
                Organization = input.Organization
            });
    }

    public void ApproveAllowance(ApplyUpdateDto input)
    {
        ForwardContract(input.Organization, FinancingConsts.ScopeIdForFinancingOrganization, nameof(ApproveAllowance),
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

    private void ForwardContract(string fromId, string scopeId, string methodName,
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
        Logger.LogInformation($"[Forward]{methodName}: {result.TransactionId}");
        if (result.Status != "MINED")
        {
            throw new TransactionFailedException($"Transaction execution failed: {result.Error}");
        }
    }
}