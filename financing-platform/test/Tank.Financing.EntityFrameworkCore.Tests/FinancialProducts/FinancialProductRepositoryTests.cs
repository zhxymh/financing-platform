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
                    availableDistricts: default,
                    guaranteeMethod: default,
                    organization: "8a82c46b1eab41909eef660a0a26d8cf1da1e778e2c141d1ac4b533fc4f7544dabe43d884e5e488786daeeb17d0cb246bdff",
                    name: "1ab1bbd47b124b439726d035b357e23a23ea12c001ca4e66a015927791308d86"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("72443cec-3247-4744-8aa2-99df9252b889"));
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
                    availableDistricts: default,
                    guaranteeMethod: default,
                    organization: "f7c14121d5884938af05e3a5eb5b24f9aad833c05d824a7e8148985fbbe2851ba71551c7d20e4ff6872cb7e3487ad9f776a6",
                    name: "edf9896b621e455ca6832b3f57471b8c67bd4aa930514678b311df79fc85906ddde12f4898c745"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}