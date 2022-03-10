using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetailsAppServiceTests : FinancingApplicationTestBase
    {
        private readonly IEnterpriseDetailsAppService _enterpriseDetailsAppService;
        private readonly IRepository<EnterpriseDetail, Guid> _enterpriseDetailRepository;

        public EnterpriseDetailsAppServiceTests()
        {
            _enterpriseDetailsAppService = GetRequiredService<IEnterpriseDetailsAppService>();
            _enterpriseDetailRepository = GetRequiredService<IRepository<EnterpriseDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _enterpriseDetailsAppService.GetListAsync(new GetEnterpriseDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("eb01b4d9-7856-46bf-8a4d-48db4dd41dac")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("b9228f64-2505-4405-b0c4-c356afa45ee8")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterpriseDetailsAppService.GetAsync(Guid.Parse("eb01b4d9-7856-46bf-8a4d-48db4dd41dac"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("eb01b4d9-7856-46bf-8a4d-48db4dd41dac"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseDetailCreateDto
            {
                EnterpriseName = "388fb33232074fa0a210897483921ecca829015c4691409aa9edf8ca2",
                TotalAssets = "5db3c35109ae497db055c0b1a8f9dfb3f9069f5583bc4eee8fd70430ea21c129421a91a789544cc38bfbaa0",
                Income = "a7415a35a3df4",
                EnterpriseType = "501c872ae2b046d2b0c6ba44557c7499c6ee3ce854924b558deb39b7091029b",
                StaffNumber = 378414690,
                Industry = "5e2855d2f676486f9c4b2b103ec6d3898465cb496b12480190d84611dcbf280ebab72159b8ba4f89a01dc162f07ee6f",
                Location = "3b78f54235aa418e820dc68f5be7203598eb8f404e6440ac97808785490097937a89",
                RegisteredAddress = "e1fa13f0d71140e0a6720ae459b3571",
                BusinessAddress = "6b9d10ea6b784ff98c92281453b08188c7ff8df835394f01a831bd2594a44a3793fefc6d369c482b9019f78e",
                BusinessScope = "513d6eb735a344268d",
                Description = "6d2786c305644ee89901b6933e08ccd00db93f3042494c17a34805549df64f41a81d596029434d6bac6f63e4a93"
            };

            // Act
            var serviceResult = await _enterpriseDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("388fb33232074fa0a210897483921ecca829015c4691409aa9edf8ca2");
            result.TotalAssets.ShouldBe("5db3c35109ae497db055c0b1a8f9dfb3f9069f5583bc4eee8fd70430ea21c129421a91a789544cc38bfbaa0");
            result.Income.ShouldBe("a7415a35a3df4");
            result.EnterpriseType.ShouldBe("501c872ae2b046d2b0c6ba44557c7499c6ee3ce854924b558deb39b7091029b");
            result.StaffNumber.ShouldBe(378414690);
            result.Industry.ShouldBe("5e2855d2f676486f9c4b2b103ec6d3898465cb496b12480190d84611dcbf280ebab72159b8ba4f89a01dc162f07ee6f");
            result.Location.ShouldBe("3b78f54235aa418e820dc68f5be7203598eb8f404e6440ac97808785490097937a89");
            result.RegisteredAddress.ShouldBe("e1fa13f0d71140e0a6720ae459b3571");
            result.BusinessAddress.ShouldBe("6b9d10ea6b784ff98c92281453b08188c7ff8df835394f01a831bd2594a44a3793fefc6d369c482b9019f78e");
            result.BusinessScope.ShouldBe("513d6eb735a344268d");
            result.Description.ShouldBe("6d2786c305644ee89901b6933e08ccd00db93f3042494c17a34805549df64f41a81d596029434d6bac6f63e4a93");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseDetailUpdateDto()
            {
                EnterpriseName = "f5c503c19d274d1facf777e2159e1397ddd21",
                TotalAssets = "b71b1a6e6f9c455abe63e650f2278158503157",
                Income = "98b0aab751d946f5",
                EnterpriseType = "fbc49e4946d149d194c16f218cfa3272ea60fed0e550448dad7c1a95e9687dc405",
                StaffNumber = 613532156,
                Industry = "7e0674d6a00b4ae89ecb6a6b95f7e6dccd8027b7271849909e48ca3d8bf59ad4d9bcf725c1b14d8f9f2a5b9fe34319",
                Location = "42bf31f15c8c4",
                RegisteredAddress = "760ce58c219549d392978aa8dd7953e72af8434d482",
                BusinessAddress = "76d9d2f4",
                BusinessScope = "92baf8dbe6cf4acf9cb366185bc92854a399358fdd6b4e52970ec8aaa65d35947f3f994966064a929bcb075f068b08710",
                Description = "6d696de25f1c49c0a39"
            };

            // Act
            var serviceResult = await _enterpriseDetailsAppService.UpdateAsync(Guid.Parse("eb01b4d9-7856-46bf-8a4d-48db4dd41dac"), input);

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("f5c503c19d274d1facf777e2159e1397ddd21");
            result.TotalAssets.ShouldBe("b71b1a6e6f9c455abe63e650f2278158503157");
            result.Income.ShouldBe("98b0aab751d946f5");
            result.EnterpriseType.ShouldBe("fbc49e4946d149d194c16f218cfa3272ea60fed0e550448dad7c1a95e9687dc405");
            result.StaffNumber.ShouldBe(613532156);
            result.Industry.ShouldBe("7e0674d6a00b4ae89ecb6a6b95f7e6dccd8027b7271849909e48ca3d8bf59ad4d9bcf725c1b14d8f9f2a5b9fe34319");
            result.Location.ShouldBe("42bf31f15c8c4");
            result.RegisteredAddress.ShouldBe("760ce58c219549d392978aa8dd7953e72af8434d482");
            result.BusinessAddress.ShouldBe("76d9d2f4");
            result.BusinessScope.ShouldBe("92baf8dbe6cf4acf9cb366185bc92854a399358fdd6b4e52970ec8aaa65d35947f3f994966064a929bcb075f068b08710");
            result.Description.ShouldBe("6d696de25f1c49c0a39");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterpriseDetailsAppService.DeleteAsync(Guid.Parse("eb01b4d9-7856-46bf-8a4d-48db4dd41dac"));

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == Guid.Parse("eb01b4d9-7856-46bf-8a4d-48db4dd41dac"));

            result.ShouldBeNull();
        }
    }
}