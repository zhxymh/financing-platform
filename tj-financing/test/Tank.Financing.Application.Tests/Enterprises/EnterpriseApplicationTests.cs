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
            result.Items.Any(x => x.Id == Guid.Parse("de3a0132-54e0-422e-9bab-d7baf1e06286")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("e208ab7d-1e29-4bac-882d-2b685e99cb63")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterprisesAppService.GetAsync(Guid.Parse("de3a0132-54e0-422e-9bab-d7baf1e06286"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("de3a0132-54e0-422e-9bab-d7baf1e06286"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseCreateDto
            {
                EnterpriseName = "58724236a9264ced85d60cac288f560c67c197997",
                ArtificialPerson = "3d8d9cc4295b47998d5a30f6e4a4e07def307879bfaf490ca1229d82c4e15d5e4f900c86fe1544d199c38ac5ca305a71a0",
                EstablishedTime = 1750107286,
                DueTime = 1379332887,
                CreditCode = "5ed0868e0fba4854bcf6219d23ce3",
                ArtificialPersonId = "c512a8a329ef4f8e9f5c9c9ff59",
                RegisteredCapital = "1ca7359e74e14fffa06a97d8e63",
                PhoneNumber = "7db7d82f1bbe4b678cb43685896901faaeff30f9e1fe48e09c51145a",
                CertPhotoPath = "7505648a9109403b97af8c9dbe364aa91b40a19b909c454",
                IdPhotoPath1 = "8318b3a856d84fd89be26d5429a835bea44cca0c9a1146",
                IdPhotoPath2 = "10ce864493ef4e56a19cbdc8a0533323425e8999c9114b0ba84d986e1ed263e",
                CertificateStatus = default,
                CertificateTxId = "2f321b",
                ConfirmCertificateTxId = "2f6e04a517d046d0b636497902926262f5d7e917700d4d10a0068513e19702a00067daf5432f49e293551626d5b3c2d84b6",
                CommitUserName = "89352dbe030b4c10b8ce7ebf21c7a3df2d3f17f3de1042c5a092c46e3d36cefd4f274ed81f03448f"
            };

            // Act
            var serviceResult = await _enterprisesAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("58724236a9264ced85d60cac288f560c67c197997");
            result.ArtificialPerson.ShouldBe("3d8d9cc4295b47998d5a30f6e4a4e07def307879bfaf490ca1229d82c4e15d5e4f900c86fe1544d199c38ac5ca305a71a0");
            result.EstablishedTime.ShouldBe(1750107286);
            result.DueTime.ShouldBe(1379332887);
            result.CreditCode.ShouldBe("5ed0868e0fba4854bcf6219d23ce3");
            result.ArtificialPersonId.ShouldBe("c512a8a329ef4f8e9f5c9c9ff59");
            result.RegisteredCapital.ShouldBe("1ca7359e74e14fffa06a97d8e63");
            result.PhoneNumber.ShouldBe("7db7d82f1bbe4b678cb43685896901faaeff30f9e1fe48e09c51145a");
            result.CertPhotoPath.ShouldBe("7505648a9109403b97af8c9dbe364aa91b40a19b909c454");
            result.IdPhotoPath1.ShouldBe("8318b3a856d84fd89be26d5429a835bea44cca0c9a1146");
            result.IdPhotoPath2.ShouldBe("10ce864493ef4e56a19cbdc8a0533323425e8999c9114b0ba84d986e1ed263e");
            result.CertificateStatus.ShouldBe(default);
            result.CertificateTxId.ShouldBe("2f321b");
            result.ConfirmCertificateTxId.ShouldBe("2f6e04a517d046d0b636497902926262f5d7e917700d4d10a0068513e19702a00067daf5432f49e293551626d5b3c2d84b6");
            result.CommitUserName.ShouldBe("89352dbe030b4c10b8ce7ebf21c7a3df2d3f17f3de1042c5a092c46e3d36cefd4f274ed81f03448f");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseUpdateDto()
            {
                EnterpriseName = "1d2e3a49a21a45c0afc9b3f03d2d1e18c4838d22bfeb419683f93f40ea8f48f",
                ArtificialPerson = "55421e9f765b4d7986efc9f79c7ff17a420905ca7413431485fc6523981bd8de907382ff184",
                EstablishedTime = 2115016529,
                DueTime = 283975905,
                CreditCode = "fcf2fb0821384d18bf6811530536705a8c1312b101a24ba094943641b65655f932e8975ebfcb4f0b93452d4c",
                ArtificialPersonId = "e18219d2252f4389b5b53c9f0cb9ef92016969dbffc248d0bf283e7dca93a74d1a726ed61fd94aa",
                RegisteredCapital = "8c40b002e6c1436",
                PhoneNumber = "77784e",
                CertPhotoPath = "75cd843aa4f548fcbd554ff0f74cd66f7e6532307610499880",
                IdPhotoPath1 = "ff4ff8",
                IdPhotoPath2 = "effed4ae5f17421385cc66f21d3b496a79602142ca294b389c3bbd613925b81b1570e8324932486dbe213e7a",
                CertificateStatus = default,
                CertificateTxId = "b21b20c",
                ConfirmCertificateTxId = "53a6d09",
                CommitUserName = "02390f2f634d4ce491176678e8765af6ceb480ad05ee441f8d78e7b3d170e21a7448e42beab94391a59fd6e896"
            };

            // Act
            var serviceResult = await _enterprisesAppService.UpdateAsync(Guid.Parse("de3a0132-54e0-422e-9bab-d7baf1e06286"), input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("1d2e3a49a21a45c0afc9b3f03d2d1e18c4838d22bfeb419683f93f40ea8f48f");
            result.ArtificialPerson.ShouldBe("55421e9f765b4d7986efc9f79c7ff17a420905ca7413431485fc6523981bd8de907382ff184");
            result.EstablishedTime.ShouldBe(2115016529);
            result.DueTime.ShouldBe(283975905);
            result.CreditCode.ShouldBe("fcf2fb0821384d18bf6811530536705a8c1312b101a24ba094943641b65655f932e8975ebfcb4f0b93452d4c");
            result.ArtificialPersonId.ShouldBe("e18219d2252f4389b5b53c9f0cb9ef92016969dbffc248d0bf283e7dca93a74d1a726ed61fd94aa");
            result.RegisteredCapital.ShouldBe("8c40b002e6c1436");
            result.PhoneNumber.ShouldBe("77784e");
            result.CertPhotoPath.ShouldBe("75cd843aa4f548fcbd554ff0f74cd66f7e6532307610499880");
            result.IdPhotoPath1.ShouldBe("ff4ff8");
            result.IdPhotoPath2.ShouldBe("effed4ae5f17421385cc66f21d3b496a79602142ca294b389c3bbd613925b81b1570e8324932486dbe213e7a");
            result.CertificateStatus.ShouldBe(default);
            result.CertificateTxId.ShouldBe("b21b20c");
            result.ConfirmCertificateTxId.ShouldBe("53a6d09");
            result.CommitUserName.ShouldBe("02390f2f634d4ce491176678e8765af6ceb480ad05ee441f8d78e7b3d170e21a7448e42beab94391a59fd6e896");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterprisesAppService.DeleteAsync(Guid.Parse("de3a0132-54e0-422e-9bab-d7baf1e06286"));

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == Guid.Parse("de3a0132-54e0-422e-9bab-d7baf1e06286"));

            result.ShouldBeNull();
        }
    }
}