using AElf;
using AElf.Types;

namespace Tank.Contracts.Financing
{
    public partial class FinancingContract
    {
        private void AssertSenderIsDelegatorContract()
        {
            State.DelegatorContract.ForwardCheck.Send(Context.OriginTransactionId);
        }

        private void AssertSenderIsOwner()
        {
            Assert(Context.Sender == State.Owner.Value, "Sender must be the contract owner.");
        }

        private Hash CalculateProductHash(string organization, string productName)
        {
            return HashHelper.ComputeFrom($"{organization}{productName}");
        }
    }
}