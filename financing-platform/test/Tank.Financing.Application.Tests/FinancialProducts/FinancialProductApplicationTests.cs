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
            result.Items.Any(x => x.Id == Guid.Parse("b685c219-8b42-4a17-ab6f-7e91d8ebc47c")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("af289d5f-7538-4b6f-9741-4728f9786d84")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _financialProductsAppService.GetAsync(Guid.Parse("b685c219-8b42-4a17-ab6f-7e91d8ebc47c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b685c219-8b42-4a17-ab6f-7e91d8ebc47c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FinancialProductCreateDto
            {
                Period = 3235,
                GuaranteeMethod = default,
                CreditCeiling = "a3eebf04cb5843eda51339bd6a4d",
                Organization = "b2075d03e7234f19b2afcfa32a54adbe00abe119a1d34985adb0841dc30378410a0c30c54c0e442a9d424f6f6138b551d174",
                AppliedNumber = 1554995376,
                APR = "02ec80ec52fa43",
                Rating = "c25d7c208e564236ae4eb3aee4e1cdb5f3a4fb68fffd42469cb",
                Name = "cec95b06b04744a680e454e8be9a93b6854ac57312b84"
            };

            // Act
            var serviceResult = await _financialProductsAppService.CreateAsync(input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Period.ShouldBe(3235);
            result.GuaranteeMethod.ShouldBe(default);
            result.CreditCeiling.ShouldBe("a3eebf04cb5843eda51339bd6a4d");
            result.Organization.ShouldBe("b2075d03e7234f19b2afcfa32a54adbe00abe119a1d34985adb0841dc30378410a0c30c54c0e442a9d424f6f6138b551d174");
            result.AppliedNumber.ShouldBe(1554995376);
            result.APR.ShouldBe("02ec80ec52fa43");
            result.Rating.ShouldBe("c25d7c208e564236ae4eb3aee4e1cdb5f3a4fb68fffd42469cb");
            result.Name.ShouldBe("cec95b06b04744a680e454e8be9a93b6854ac57312b84");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FinancialProductUpdateDto()
            {
                Period = 2670,
                GuaranteeMethod = default,
                CreditCeiling = "6ee23fc8b1da4ef3bb41711e2bbb0b422bf6ad5c00994fd8aeb3e53c4b2356d366fabdf77358446d8",
                Organization = "89393199a0414ede8f7a46995b33a266a815fbb4a06241e9b496eb7a37ee634175d337fcdde84f9f9c6cb7a04aa0d8ecbc7a",
                AppliedNumber = 380043191,
                APR = "3d64023c513c4eb2bff98c578c59b4fb54be9cebe2b94ee7b15c06e6d2",
                Rating = "600f827855264bdb8c666c3b698871b35b3456b4b1534255b79193dcdf23b57ef5e4aa27866c46e5ba92baa3434bf9a659f",
                Name = "26d9950605c54183a2e517d68078340aa1b"
            };

            // Act
            var serviceResult = await _financialProductsAppService.UpdateAsync(Guid.Parse("b685c219-8b42-4a17-ab6f-7e91d8ebc47c"), input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Period.ShouldBe(2670);
            result.GuaranteeMethod.ShouldBe(default);
            result.CreditCeiling.ShouldBe("6ee23fc8b1da4ef3bb41711e2bbb0b422bf6ad5c00994fd8aeb3e53c4b2356d366fabdf77358446d8");
            result.Organization.ShouldBe("89393199a0414ede8f7a46995b33a266a815fbb4a06241e9b496eb7a37ee634175d337fcdde84f9f9c6cb7a04aa0d8ecbc7a");
            result.AppliedNumber.ShouldBe(380043191);
            result.APR.ShouldBe("3d64023c513c4eb2bff98c578c59b4fb54be9cebe2b94ee7b15c06e6d2");
            result.Rating.ShouldBe("600f827855264bdb8c666c3b698871b35b3456b4b1534255b79193dcdf23b57ef5e4aa27866c46e5ba92baa3434bf9a659f");
            result.Name.ShouldBe("26d9950605c54183a2e517d68078340aa1b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _financialProductsAppService.DeleteAsync(Guid.Parse("b685c219-8b42-4a17-ab6f-7e91d8ebc47c"));

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == Guid.Parse("b685c219-8b42-4a17-ab6f-7e91d8ebc47c"));

            result.ShouldBeNull();
        }
    }
}