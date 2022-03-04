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
            result.Items.Any(x => x.FinancialProduct.Id == Guid.Parse("72443cec-3247-4744-8aa2-99df9252b889")).ShouldBe(true);
            result.Items.Any(x => x.FinancialProduct.Id == Guid.Parse("8d849af2-4a89-41f1-9f28-45677c193862")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _financialProductsAppService.GetAsync(Guid.Parse("72443cec-3247-4744-8aa2-99df9252b889"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("72443cec-3247-4744-8aa2-99df9252b889"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FinancialProductCreateDto
            {
                AvailableDistricts = default,
                TimeLimit = 1320,
                GuaranteeMethod = default,
                CreditCeiling = 364716483,
                Organization = "c968050d7c78498084a65780dfc1bd94dec23a0caeeb4a59b59aebb6e950f881fa76cbc5ba8f4b54be79d96cd7858857296d",
                AppliedNumber = 1855857933,
                APR = 2091375588,
                Rating = 1461850589,
                Name = "1b2ec443ad024cc0b0405ea471ac74116bd8bcfbed064b7f850b43"
            };

            // Act
            var serviceResult = await _financialProductsAppService.CreateAsync(input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AvailableDistricts.ShouldBe(default);
            result.TimeLimit.ShouldBe(1320);
            result.GuaranteeMethod.ShouldBe(default);
            result.CreditCeiling.ShouldBe(364716483);
            result.Organization.ShouldBe("c968050d7c78498084a65780dfc1bd94dec23a0caeeb4a59b59aebb6e950f881fa76cbc5ba8f4b54be79d96cd7858857296d");
            result.AppliedNumber.ShouldBe(1855857933);
            result.APR.ShouldBe(2091375588);
            result.Rating.ShouldBe(1461850589);
            result.Name.ShouldBe("1b2ec443ad024cc0b0405ea471ac74116bd8bcfbed064b7f850b43");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FinancialProductUpdateDto()
            {
                AvailableDistricts = default,
                TimeLimit = 2166,
                GuaranteeMethod = default,
                CreditCeiling = 401420322,
                Organization = "de5deb60b66a412aac4df94ebeaf06e52bd2261de43644b9b52ffba4f8bf051e5ce02ac2580a4b0cb1d57d4f2913cdb8816a",
                AppliedNumber = 480461101,
                APR = 1917836309,
                Rating = 1841305285,
                Name = "adc60dea52724ea88c3c33faa8ebfa73c367319ac14b4b0d9f37e3f4f9814df9"
            };

            // Act
            var serviceResult = await _financialProductsAppService.UpdateAsync(Guid.Parse("72443cec-3247-4744-8aa2-99df9252b889"), input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AvailableDistricts.ShouldBe(default);
            result.TimeLimit.ShouldBe(2166);
            result.GuaranteeMethod.ShouldBe(default);
            result.CreditCeiling.ShouldBe(401420322);
            result.Organization.ShouldBe("de5deb60b66a412aac4df94ebeaf06e52bd2261de43644b9b52ffba4f8bf051e5ce02ac2580a4b0cb1d57d4f2913cdb8816a");
            result.AppliedNumber.ShouldBe(480461101);
            result.APR.ShouldBe(1917836309);
            result.Rating.ShouldBe(1841305285);
            result.Name.ShouldBe("adc60dea52724ea88c3c33faa8ebfa73c367319ac14b4b0d9f37e3f4f9814df9");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _financialProductsAppService.DeleteAsync(Guid.Parse("72443cec-3247-4744-8aa2-99df9252b889"));

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == Guid.Parse("72443cec-3247-4744-8aa2-99df9252b889"));

            result.ShouldBeNull();
        }
    }
}