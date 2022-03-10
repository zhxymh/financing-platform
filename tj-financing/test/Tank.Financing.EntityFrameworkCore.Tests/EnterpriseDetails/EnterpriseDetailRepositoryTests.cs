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
                    enterpriseName: "bc54bd640e5d4f69891f520d03976604aca3d7938d2b4409b33f8fb56da71dad88f27da",
                    totalAssets: "e821ba69d7fa49e582d1f85b015b690b6c46ba835e28410a9ed51898664f2b8057b966db7cc94e41a1e21f51c257cb",
                    income: "afa1d213317d45a3aadcec6b3209ca55c998761bbc0544bb8",
                    enterpriseType: "9a8e39deb5604e52adfd5df31d7ec240775a5fb282134bf0ac9d168c1b48cf5cf1e",
                    industry: "9061a4bf4f8e480c98641936da811924e6b2e8f39e0f47c9b26e884057b07c3c53d696e5b2",
                    location: "a6a0bd6677284bfe842fb06f",
                    registeredAddress: "51959d027d274363bcc1a5943f9bddc842c2d04bd2444cd2a63b612c1c5fdcecf2a1ed6e1e574b5abbdcb23ba1c638ac",
                    businessAddress: "5ed771d8cee14b428d28a73b7ffb3fe6d51686800c1b42e2b0f553d1abed4ec23f63631",
                    businessScope: "f8dcd79bb8d44bab9961f4abc89fb366205eb300557142edbf0392bc89939e76496b99ddad5f",
                    description: "02e11b2106e5431ab261d7c88d57e7a"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("eb01b4d9-7856-46bf-8a4d-48db4dd41dac"));
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
                    enterpriseName: "dadafc84538c471a9dc9d3ed935d652f2de2b43806924469b5669c76f85685dd5b7448ff8b434",
                    totalAssets: "a425cb023da64b6085aeda2858181f25e416838fafbd4a62846fea407dd21a2b80c484f6c54b4f4d867",
                    income: "82177390ffc446218e70e17a7c65cc43ecefaecfbd204b03905",
                    enterpriseType: "5408e2a7ea9e4542a99dc55f3f3ddb76b077ac6af6a3405eb4c80f655899a08c1e70add4",
                    industry: "25ebcbefd9",
                    location: "4dfc4164bd694e58b0dbdb5a372563242fd667e203d1450",
                    registeredAddress: "f1f74bc2e3734aa49ce8cc7abf65c673a023e6",
                    businessAddress: "7522f93538334e29a47fca979c820d9fe8c69097d5564f2eb01b974c84d17d1f9475fe493b9c4ca8b966eec823192c577",
                    businessScope: "7dc71bf36ebe4efb8c2fea70ea388",
                    description: "fec8fb213dbd47438ee1668f210f081738439d0ff6704a14b52fc113f12"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}