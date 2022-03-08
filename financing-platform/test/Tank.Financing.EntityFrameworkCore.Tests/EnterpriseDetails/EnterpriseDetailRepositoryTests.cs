using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tank.Financing.EnterpriseDetails;
using Tank.Financing.EntityFrameworkCore;
using Xunit;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetailRepositoryTests : FinancingEntityFrameworkCoreTestBase
    {
        private readonly IEnterpriseDetailRepository _enterpriseDetailRepository;

        public EnterpriseDetailRepositoryTests()
        {
            _enterpriseDetailRepository = GetRequiredService<IEnterpriseDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _enterpriseDetailRepository.GetListAsync(
                    enterpriseName: "da30763f0baf454fa980b186f08226fd65e0f",
                    totalAssets: "da7f64fb63f44906b1054e0e4bb51e645a2debb8848e",
                    income: "5bea7b6af3bd4529a5a65c6e800df0789fb1a93202e34543a79471a24aec9b3cfad8d2",
                    enterpriseType: "1c45a41c7b464deb810a5fadd4fe01e663f930b0ab9f4aae888a0a9a0b141afb115a7a150ff040108943be8ca43afb4a",
                    industry: "624781b9aabd45678500ecb46b291abd725d8050694947249c7405d998251a58902b01e1ac534f30b329505",
                    location: "abf2c4a92d494194885d6e953313d5633d68350c28124ce7b71bad88241e6495d3a531765e164d169ac957635f8f",
                    registeredAddress: "df3633b87b8a4eb0aff51a9083e533300f48585c08514f148d95d8d4933f0ddb34cdea480f984608abc48d194dd6",
                    businessAddress: "c959c8b698584c369b311262a93e40a95b1cf183b7b342ceb3f0a6b946b4a5217b68f7e3712845feb7a96",
                    businessScope: "a0e90fb929b144209e86ff69144608e614439d972dd0462fbefee2ef24b2a169f2bd976d4e29419d87a5c",
                    description: "fc18400dae784598a3c9e40"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("98cef5d2-574e-429e-92f1-c317905b39f6"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _enterpriseDetailRepository.GetCountAsync(
                    enterpriseName: "48fa78080c5b461faeb1",
                    totalAssets: "9b739a7b9e8e482c9a238d9efa82b6a523fca",
                    income: "814e91c6bd5343b0ab06",
                    enterpriseType: "c84ea2e9223249fa8b114d21e8ca9c7952a42b0425f74a41b54c62a0fc28749cfed5f43b",
                    industry: "05846d82",
                    location: "61d4f737444e43fa8d45d1fbffbd7678f93943431b65490384b4e579937641b9ad3619825e9a4548828",
                    registeredAddress: "0ea6508fb0ad4bacb2b788fcacc8541bab15394931d2469d9ade3e77dc8ebcd0424adcb36ae44ddabe",
                    businessAddress: "453dccae645",
                    businessScope: "8f8be311d46",
                    description: "4284a0b9b1d1"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}