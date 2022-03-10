using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tank.Financing.Enterprises
{
    public class EnterprisesAppServiceTests : FinancingApplicationTestBase
    {
        private readonly IEnterprisesAppService _enterprisesAppService;
        private readonly IRepository<Enterprise, Guid> _enterpriseRepository;

        public EnterprisesAppServiceTests()
        {
            _enterprisesAppService = GetRequiredService<IEnterprisesAppService>();
            _enterpriseRepository = GetRequiredService<IRepository<Enterprise, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _enterprisesAppService.GetListAsync(new GetEnterprisesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("4b826053-560b-4e05-8560-2e6614ec7d71")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("8cce2673-4bad-4744-b1cd-4a1fa3b81586")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterprisesAppService.GetAsync(Guid.Parse("4b826053-560b-4e05-8560-2e6614ec7d71"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4b826053-560b-4e05-8560-2e6614ec7d71"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseCreateDto
            {
                EnterpriseName = "23682f73ac6349ad8bbb8e973f0a04392",
                ArtificialPerson = "470912b9e0e34c2e97a564094ce46a5cbd75d0326c044ea297d1329ff11f18db20f38a890ea6410ca786",
                EstablishedTime = 1821400234,
                DueTime = 2092898356,
                CreditCode = "56ca248359524bfe9e755960124fdcca1183d645f6d545a6bbf6c31f33995fecd0ed28384fe8439d",
                ArtificialPersonId = "fe2f24884a044dd786d8ad8793c14953ede70bc6ce07482bb80f111104a83155201b9ad889ea434baaecbb3c4e08",
                RegisteredCapital = "fc135467c4254bba8c3dab49afb35e226",
                PhoneNumber = "e08808712f764159b3e231832f1bae593002597b2ee2401fbdd531eac6616c69a477e32535ea454394cd9cb88d3518ec",
                CertPhotoPath = "faa09ae025674c99853e40eead2888f13",
                IdPhotoPath1 = "a9d025257ac747daac90",
                IdPhotoPath2 = "e840b7",
                CertificateStatus = default
            };

            // Act
            var serviceResult = await _enterprisesAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("23682f73ac6349ad8bbb8e973f0a04392");
            result.ArtificialPerson.ShouldBe("470912b9e0e34c2e97a564094ce46a5cbd75d0326c044ea297d1329ff11f18db20f38a890ea6410ca786");
            result.EstablishedTime.ShouldBe(1821400234);
            result.DueTime.ShouldBe(2092898356);
            result.CreditCode.ShouldBe("56ca248359524bfe9e755960124fdcca1183d645f6d545a6bbf6c31f33995fecd0ed28384fe8439d");
            result.ArtificialPersonId.ShouldBe("fe2f24884a044dd786d8ad8793c14953ede70bc6ce07482bb80f111104a83155201b9ad889ea434baaecbb3c4e08");
            result.RegisteredCapital.ShouldBe("fc135467c4254bba8c3dab49afb35e226");
            result.PhoneNumber.ShouldBe("e08808712f764159b3e231832f1bae593002597b2ee2401fbdd531eac6616c69a477e32535ea454394cd9cb88d3518ec");
            result.CertPhotoPath.ShouldBe("faa09ae025674c99853e40eead2888f13");
            result.IdPhotoPath1.ShouldBe("a9d025257ac747daac90");
            result.IdPhotoPath2.ShouldBe("e840b7");
            result.CertificateStatus.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseUpdateDto()
            {
                EnterpriseName = "5630704bf4804b5fa7",
                ArtificialPerson = "70006ea239db4cf19b82a07d",
                EstablishedTime = 881043868,
                DueTime = 1738857392,
                CreditCode = "eca54730fe0c45a38d854f3ae282ab7c6e7d2e839c1a49219010f70593941137349450d51e10",
                ArtificialPersonId = "13a0bd3f5d6546978f1a892e05",
                RegisteredCapital = "5183cbd7dd82435e9eb89a18adbc6d7172392f39cc9249619c0e60577d2cb262d429f183dfb940d3be9dd5bc5739b",
                PhoneNumber = "e5cd9b14e4504252b170518a67be89f7e038116da5ff4be68629",
                CertPhotoPath = "950371ad0bdf48e68f6a8bfa441a",
                IdPhotoPath1 = "3bb0aed8a6d64d9e9b46316c6cb94d72aa59ca79ceda4693943023f9329bcfa5c62dfc65c0f24bb",
                IdPhotoPath2 = "5e755845fe734a9db1489f7d5d8aeadc8434f7fd44ce4aaab6a941ef61dbda3756f7ee",
                CertificateStatus = default
            };

            // Act
            var serviceResult = await _enterprisesAppService.UpdateAsync(Guid.Parse("4b826053-560b-4e05-8560-2e6614ec7d71"), input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("5630704bf4804b5fa7");
            result.ArtificialPerson.ShouldBe("70006ea239db4cf19b82a07d");
            result.EstablishedTime.ShouldBe(881043868);
            result.DueTime.ShouldBe(1738857392);
            result.CreditCode.ShouldBe("eca54730fe0c45a38d854f3ae282ab7c6e7d2e839c1a49219010f70593941137349450d51e10");
            result.ArtificialPersonId.ShouldBe("13a0bd3f5d6546978f1a892e05");
            result.RegisteredCapital.ShouldBe("5183cbd7dd82435e9eb89a18adbc6d7172392f39cc9249619c0e60577d2cb262d429f183dfb940d3be9dd5bc5739b");
            result.PhoneNumber.ShouldBe("e5cd9b14e4504252b170518a67be89f7e038116da5ff4be68629");
            result.CertPhotoPath.ShouldBe("950371ad0bdf48e68f6a8bfa441a");
            result.IdPhotoPath1.ShouldBe("3bb0aed8a6d64d9e9b46316c6cb94d72aa59ca79ceda4693943023f9329bcfa5c62dfc65c0f24bb");
            result.IdPhotoPath2.ShouldBe("5e755845fe734a9db1489f7d5d8aeadc8434f7fd44ce4aaab6a941ef61dbda3756f7ee");
            result.CertificateStatus.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterprisesAppService.DeleteAsync(Guid.Parse("4b826053-560b-4e05-8560-2e6614ec7d71"));

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == Guid.Parse("4b826053-560b-4e05-8560-2e6614ec7d71"));

            result.ShouldBeNull();
        }
    }
}