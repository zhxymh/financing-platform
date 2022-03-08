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
            result.Items.Any(x => x.Id == Guid.Parse("11720ca5-8951-4cb1-9e95-487613da6d9f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d4399153-f0fe-4d99-88d4-2cfd6b8d35ae")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _appliesAppService.GetAsync(Guid.Parse("11720ca5-8951-4cb1-9e95-487613da6d9f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("11720ca5-8951-4cb1-9e95-487613da6d9f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ApplyCreateDto
            {
                EnterpriseName = "1f04dea64d8140a7b0b02b610b739312dfb0cd9f89ff42d78ec28be2d6c0bdedf1140f6d43b34f8d839ac0b8f31d",
                Organization = "4cb0380b43d54c06a15fb0bff8b3ce8bfff8ffb5662e43c8af",
                ProductName = "3ad64d01ceab4badb71162ab49d2c72214a61b48148640759d57e2a2cdbfe77",
                Allowance = "3f5a881080014f799797ebe64b3e87359e584ea8e7ad407d9b3d0d8458d4ab",
                APR = "82ca10512666440c8fbc2aa2a366a3febe07e",
                Period = "bfc3988216024cb",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 1451878474,
                PassedTime = 660834753
            };

            // Act
            var serviceResult = await _appliesAppService.CreateAsync(input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("1f04dea64d8140a7b0b02b610b739312dfb0cd9f89ff42d78ec28be2d6c0bdedf1140f6d43b34f8d839ac0b8f31d");
            result.Organization.ShouldBe("4cb0380b43d54c06a15fb0bff8b3ce8bfff8ffb5662e43c8af");
            result.ProductName.ShouldBe("3ad64d01ceab4badb71162ab49d2c72214a61b48148640759d57e2a2cdbfe77");
            result.Allowance.ShouldBe("3f5a881080014f799797ebe64b3e87359e584ea8e7ad407d9b3d0d8458d4ab");
            result.APR.ShouldBe("82ca10512666440c8fbc2aa2a366a3febe07e");
            result.Period.ShouldBe("bfc3988216024cb");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(1451878474);
            result.PassedTime.ShouldBe(660834753);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ApplyUpdateDto()
            {
                EnterpriseName = "b51d55d7a0484",
                Organization = "d2ceefc1f1ab4f3bb697e70d71b82ffec02bff3bf7004b65a00575feaf214d2124b1ed2a8533",
                ProductName = "2de9ee8bb20d4c1e867323a718b227aef070f7cc41e44b5299352daf42f68bef066f959639aa420daf6ce20f",
                Allowance = "3e857f9d503f493884a69af681e733e5e395f8b0ae67",
                APR = "2022762e744e4b8b82b40554906a9db9b8df5306ce834c64ba26",
                Period = "8e95931d6dab4e93a8b31cd376b50e80db0fc14304f04dffb299d51e8a5b",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 1782813875,
                PassedTime = 400404220
            };

            // Act
            var serviceResult = await _appliesAppService.UpdateAsync(Guid.Parse("11720ca5-8951-4cb1-9e95-487613da6d9f"), input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("b51d55d7a0484");
            result.Organization.ShouldBe("d2ceefc1f1ab4f3bb697e70d71b82ffec02bff3bf7004b65a00575feaf214d2124b1ed2a8533");
            result.ProductName.ShouldBe("2de9ee8bb20d4c1e867323a718b227aef070f7cc41e44b5299352daf42f68bef066f959639aa420daf6ce20f");
            result.Allowance.ShouldBe("3e857f9d503f493884a69af681e733e5e395f8b0ae67");
            result.APR.ShouldBe("2022762e744e4b8b82b40554906a9db9b8df5306ce834c64ba26");
            result.Period.ShouldBe("8e95931d6dab4e93a8b31cd376b50e80db0fc14304f04dffb299d51e8a5b");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(1782813875);
            result.PassedTime.ShouldBe(400404220);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _appliesAppService.DeleteAsync(Guid.Parse("11720ca5-8951-4cb1-9e95-487613da6d9f"));

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == Guid.Parse("11720ca5-8951-4cb1-9e95-487613da6d9f"));

            result.ShouldBeNull();
        }
    }
}