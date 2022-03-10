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
                    productName: "b664e5945c7b4d12b92162ddfa751ba6daf1eb88c94b414c91a7f9ee4687b2e4dcb4a09804a",
                    organization: "9c9bf53a0a6a439297dbaca3396b749d78b97e0c02fa47f49",
                    guaranteeMethod: default,
                    aPR: "b8ebf7660d874f30877b0c078d72dee89324181f79654a7faada21989e34d09761e425f34ca044148e",
                    rating: "0bc87f1b401740e7893bb31949913"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("bb9bef20-7da9-429a-9883-1d66bcc565b6"));
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
                    productName: "527cefb899774394ae4475e23ebefe4c4ae34e38b5c24ae5a14bcf",
                    organization: "3ec0113e9d0041",
                    guaranteeMethod: default,
                    aPR: "ffbdb0e667e944b4bf38af0fc60aaa1f920010f4280743beb642b6b67d426569c887",
                    rating: "76f160c632c645c593d680e8c723c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}