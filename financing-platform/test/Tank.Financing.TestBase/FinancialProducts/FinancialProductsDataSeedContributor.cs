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
                id: Guid.Parse("72443cec-3247-4744-8aa2-99df9252b889"),
                availableDistricts: default,
                timeLimit: 1948,
                guaranteeMethod: default,
                creditCeiling: 1779863748,
                organization: "8a82c46b1eab41909eef660a0a26d8cf1da1e778e2c141d1ac4b533fc4f7544dabe43d884e5e488786daeeb17d0cb246bdff",
                appliedNumber: 698471889,
                aPR: 123444071,
                rating: 845255812,
                name: "1ab1bbd47b124b439726d035b357e23a23ea12c001ca4e66a015927791308d86"
            ));

            await _financialProductRepository.InsertAsync(new FinancialProduct
            (
                id: Guid.Parse("8d849af2-4a89-41f1-9f28-45677c193862"),
                availableDistricts: default,
                timeLimit: 2485,
                guaranteeMethod: default,
                creditCeiling: 1090984761,
                organization: "f7c14121d5884938af05e3a5eb5b24f9aad833c05d824a7e8148985fbbe2851ba71551c7d20e4ff6872cb7e3487ad9f776a6",
                appliedNumber: 1675076591,
                aPR: 110912364,
                rating: 1967417906,
                name: "edf9896b621e455ca6832b3f57471b8c67bd4aa930514678b311df79fc85906ddde12f4898c745"
            ));
        }
    }
}