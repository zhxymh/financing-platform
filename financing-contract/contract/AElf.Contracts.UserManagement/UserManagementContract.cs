using AElf.Contracts.Delegator;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;

namespace AElf.Contracts.UserManagement
{
    public partial class UserManagementContract : UserManagementContractContainer.UserManagementContractBase
    {
        public override Empty Initialize(InitializeInput input)
        {
            Assert(!State.Initialized.Value, "Already initialized.");
            
            State.Initialized.Value = true;
            State.Owner.Value = input.Owner;

            return new Empty();
        }

        public override Empty SetDelegatorContract(Address input)
        {
            AssertSenderIsOwner();

            State.DelegatorContract.Value = input;
            return new Empty();
        }

        public override Empty SetAdminDelegators(AddressList input)
        {
            AssertSenderIsOwner();

            State.AdminDelegators.Value = input;
            
            State.DelegatorContract.RegisterSenders.Send(new RegisterSendersInput
            {
                ScopeId = ScopeIdForAdmin,
                AddressList = new AElf.Contracts.Delegator.AddressList { Value = { input.Value } },
            });
            State.DelegatorContract.RegisterMethods.Send(new RegisterMethodsInput
            {
                ScopeId = ScopeIdForAdmin,
                MethodNameList = new MethodList
                {
                    Value =
                    {
                        nameof(Approve),
                        nameof(Reject)
                    }
                }
            });
            
            return new Empty();
        }

        public override Empty SetUserDelegators(AddressList input)
        {
            AssertSenderIsOwner();

            State.UserDelegators.Value = input;
            
            State.DelegatorContract.RegisterSenders.Send(new RegisterSendersInput
            {
                ScopeId = ScopeIdForUser,
                AddressList = new AElf.Contracts.Delegator.AddressList { Value = { input.Value } },
            });
            State.DelegatorContract.RegisterMethods.Send(new RegisterMethodsInput
            {
                ScopeId = ScopeIdForUser,
                MethodNameList = new MethodList
                {
                    Value =
                    {
                        nameof(Register),
                        nameof(ChangeUserInfo)
                    }
                }
            });
            
            return new Empty();
        }
    }
}