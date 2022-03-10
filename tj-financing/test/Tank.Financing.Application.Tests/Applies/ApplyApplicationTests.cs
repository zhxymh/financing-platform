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
            result.Items.Any(x => x.Id == Guid.Parse("ebb7b728-053d-49d6-bb38-2c6404b0da19")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("75e56cf5-a6ec-4e18-b777-2a87b9d9ea12")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _appliesAppService.GetAsync(Guid.Parse("ebb7b728-053d-49d6-bb38-2c6404b0da19"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ebb7b728-053d-49d6-bb38-2c6404b0da19"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ApplyCreateDto
            {
                EnterpriseName = "c8c21f392eff415a807d63b917855a36b1418c8069fb4827975e425e1473b38",
                Organization = "ff4ebdcbc2cb4ea",
                ProductName = "5019a4c14454416092",
                Allowance = "71b56e17f38247678eb9383f64bbdf201e2be07831b947738",
                APR = "601a961d9a9d45c2a409ff5ffabe67b6a7e5f331cf1c40e6b68ba095043a9645d10a00d41f5c41ec9c",
                Period = "398954202f0c494ab12598aebc9807959e01f62f6aa24932b",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 1839893132,
                PassedTime = 1228948524
            };

            // Act
            var serviceResult = await _appliesAppService.CreateAsync(input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("c8c21f392eff415a807d63b917855a36b1418c8069fb4827975e425e1473b38");
            result.Organization.ShouldBe("ff4ebdcbc2cb4ea");
            result.ProductName.ShouldBe("5019a4c14454416092");
            result.Allowance.ShouldBe("71b56e17f38247678eb9383f64bbdf201e2be07831b947738");
            result.APR.ShouldBe("601a961d9a9d45c2a409ff5ffabe67b6a7e5f331cf1c40e6b68ba095043a9645d10a00d41f5c41ec9c");
            result.Period.ShouldBe("398954202f0c494ab12598aebc9807959e01f62f6aa24932b");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(1839893132);
            result.PassedTime.ShouldBe(1228948524);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ApplyUpdateDto()
            {
                EnterpriseName = "658046e4879640ad8f676d109527394f697d99fc79d640e2a1bb305379d974c6f261d91fba024085b9ad3882",
                Organization = "221022f231144bea877b68087dd52ed053b9f0fd8e38411ca85244a578b31113876a9f3e11c4461f8b4d00e3",
                ProductName = "b4e94a69d6a34a558931b02308e81806fd3560cd3f564b40a15620cce54bd7eed30059f3",
                Allowance = "b84fe8",
                APR = "521a753f44294b16a55c9dbad15c58570",
                Period = "c517e9f48c27",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 1278219940,
                PassedTime = 445145235
            };

            // Act
            var serviceResult = await _appliesAppService.UpdateAsync(Guid.Parse("ebb7b728-053d-49d6-bb38-2c6404b0da19"), input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("658046e4879640ad8f676d109527394f697d99fc79d640e2a1bb305379d974c6f261d91fba024085b9ad3882");
            result.Organization.ShouldBe("221022f231144bea877b68087dd52ed053b9f0fd8e38411ca85244a578b31113876a9f3e11c4461f8b4d00e3");
            result.ProductName.ShouldBe("b4e94a69d6a34a558931b02308e81806fd3560cd3f564b40a15620cce54bd7eed30059f3");
            result.Allowance.ShouldBe("b84fe8");
            result.APR.ShouldBe("521a753f44294b16a55c9dbad15c58570");
            result.Period.ShouldBe("c517e9f48c27");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(1278219940);
            result.PassedTime.ShouldBe(445145235);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _appliesAppService.DeleteAsync(Guid.Parse("ebb7b728-053d-49d6-bb38-2c6404b0da19"));

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == Guid.Parse("ebb7b728-053d-49d6-bb38-2c6404b0da19"));

            result.ShouldBeNull();
        }
    }
}