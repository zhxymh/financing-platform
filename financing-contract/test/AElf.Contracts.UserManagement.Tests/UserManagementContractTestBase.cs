using AElf.Boilerplate.TestBase;
using AElf.Contracts.Delegator;
using AElf.Cryptography.ECDSA;
using AElf.Types;

namespace AElf.Contracts.UserManagement
{
    public class UserManagementContractTestBase : DAppContractTestBase<UserManagementContractTestModule>
    {
        internal Address DelegatorContractAddress =>
            SystemContractAddresses[DelegatorSmartContractAddressNameProvider.Name];

        internal UserManagementContractContainer.UserManagementContractStub GetUserManagementContractStub(ECKeyPair senderKeyPair)
        {
            return GetTester<UserManagementContractContainer.UserManagementContractStub>(DAppContractAddress, senderKeyPair);
        }
        
        internal DelegatorContractContainer.DelegatorContractStub GetDelegatorContractStub(ECKeyPair senderKeyPair)
        {
            return GetTester<DelegatorContractContainer.DelegatorContractStub>(DelegatorContractAddress, senderKeyPair);
        }
    }
}