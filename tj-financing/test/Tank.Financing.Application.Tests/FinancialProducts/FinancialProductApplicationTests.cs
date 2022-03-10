using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductsAppServiceTests : FinancingApplicationTestBase
    {
        private readonly IFinancialProductsAppService _financialProductsAppService;
        private readonly IRepository<FinancialProduct, Guid> _financialProductRepository;

        public FinancialProductsAppServiceTests()
        {
            _financialProductsAppService = GetRequiredService<IFinancialProductsAppService>();
            _financialProductRepository = GetRequiredService<IRepository<FinancialProduct, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _financialProductsAppService.GetListAsync(new GetFinancialProductsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("bb9bef20-7da9-429a-9883-1d66bcc565b6")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("2f9f0d4a-44d9-4a04-8206-90dea1ae7d94")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _financialProductsAppService.GetAsync(Guid.Parse("bb9bef20-7da9-429a-9883-1d66bcc565b6"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bb9bef20-7da9-429a-9883-1d66bcc565b6"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FinancialProductCreateDto
            {
                ProductName = "c03092ed7de342ef8a9b6f67166f701538113db819a74454966ca1a",
                Organization = "881cdbfdff444302863ab6961724e35",
                Period = 1961385692,
                GuaranteeMethod = default,
                AppliedNumber = 1424664985,
                APR = "2a8adcedc1064174979e651f2dd9a6171ba401fe7",
                Rating = "aaa1aa2f7ae4432786cc8eb27d0cbc5ab5d6052fde0a4190a70ab2b4a1811e2df7c5fe742c4b40d2b32b86c0c",
                CreditCeiling = 948422413
            };

            // Act
            var serviceResult = await _financialProductsAppService.CreateAsync(input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProductName.ShouldBe("c03092ed7de342ef8a9b6f67166f701538113db819a74454966ca1a");
            result.Organization.ShouldBe("881cdbfdff444302863ab6961724e35");
            result.Period.ShouldBe(1961385692);
            result.GuaranteeMethod.ShouldBe(default);
            result.AppliedNumber.ShouldBe(1424664985);
            result.APR.ShouldBe("2a8adcedc1064174979e651f2dd9a6171ba401fe7");
            result.Rating.ShouldBe("aaa1aa2f7ae4432786cc8eb27d0cbc5ab5d6052fde0a4190a70ab2b4a1811e2df7c5fe742c4b40d2b32b86c0c");
            result.CreditCeiling.ShouldBe(948422413);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FinancialProductUpdateDto()
            {
                ProductName = "1df66d5fa3bb479ea6c6dfc00de913d3d4f03995610f433e9bf1707",
                Organization = "86f101c5bec249f388373",
                Period = 1874570939,
                GuaranteeMethod = default,
                AppliedNumber = 1152798988,
                APR = "8d4c297fa28b438daff69b5f94991ba73",
                Rating = "92f2dd16b522487d997ae4f3e50b72fee3172e26efdf45278e80334139eae1c476f22963f7fd488da33b56bcf10",
                CreditCeiling = 1122480582
            };

            // Act
            var serviceResult = await _financialProductsAppService.UpdateAsync(Guid.Parse("bb9bef20-7da9-429a-9883-1d66bcc565b6"), input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProductName.ShouldBe("1df66d5fa3bb479ea6c6dfc00de913d3d4f03995610f433e9bf1707");
            result.Organization.ShouldBe("86f101c5bec249f388373");
            result.Period.ShouldBe(1874570939);
            result.GuaranteeMethod.ShouldBe(default);
            result.AppliedNumber.ShouldBe(1152798988);
            result.APR.ShouldBe("8d4c297fa28b438daff69b5f94991ba73");
            result.Rating.ShouldBe("92f2dd16b522487d997ae4f3e50b72fee3172e26efdf45278e80334139eae1c476f22963f7fd488da33b56bcf10");
            result.CreditCeiling.ShouldBe(1122480582);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _financialProductsAppService.DeleteAsync(Guid.Parse("bb9bef20-7da9-429a-9883-1d66bcc565b6"));

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == Guid.Parse("bb9bef20-7da9-429a-9883-1d66bcc565b6"));

            result.ShouldBeNull();
        }
    }
}