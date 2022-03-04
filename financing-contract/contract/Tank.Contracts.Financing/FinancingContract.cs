using AElf.Contracts.Delegator;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;

namespace Tank.Contracts.Financing
{
    public partial class FinancingContract : FinancingContractContainer.FinancingContractBase
    {
        public override Empty Initialize(InitializeInput input)
        {
            State.DelegatorContract.Value = input.DelegatorContractAddress;
            PerformRegister(new ResetDelegatorInput
            {
                DelegatorContractAddress = input.DelegatorContractAddress,
                Admins = { input.Admins },
                FinancingOrganizations = { input.FinancingOrganizations },
                Enterprises = { input.Enterprises }
            });
            return new Empty();
        }

        public override Empty ResetDelegator(ResetDelegatorInput input)
        {
            AssertSenderIsOwner();
            PerformRegister(input);
            return new Empty();
        }

        public override Empty ChangeOwner(Address input)
        {
            AssertSenderIsOwner();
            State.Owner.Value = input;
            return new Empty();
        }

        private void PerformRegister(ResetDelegatorInput input)
        {
            State.Admins.Value = new AddressList { Value = { input.Admins } };
            State.Organizations.Value = new AddressList { Value = { input.FinancingOrganizations } };
            State.Enterprises.Value = new AddressList { Value = { input.Enterprises } };

            State.DelegatorContract.RegisterSenders.Send(new RegisterSendersInput
            {
                ScopeId = ScopeIdForEnterprise,
                AddressList = new AElf.Contracts.Delegator.AddressList { Value = { input.Enterprises } },
            });
            State.DelegatorContract.RegisterMethods.Send(new RegisterMethodsInput
            {
                ScopeId = ScopeIdForEnterprise,
                MethodNameList = new MethodList
                {
                    Value =
                    {
                        nameof(Certificate),
                        nameof(Complete),
                        nameof(Apply),
                        nameof(Cancel)
                    }
                }
            });

            State.DelegatorContract.RegisterSenders.Send(new RegisterSendersInput
            {
                ScopeId = ScopeIdForFinancingOrganization,
                AddressList = new AElf.Contracts.Delegator.AddressList
                    { Value = { input.FinancingOrganizations } },
            });
            State.DelegatorContract.RegisterMethods.Send(new RegisterMethodsInput
            {
                ScopeId = ScopeIdForFinancingOrganization,
                MethodNameList = new MethodList
                {
                    Value =
                    {
                        nameof(SetAllowance),
                        nameof(OnlineApprove),
                        nameof(OfflineApprove),
                        nameof(ApproveAllowance)
                    }
                }
            });

            State.DelegatorContract.RegisterSenders.Send(new RegisterSendersInput
            {
                ScopeId = ScopeIdForAdmin,
                AddressList = new AElf.Contracts.Delegator.AddressList
                    { Value = { input.Admins } },
            });
            State.DelegatorContract.RegisterMethods.Send(new RegisterMethodsInput
            {
                ScopeId = ScopeIdForAdmin,
                MethodNameList = new MethodList
                {
                    Value =
                    {
                        nameof(AddFinancingProduct),
                        nameof(RemoveFinancingProduct),
                        nameof(ConfirmCertificate),
                        nameof(RemoveEnterpriseInfo)
                    }
                }
            });
        }
    }
}