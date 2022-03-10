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
            result.Items.Any(x => x.Id == Guid.Parse("5a8b8948-245f-48e3-8360-9ce4b767aa93")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("732e3eb9-65fe-4585-b8d0-39b286409087")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _financialProductsAppService.GetAsync(Guid.Parse("5a8b8948-245f-48e3-8360-9ce4b767aa93"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5a8b8948-245f-48e3-8360-9ce4b767aa93"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FinancialProductCreateDto
            {
                ProductName = "c4fdfdf186234d67b90bb48f4373b833c793832e8a3549e3ac948a6ec91e888c8869a5",
                Organization = "e17798d6bd984bbf849ba35f3f7917f18640cc6f13504426832072",
                Period = 681907494,
                GuaranteeMethod = default,
                AppliedNumber = 1677023596,
                APR = "ad090be",
                Rating = "c969e071bfa946b7857199dd79a2244b1d73fa4955de441db1805c",
                CreditCeiling = 1878213306,
                AddFinancingProductTxId = "aecc8a8972e24633958787d44381bf6e92c93c"
            };

            // Act
            var serviceResult = await _financialProductsAppService.CreateAsync(input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProductName.ShouldBe("c4fdfdf186234d67b90bb48f4373b833c793832e8a3549e3ac948a6ec91e888c8869a5");
            result.Organization.ShouldBe("e17798d6bd984bbf849ba35f3f7917f18640cc6f13504426832072");
            result.Period.ShouldBe(681907494);
            result.GuaranteeMethod.ShouldBe(default);
            result.AppliedNumber.ShouldBe(1677023596);
            result.APR.ShouldBe("ad090be");
            result.Rating.ShouldBe("c969e071bfa946b7857199dd79a2244b1d73fa4955de441db1805c");
            result.CreditCeiling.ShouldBe(1878213306);
            result.AddFinancingProductTxId.ShouldBe("aecc8a8972e24633958787d44381bf6e92c93c");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FinancialProductUpdateDto()
            {
                ProductName = "9f04c20c952b45009b5fd15f045ad0a1ee8b09",
                Organization = "9752bca771b24bceb2678c7035ee3be84762b77890b141bbbb08a0358a7524879696d35de9f246acac8",
                Period = 2016754191,
                GuaranteeMethod = default,
                AppliedNumber = 134316730,
                APR = "1c25db832ee54ff7a91606c153c91e34136d86cd9f524ad3b461bae38d1ad4448089faf381de4917b44c4",
                Rating = "ed05bdfaeef348e19fc6323709e35a97f1",
                CreditCeiling = 658472400,
                AddFinancingProductTxId = "cd2d7714feef498f9f003b6ccb46e0a07c082d271cae4c0884b9e4f9545af00d9a1fb28686e644f"
            };

            // Act
            var serviceResult = await _financialProductsAppService.UpdateAsync(Guid.Parse("5a8b8948-245f-48e3-8360-9ce4b767aa93"), input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProductName.ShouldBe("9f04c20c952b45009b5fd15f045ad0a1ee8b09");
            result.Organization.ShouldBe("9752bca771b24bceb2678c7035ee3be84762b77890b141bbbb08a0358a7524879696d35de9f246acac8");
            result.Period.ShouldBe(2016754191);
            result.GuaranteeMethod.ShouldBe(default);
            result.AppliedNumber.ShouldBe(134316730);
            result.APR.ShouldBe("1c25db832ee54ff7a91606c153c91e34136d86cd9f524ad3b461bae38d1ad4448089faf381de4917b44c4");
            result.Rating.ShouldBe("ed05bdfaeef348e19fc6323709e35a97f1");
            result.CreditCeiling.ShouldBe(658472400);
            result.AddFinancingProductTxId.ShouldBe("cd2d7714feef498f9f003b6ccb46e0a07c082d271cae4c0884b9e4f9545af00d9a1fb28686e644f");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _financialProductsAppService.DeleteAsync(Guid.Parse("5a8b8948-245f-48e3-8360-9ce4b767aa93"));

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == Guid.Parse("5a8b8948-245f-48e3-8360-9ce4b767aa93"));

            result.ShouldBeNull();
        }
    }
}