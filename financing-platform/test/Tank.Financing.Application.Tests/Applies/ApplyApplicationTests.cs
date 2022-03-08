using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tank.Financing.Applies
{
    public class AppliesAppServiceTests : FinancingApplicationTestBase
    {
        private readonly IAppliesAppService _appliesAppService;
        private readonly IRepository<Apply, Guid> _applyRepository;

        public AppliesAppServiceTests()
        {
            _appliesAppService = GetRequiredService<IAppliesAppService>();
            _applyRepository = GetRequiredService<IRepository<Apply, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _appliesAppService.GetListAsync(new GetAppliesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("704ce5e8-2fa7-4116-a085-5e067b182bad")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("51d4c0e3-d5cc-4974-a530-0432d405f5af")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _appliesAppService.GetAsync(Guid.Parse("704ce5e8-2fa7-4116-a085-5e067b182bad"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("704ce5e8-2fa7-4116-a085-5e067b182bad"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ApplyCreateDto
            {
                EnterpriseName = "90611652c46",
                Organization = "302197f798df4943af1a5d5b",
                ProductName = "552b36c6f90c4feda19a9a7a62139f718bd2aeb59e9942bebd0736f3c6f1bc",
                Allowance = "ad64c0fefb4242a2b7f6e",
                APY = "5cfbc72a86664e679b21998",
                Period = "dc3244f1eb1f48fc9c5670644f0b8da24b7c2e23d9114361b691e2a04227cba5dbdc3",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = "f19a2fc10a1143608b5ff940fb64503dc149111f1be140a98a74a5ad62d3189e139f70f7d19946b3b",
                PassedTime = "b77c65aaba554f76928435ace736d0a97bf120acb60b47ee8bd700d3b33041f196069ce768704f64b6b"
            };

            // Act
            var serviceResult = await _appliesAppService.CreateAsync(input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("90611652c46");
            result.Organization.ShouldBe("302197f798df4943af1a5d5b");
            result.ProductName.ShouldBe("552b36c6f90c4feda19a9a7a62139f718bd2aeb59e9942bebd0736f3c6f1bc");
            result.Allowance.ShouldBe("ad64c0fefb4242a2b7f6e");
            result.APY.ShouldBe("5cfbc72a86664e679b21998");
            result.Period.ShouldBe("dc3244f1eb1f48fc9c5670644f0b8da24b7c2e23d9114361b691e2a04227cba5dbdc3");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe("f19a2fc10a1143608b5ff940fb64503dc149111f1be140a98a74a5ad62d3189e139f70f7d19946b3b");
            result.PassedTime.ShouldBe("b77c65aaba554f76928435ace736d0a97bf120acb60b47ee8bd700d3b33041f196069ce768704f64b6b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ApplyUpdateDto()
            {
                EnterpriseName = "30db665f82c04c1c9ad56288b9f99890e46697bf0f6d488c8b971a",
                Organization = "b671e878e5d04765b2ced7db485cd98550ce01c099dd4516a178b6e8ced",
                ProductName = "747c230a35d",
                Allowance = "9648542ecad143a7a5da6748eaf23940d2e422e65eb24c1e8f660e86eaccdb7a1624713f6c37479db04e5fc1947dcbb60",
                APY = "696dd118",
                Period = "4e56b109b51b467fb0b239fc069023a2802fc3c91781401e8575f590a89f3f96c21e3628902f4b15bb035f09c0be9",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = "0e78e796be01456bb7e5d5109f14058367c0af2abc1544e1bd1d",
                PassedTime = "8f210f2f219642a1a1eefb58c4f55895f2b16da2ce7d4c7893ae0248baacbc0"
            };

            // Act
            var serviceResult = await _appliesAppService.UpdateAsync(Guid.Parse("704ce5e8-2fa7-4116-a085-5e067b182bad"), input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("30db665f82c04c1c9ad56288b9f99890e46697bf0f6d488c8b971a");
            result.Organization.ShouldBe("b671e878e5d04765b2ced7db485cd98550ce01c099dd4516a178b6e8ced");
            result.ProductName.ShouldBe("747c230a35d");
            result.Allowance.ShouldBe("9648542ecad143a7a5da6748eaf23940d2e422e65eb24c1e8f660e86eaccdb7a1624713f6c37479db04e5fc1947dcbb60");
            result.APY.ShouldBe("696dd118");
            result.Period.ShouldBe("4e56b109b51b467fb0b239fc069023a2802fc3c91781401e8575f590a89f3f96c21e3628902f4b15bb035f09c0be9");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe("0e78e796be01456bb7e5d5109f14058367c0af2abc1544e1bd1d");
            result.PassedTime.ShouldBe("8f210f2f219642a1a1eefb58c4f55895f2b16da2ce7d4c7893ae0248baacbc0");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _appliesAppService.DeleteAsync(Guid.Parse("704ce5e8-2fa7-4116-a085-5e067b182bad"));

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == Guid.Parse("704ce5e8-2fa7-4116-a085-5e067b182bad"));

            result.ShouldBeNull();
        }
    }
}