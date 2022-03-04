using AElf.Boilerplate.TestBase;
using AElf.Contracts.Delegator;
using AElf.Cryptography.ECDSA;
using AElf.Types;

namespace Tank.Contracts.Financing
{
    public class FinancingContractTestBase : DAppContractTestBase<FinancingContractTestModule>
    {
        internal Address DelegatorContractAddress =>
            SystemContractAddresses[DelegatorSmartContractAddressNameProvider.Name];

        internal FinancingContractContainer.FinancingContractStub GetFinancingContractStub(ECKeyPair senderKeyPair)
        {
            return GetTester<FinancingContractContainer.FinancingContractStub>(DAppContractAddress, senderKeyPair);
        }
        
        internal DelegatorContractContainer.DelegatorContractStub GetDelegatorContractStub(ECKeyPair senderKeyPair)
        {
            return GetTester<DelegatorContractContainer.DelegatorContractStub>(DelegatorContractAddress, senderKeyPair);
        }
    }
}