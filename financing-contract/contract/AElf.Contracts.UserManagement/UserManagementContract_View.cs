using Google.Protobuf.WellKnownTypes;

namespace AElf.Contracts.UserManagement
{
    public partial class UserManagementContract
    {
        public override AddressList GetAdminDelegators(Empty input)
        {
            return State.AdminDelegators.Value;
        }

        public override AddressList GetUserDelegators(Empty input)
        {
            return State.UserDelegators.Value;
        }

        public override UserApprovalList GetApprovalList(Empty input)
        {
            return State.UserApprovalList.Value;
        }

        public override UserInfo GetUser(StringValue input)
        {
            return State.UserMap[input.Value];
        }
    }
}