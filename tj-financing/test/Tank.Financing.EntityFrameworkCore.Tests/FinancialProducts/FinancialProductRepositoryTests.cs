using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tank.Financing.FinancialProducts;
using Tank.Financing.EntityFrameworkCore;
using Xunit;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductRepositoryTests : FinancingEntityFrameworkCoreTestBase
    {
        private readonly IFinancialProductRepository _financialProductRepository;

        public FinancialProductRepositoryTests()
        {
            _financialProductRepository = GetRequiredService<IFinancialProductRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _financialProductRepository.GetListAsync(
                    productName: "3c7ed7f46dc745439812b67efbe48a440572acd828b84873b6",
                    organization: "d99c94ff2e554413aa8ecc65dd",
                    guaranteeMethod: default,
                    aPR: "406c91e12af142d0992e8dbd37631b785b68821edb154a9795550e349aa326141f317036951841d9a",
                    rating: "504778f825",
                    addFinancingProductTxId: "bbdf3416544045f19e41e25f914db"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("5a8b8948-245f-48e3-8360-9ce4b767aa93"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _financialProductRepository.GetCountAsync(
                    productName: "1ecebefaa14f429b9f20f85ffa7ae8fad7830d48e9fb4bf38360f607d45c55ca27d1",
                    organization: "824e6490447e485ea5594f6b7186ba37eb908a5ce2224f7bb102e17e8bfb55bcc2e2119148894278b05b3bb10bb",
                    guaranteeMethod: default,
                    aPR: "b9c394b4c15f4316b537d5b6d1cc528c6afa20f8aa4848e1b9f070bcd6c61e948822be267632413b82fe87385eb985b",
                    rating: "3b8532d7a5904a208547778",
                    addFinancingProductTxId: "72ac6f0b7573"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}