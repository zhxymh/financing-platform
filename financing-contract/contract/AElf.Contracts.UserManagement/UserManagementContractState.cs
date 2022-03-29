using AElf.Contracts.Delegator;
using AElf.Sdk.CSharp.State;
using AElf.Types;

namespace AElf.Contracts.UserManagement
{
    public class UserManagementContractState : ContractState
    {
        public BoolState Initialized { get; set; }
        public SingletonState<Address> Owner { get; set; }

        public SingletonState<AddressList> AdminDelegators { get; set; }
        
        public SingletonState<AddressList> UserDelegators { get; set; }

        public MappedState<Hash, UserApproval> UserApprovalMap { get; set; }
        
        public SingletonState<UserApprovalList> UserApprovalList { get; set; }
        
        public MappedState<string, UserInfo> UserMap { get; set; }
        
        internal DelegatorContractContainer.DelegatorContractReferenceState DelegatorContract { get; set; }
    }
}