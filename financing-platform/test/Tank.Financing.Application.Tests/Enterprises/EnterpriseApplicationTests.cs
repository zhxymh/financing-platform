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
            result.Items.Any(x => x.Id == Guid.Parse("4c390729-1064-4b40-92b0-8058ffdff0e0")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("3edecb90-a45c-465b-937d-3cb7e96803e9")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterprisesAppService.GetAsync(Guid.Parse("4c390729-1064-4b40-92b0-8058ffdff0e0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4c390729-1064-4b40-92b0-8058ffdff0e0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseCreateDto
            {
                EnterpriseName = "7ce854ae01bc4d9480b98781d3bfa1",
                ArtificialPerson = "91e912c01c334fd29d617eff133d12a7d34a5",
                EstablishedTime = "6b1ecf2417fd4dd294e581462c6e847392fc8de94132486b92dbc478198f0ee89f6615dc46bd40868c1e0fb9",
                DueTime = "cd3bfa0fd91b483fadfea5f231c00778a388a175998946528d6d156fafe86d6a5c53cd61cfdf4c348043",
                CreditCode = "2028e7c893934261a20c3036bd0e0a3371a85ea1892b4a8fa3e34bde9d2757156e1",
                ArtificialPersonId = "2aa0c29b752c438b803ffa1edee77c360f87679a43f4413c80a43e65",
                RegisteredCapital = "0df244cde8704173bbc001",
                PhoneNumber = "cbeb9d92f2f3456287168ff58e71ea069e41aa471f0a4442878c98ac7",
                CertPhotoPath = "5e2dfb5f53c84dd392",
                IdPhotoPath1 = "49c3621b42f",
                IdPhotoPath2 = "f3e44ef21b5842e69672035fe5eece4e9bf2aa0bf775434ebe97c9d3b3d8dfc7dcac7dc46c974b4d82495c319c",
                CertificateStatus = "6260ea26b57e44"
            };

            // Act
            var serviceResult = await _enterprisesAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("7ce854ae01bc4d9480b98781d3bfa1");
            result.ArtificialPerson.ShouldBe("91e912c01c334fd29d617eff133d12a7d34a5");
            result.EstablishedTime.ShouldBe("6b1ecf2417fd4dd294e581462c6e847392fc8de94132486b92dbc478198f0ee89f6615dc46bd40868c1e0fb9");
            result.DueTime.ShouldBe("cd3bfa0fd91b483fadfea5f231c00778a388a175998946528d6d156fafe86d6a5c53cd61cfdf4c348043");
            result.CreditCode.ShouldBe("2028e7c893934261a20c3036bd0e0a3371a85ea1892b4a8fa3e34bde9d2757156e1");
            result.ArtificialPersonId.ShouldBe("2aa0c29b752c438b803ffa1edee77c360f87679a43f4413c80a43e65");
            result.RegisteredCapital.ShouldBe("0df244cde8704173bbc001");
            result.PhoneNumber.ShouldBe("cbeb9d92f2f3456287168ff58e71ea069e41aa471f0a4442878c98ac7");
            result.CertPhotoPath.ShouldBe("5e2dfb5f53c84dd392");
            result.IdPhotoPath1.ShouldBe("49c3621b42f");
            result.IdPhotoPath2.ShouldBe("f3e44ef21b5842e69672035fe5eece4e9bf2aa0bf775434ebe97c9d3b3d8dfc7dcac7dc46c974b4d82495c319c");
            result.CertificateStatus.ShouldBe("6260ea26b57e44");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseUpdateDto()
            {
                EnterpriseName = "7fcfe3e24402432d83f5c26c38973e6e55f66af1b9f441ed96e3686909c97",
                ArtificialPerson = "9ac3a03ae352486cae3680241b5d36e678df39e9af2a41668bda2f07ea4067ef88808d",
                EstablishedTime = "aef73a779a10446abf39",
                DueTime = "e320306f80fc40559fde3b3fcb234a3140d7c0714aae49ca8fa43e6c61f6ce7d0dfd8e",
                CreditCode = "21c603af1bac462c8f896967c1405f1218531a73602840c7a555d43ac9892b59703c091ca6d",
                ArtificialPersonId = "cf5a860ff6a14bb7",
                RegisteredCapital = "099a8899b8a84ea995a58efc566469f81e49c8dcab79468db8df06432",
                PhoneNumber = "f82b452f6cd948599137572ec77837a88f97b92cadb6402090ed65df0feb84b8829569a90b5348b6a7f65130",
                CertPhotoPath = "9f6fb676c14643299f652234fceb2a09fed47c185f0043708279898bf6349a45d710f1ef575646d",
                IdPhotoPath1 = "5b401686722e4a26b2b0fea22bc1c35c23aeb9c7d9a54a93b7dd8a426e31ef4cc2624b48d2b14a729",
                IdPhotoPath2 = "ffaf01b044a3414691149be3da2e282ffc1696953",
                CertificateStatus = "52ae318f78eb4baa9bd62f900c1d740a81ce20e82a2140dcac2c014b1da742cc51f24f6f8f774"
            };

            // Act
            var serviceResult = await _enterprisesAppService.UpdateAsync(Guid.Parse("4c390729-1064-4b40-92b0-8058ffdff0e0"), input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("7fcfe3e24402432d83f5c26c38973e6e55f66af1b9f441ed96e3686909c97");
            result.ArtificialPerson.ShouldBe("9ac3a03ae352486cae3680241b5d36e678df39e9af2a41668bda2f07ea4067ef88808d");
            result.EstablishedTime.ShouldBe("aef73a779a10446abf39");
            result.DueTime.ShouldBe("e320306f80fc40559fde3b3fcb234a3140d7c0714aae49ca8fa43e6c61f6ce7d0dfd8e");
            result.CreditCode.ShouldBe("21c603af1bac462c8f896967c1405f1218531a73602840c7a555d43ac9892b59703c091ca6d");
            result.ArtificialPersonId.ShouldBe("cf5a860ff6a14bb7");
            result.RegisteredCapital.ShouldBe("099a8899b8a84ea995a58efc566469f81e49c8dcab79468db8df06432");
            result.PhoneNumber.ShouldBe("f82b452f6cd948599137572ec77837a88f97b92cadb6402090ed65df0feb84b8829569a90b5348b6a7f65130");
            result.CertPhotoPath.ShouldBe("9f6fb676c14643299f652234fceb2a09fed47c185f0043708279898bf6349a45d710f1ef575646d");
            result.IdPhotoPath1.ShouldBe("5b401686722e4a26b2b0fea22bc1c35c23aeb9c7d9a54a93b7dd8a426e31ef4cc2624b48d2b14a729");
            result.IdPhotoPath2.ShouldBe("ffaf01b044a3414691149be3da2e282ffc1696953");
            result.CertificateStatus.ShouldBe("52ae318f78eb4baa9bd62f900c1d740a81ce20e82a2140dcac2c014b1da742cc51f24f6f8f774");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterprisesAppService.DeleteAsync(Guid.Parse("4c390729-1064-4b40-92b0-8058ffdff0e0"));

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == Guid.Parse("4c390729-1064-4b40-92b0-8058ffdff0e0"));

            result.ShouldBeNull();
        }
    }
}