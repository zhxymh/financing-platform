using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Tank.Financing.FinancialProducts;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IFinancialProductRepository _financialProductRepository;

        public FinancialProductsDataSeedContributor(IFinancialProductRepository financialProductRepository)
        {
            _financialProductRepository = financialProductRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _financialProductRepository.InsertAsync(new FinancialProduct
            (
                id: Guid.Parse("5a8b8948-245f-48e3-8360-9ce4b767aa93"),
                productName: "3c7ed7f46dc745439812b67efbe48a440572acd828b84873b6",
                organization: "d99c94ff2e554413aa8ecc65dd",
                period: 1281114360,
                guaranteeMethod: default,
                appliedNumber: 2139769840,
                aPR: "406c91e12af142d0992e8dbd37631b785b68821edb154a9795550e349aa326141f317036951841d9a",
                rating: "504778f825",
                creditCeiling: 45305367,
                addFinancingProductTxId: "bbdf3416544045f19e41e25f914db"
            ));

            await _financialProductRepository.InsertAsync(new FinancialProduct
            (
                id: Guid.Parse("732e3eb9-65fe-4585-b8d0-39b286409087"),
                productName: "1ecebefaa14f429b9f20f85ffa7ae8fad7830d48e9fb4bf38360f607d45c55ca27d1",
                organization: "824e6490447e485ea5594f6b7186ba37eb908a5ce2224f7bb102e17e8bfb55bcc2e2119148894278b05b3bb10bb",
                period: 1068277487,
                guaranteeMethod: default,
                appliedNumber: 1581029495,
                aPR: "b9c394b4c15f4316b537d5b6d1cc528c6afa20f8aa4848e1b9f070bcd6c61e948822be267632413b82fe87385eb985b",
                rating: "3b8532d7a5904a208547778",
                creditCeiling: 2132818227,
                addFinancingProductTxId: "72ac6f0b7573"
            ));
        }
    }
}