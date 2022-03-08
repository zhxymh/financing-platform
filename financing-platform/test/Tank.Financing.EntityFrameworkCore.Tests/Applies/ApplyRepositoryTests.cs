using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tank.Financing.Applies;
using Tank.Financing.EntityFrameworkCore;
using Xunit;

namespace Tank.Financing.Applies
{
    public class ApplyRepositoryTests : FinancingEntityFrameworkCoreTestBase
    {
        private readonly IApplyRepository _applyRepository;

        public ApplyRepositoryTests()
        {
            _applyRepository = GetRequiredService<IApplyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _applyRepository.GetListAsync(
                    enterpriseName: "b6ef317e1e0142debcecd599e88ce8aa7984645fba534a27b7e6986e",
                    organization: "21b748d4de284f6da04dcd7902087008acab423b16ec450d8ca11a9a46059c708c81db0f29bf4793b22aea9b30d6227cbe",
                    productName: "f3198b24a3e247fc85ccbeabddf5c2bdda5f",
                    allowance: "565147bd69d5478685b147bfc2e8b408f249a3950da84143aaffe87418dc00a6b",
                    aPR: "f69a3bd4ad104933863",
                    period: "277f67a4b96641c4bb864ad1",
                    applyStatus: default,
                    guaranteeMethod: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("11720ca5-8951-4cb1-9e95-487613da6d9f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _applyRepository.GetCountAsync(
                    enterpriseName: "fd931476f02e41b791b6c266d2e3b63bf44f4ffb343a4f979324cef0e46e7f528d060ba0dacf4f",
                    organization: "d5f279f8440e4f85a6f8278bef90050c40f61efcb19f432c9449bb75b3f81c0f3cf81d6778d243cea",
                    productName: "0ca3e404c31340b7886652ceeb0ef98e64956d5df6fa4656be4c19c43655d2cf82790337256b4d5cb6062d1f9079c70469c",
                    allowance: "534a777cfd794569a50b19879cf",
                    aPR: "f76372b170724a2890e50489702658d5fb950baf91b8486c83fc5089f892266ae1ce43588a134",
                    period: "3217cacbd7794118a2e4ec2cad35514ca9cababd53bc400cac5c398645266fd81115fd303f8e47a18a50d47ee8fbb3d3c1",
                    applyStatus: default,
                    guaranteeMethod: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}