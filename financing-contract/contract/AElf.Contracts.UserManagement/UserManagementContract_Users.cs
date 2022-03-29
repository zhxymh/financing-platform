using AElf.Sdk.CSharp;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;

namespace AElf.Contracts.UserManagement
{
    public partial class UserManagementContract
    {
        public override Hash Register(UserInfo input)
        {
            AssertSenderIsDelegatorContract();

            var id = Context.TransactionId;
            var approval = new UserApproval
            {
                Id = id,
                UserName = input.UserName,
                Name = input.Name,
                Email = input.Email
            };
            
            State.UserApprovalMap.Set(id,approval);
            
            var userApprovalList = State.UserApprovalList.Value ?? new UserApprovalList();
            userApprovalList.Value.Add(approval.Clone());
            State.UserApprovalList.Value = userApprovalList;
            
            return id;
        }

        public override Empty ChangeUserInfo(UserInfo input)
        {
            AssertSenderIsDelegatorContract();

            var user = State.UserMap[input.UserName];
            if (user == null)
            {
                throw new AssertionException("User not exist.");
            }

            user.Name = input.Name;
            user.Email = input.Email;
            State.UserMap.Set(user.UserName,user);

            return new Empty();
        }
    }
}