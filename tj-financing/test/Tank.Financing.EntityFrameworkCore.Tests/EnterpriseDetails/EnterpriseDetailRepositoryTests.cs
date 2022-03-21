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
                    enterpriseName: "bad183beadbc4baba8aaee5e9701da7ea237a2053",
                    totalAssets: "f32a56f9a1f04405b231c2691b6db3d2770245c68f4f46aa945df4968deb4c06bb4fd40fdeeb425",
                    income: "5e43bf0d973c494caec75d352fa822ceff34d93fde264ed593c22ccbdc0250cb173137206",
                    enterpriseType: "79b862490e2a",
                    industry: "f8e16a2f02654148b12a6d7ef01433fcf8a7ef682ed44f499cc04ad88952d4a86351d997627e42a691b7c8e62c",
                    location: "0bd8dfa8eb68448baef34b0624d8cfb81ec3acc397d4458296f58790f87da972eb85150666014ba0b6866f89689",
                    registeredAddress: "1216ddf63f6f404d9fec18f5a03c06556b14b28be373426289db673fc71cc5da4713721a79cb45c79",
                    businessAddress: "eac56166b738494c8841",
                    businessScope: "692a6d35b35e494a978f268805751b0d2e8f5dd37bd64dd19ea928a12060e8630bbadae3d28c427",
                    description: "901ad67f18a648fd8209",
                    completeTxId: "8ee78e5f470f49b086be2b2b841732d82c05f840354d453b95a28f7ef549117a953b013505474e81bed",
                    commitUserName: "5be6bb6f2b3b4ea78a5feee4c96ef2459359da7bec8e4b898146"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("089227f9-ded5-4ee3-8baf-f4973e762b10"));
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
                    enterpriseName: "eca86aea2a6b4756a0a073fe2e5d9f2bda6f86547a494ff189c5564086eab7f55aaff986e6ae46069039c1401b139ced57",
                    totalAssets: "7fe304daf9da40f68dcc376a7d16d8f975dc",
                    income: "41e164fa0b1b49",
                    enterpriseType: "e38ad7e4d3f542c88db9704db49530758bc6b2345e044c86a9149345d2c48c4423398ca1fb15480",
                    industry: "78e753b74c78459eab6be79540c9",
                    location: "1c594f5155a34cc19181156fb85201fbc5439f227f184f878ba731a42bdef7db228decc295934002",
                    registeredAddress: "48620833574f45a1a7eb3e68ee33598e515a83c93b2e43509cc52a1fe96c4873816f5d4206f04aefa54ac5",
                    businessAddress: "a998af",
                    businessScope: "8bd16a187528470f9d571b34c83589ad63a77a0bbb5f4183b5e52cead5a190fb880e01e575a44546927384777e235f",
                    description: "a88b50bc4e2949178dc5f8270e249949a86",
                    completeTxId: "43a02c18cb124ebf89871cd633494bc02e2f5d189a47482299eb45dfe9530fc39930375f6edd485",
                    commitUserName: "d24d775dac704e2bbec89312eeb9cb6798dde8bbf34a4f15b1939b454d4a6dc413223c7d625241beb61d59e4fedd24ffff"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}