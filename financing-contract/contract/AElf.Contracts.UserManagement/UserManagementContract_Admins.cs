using AElf.Sdk.CSharp;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;

namespace AElf.Contracts.UserManagement
{
    public partial class UserManagementContract
    {
        public override BoolValue Approve(Hash input)
        {
            AssertSenderIsDelegatorContract();

            var approval = State.UserApprovalMap[input];
            if (approval == null)
            {
                throw new AssertionException("User approval not exist.");
            }

            RemoveUserApproval(input);
            
            var user = State.UserMap[approval.UserName];
            if (user != null)
            {
                return new BoolValue
                {
                    Value = false
                };
            }
            
            State.UserMap.Set(approval.UserName,new UserInfo
            {
                UserName = approval.UserName,
                Name = approval.Name,
                Email = approval.Email
            });

            return new BoolValue
            {
                Value = true
            };
        }

        public override Empty Reject(Hash input)
        {
            AssertSenderIsDelegatorContract();

            var approval = State.UserApprovalMap[input];
            if (approval == null)
            {
                throw new AssertionException("User approval not exist.");
            }

            RemoveUserApproval(input);
            
            return new Empty();
        }

        private void RemoveUserApproval(Hash id)
        {
            var approval = State.UserApprovalMap[id];
            var approvalList = State.UserApprovalList.Value;
            approvalList.Value.Remove(approval);
            State.UserApprovalList.Value = approvalList;
            State.UserApprovalMap.Remove(id);
        }
    }
}