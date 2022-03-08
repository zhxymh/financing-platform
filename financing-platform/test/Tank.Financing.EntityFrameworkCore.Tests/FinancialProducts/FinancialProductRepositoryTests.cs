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
                    guaranteeMethod: default,
                    creditCeiling: "c5262dbd3742401fa4b2ec32d556c1911ec5bfe10b7844a492ac0a9",
                    organization: "be7ed39c735149ff90742ff1d4622df67e31999f51d74d879b1204e403d584348e6557af18be4567a7a4180dfa323553f734",
                    aPR: "66223f206c3c4ad5ac4530d0ee215ee2e4f2febf7b93476b94ccceccddbdcd6bbb2c",
                    rating: "3d075aa5ff9f47d1827cb0bef3da14a2f7229d4fcd5f425983",
                    name: "74aae80a6fb94fc3ac27ef712a91af5d52674c1f0a3842899de451b1b3cd5"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b685c219-8b42-4a17-ab6f-7e91d8ebc47c"));
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
                    guaranteeMethod: default,
                    creditCeiling: "2b24eaddf3324bc0abfa46e5371",
                    organization: "8f3cf93a295b43799a5fd70c6e4f7c1b4b43c91a37584eaa8f463cc0af6518c2cc0e0a7f28c144078311ee2643d6b57d15f3",
                    aPR: "9c39edeb687c4e05a3bf8f64976e59afe47e1948b83c4306961f6c20",
                    rating: "9eb332510df14ff9aa73e96a81d7097e7a5",
                    name: "fa61a52081914d48b4c9957a43934ffd2d4113b60ff34f38a1d5438b0246e7769fdb383b7fa943a1bf4b226fa3c92"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}