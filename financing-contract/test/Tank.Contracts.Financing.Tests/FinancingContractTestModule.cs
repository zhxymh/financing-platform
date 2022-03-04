using System.Collections.Generic;
using System.IO;
using AElf.Boilerplate.TestBase;
using AElf.Contracts.Delegator;
using AElf.ContractTestBase;
using AElf.Kernel.SmartContract.Application;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Tank.Contracts.Financing
{
    [DependsOn(typeof(MainChainDAppContractTestModule))]
    public class FinancingContractTestModule : MainChainDAppContractTestModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IContractInitializationProvider, FinancingContractInitializationProvider>();
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            var contractCodeProvider = context.ServiceProvider.GetService<IContractCodeProvider>();
            var contractDllLocation = typeof(FinancingContract).Assembly.Location;
            var contractCodes = new Dictionary<string, byte[]>(contractCodeProvider.Codes)
            {
                {
                    new FinancingContractInitializationProvider().ContractCodeName,
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