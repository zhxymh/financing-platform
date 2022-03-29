namespace AElf.Contracts.UserManagement
{
    public partial class UserManagementContract
    {
        private const string ScopeIdForAdmin = "Admin";
        private const string ScopeIdForUser = "User";
        
        private void AssertSenderIsDelegatorContract()
        {
            State.DelegatorContract.ForwardCheck.Call(Context.OriginTransactionId);
        }

        private void AssertSenderIsOwner()
        {
            Assert(Context.Sender == State.Owner.Value, "No permission.");
        }
    }
}