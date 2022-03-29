using System.Collections.Generic;
using System.IO;
using AElf.Boilerplate.TestBase;
using AElf.Contracts.Delegator;
using AElf.ContractTestBase;
using AElf.Kernel.SmartContract.Application;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace AElf.Contracts.UserManagement
{
    [DependsOn(typeof(MainChainDAppContractTestModule))]
    public class UserManagementContractTestModule : MainChainDAppContractTestModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IContractInitializationProvider, UserManagementContractInitializationProvider>();
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            var contractCodeProvider = context.ServiceProvider.GetService<IContractCodeProvider>();
            var contractDllLocation = typeof(UserManagementContract).Assembly.Location;
            var contractCodes = new Dictionary<string, byte[]>(contractCodeProvider.Codes)
            {
                {
                    new UserManagementContractInitializationProvider().ContractCodeName,
                    File.ReadAllBytes(contractDllLocation)
                },
                {
                    new DelegatorContractInitializationProvider().ContractCodeName,
                    File.ReadAllBytes(typeof(DelegatorContract).Assembly.Location)
                }
            };
            contractCodeProvider.Codes = contractCodes;
        }
    }
}