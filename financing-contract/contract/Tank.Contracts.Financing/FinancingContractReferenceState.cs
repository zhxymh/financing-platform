using AElf.Contracts.Delegator;

namespace Tank.Contracts.Financing
{
    public partial class FinancingContractState
    {
        internal DelegatorContractContainer.DelegatorContractReferenceState DelegatorContract { get; set; }
    }
}