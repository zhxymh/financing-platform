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
            result.Items.Any(x => x.Id == Guid.Parse("4ded5ed9-024e-4820-b29c-46bea9ffabc9")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("7d8618a8-4c0b-4c3a-8717-76fe78b6e8b8")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _appliesAppService.GetAsync(Guid.Parse("4ded5ed9-024e-4820-b29c-46bea9ffabc9"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4ded5ed9-024e-4820-b29c-46bea9ffabc9"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ApplyCreateDto
            {
                EnterpriseName = "fff32832ec984d1b9c76fe09e18086ef25b48ae607e745a28b49498f8565a0c049bc6dc01ba74a75a145d25f55153553ba9",
                Organization = "49eb099840354f439464c1e340f6a93d798bdd235e0246d5ad090716ed8847b3",
                ProductName = "f81e35ba08ce4235b0cd1401f1bc7f2b1e55de6688974",
                Allowance = "1fd0b8a5ccc246baad78d64b950d4c256ea291b260c34c568256bf568b688d427851fed01c3e4e2ebe156d6a0396c82f",
                APR = "d9c34622888444b084d4648247174174ca0",
                Period = "dc635effff054934b68d4524374152dd61e3f9eb05184279bdb8e15226ef9de3f829b27c9d294d988",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 576359549,
                PassedTime = 1474407520
            };

            // Act
            var serviceResult = await _appliesAppService.CreateAsync(input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("fff32832ec984d1b9c76fe09e18086ef25b48ae607e745a28b49498f8565a0c049bc6dc01ba74a75a145d25f55153553ba9");
            result.Organization.ShouldBe("49eb099840354f439464c1e340f6a93d798bdd235e0246d5ad090716ed8847b3");
            result.ProductName.ShouldBe("f81e35ba08ce4235b0cd1401f1bc7f2b1e55de6688974");
            result.Allowance.ShouldBe("1fd0b8a5ccc246baad78d64b950d4c256ea291b260c34c568256bf568b688d427851fed01c3e4e2ebe156d6a0396c82f");
            result.APR.ShouldBe("d9c34622888444b084d4648247174174ca0");
            result.Period.ShouldBe("dc635effff054934b68d4524374152dd61e3f9eb05184279bdb8e15226ef9de3f829b27c9d294d988");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(576359549);
            result.PassedTime.ShouldBe(1474407520);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ApplyUpdateDto()
            {
                EnterpriseName = "d1ef7222cbfb49db9eff8cc217d06d00f22cdba06666477c88eb9bd6ec5f",
                Organization = "ebd14bf9089f4ec98b88dac5d3be890fcfd6e2f16f",
                ProductName = "10cfeab07205",
                Allowance = "72e4612c26c349e6999a3ece5a5720f17d",
                APR = "524dcd30aae4498292772f2880cc904e09ef5cbfcdb640568bd372fd2",
                Period = "5a1fa5f404ea476d84d1ebd30a2e0abf1e24d6d88b2a442fb3b8d36ee93b6f747d36c",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 1323429093,
                PassedTime = 770711757
            };

            // Act
            var serviceResult = await _appliesAppService.UpdateAsync(Guid.Parse("4ded5ed9-024e-4820-b29c-46bea9ffabc9"), input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("d1ef7222cbfb49db9eff8cc217d06d00f22cdba06666477c88eb9bd6ec5f");
            result.Organization.ShouldBe("ebd14bf9089f4ec98b88dac5d3be890fcfd6e2f16f");
            result.ProductName.ShouldBe("10cfeab07205");
            result.Allowance.ShouldBe("72e4612c26c349e6999a3ece5a5720f17d");
            result.APR.ShouldBe("524dcd30aae4498292772f2880cc904e09ef5cbfcdb640568bd372fd2");
            result.Period.ShouldBe("5a1fa5f404ea476d84d1ebd30a2e0abf1e24d6d88b2a442fb3b8d36ee93b6f747d36c");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(1323429093);
            result.PassedTime.ShouldBe(770711757);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _appliesAppService.DeleteAsync(Guid.Parse("4ded5ed9-024e-4820-b29c-46bea9ffabc9"));

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == Guid.Parse("4ded5ed9-024e-4820-b29c-46bea9ffabc9"));

            result.ShouldBeNull();
        }
    }
}