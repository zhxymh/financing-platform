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
            result.Items.Any(x => x.Id == Guid.Parse("5ec03ba0-82da-4fc7-85c6-67d5955baa61")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("401bb822-9cef-4c0c-b676-5f55cdf4ac97")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterprisesAppService.GetAsync(Guid.Parse("5ec03ba0-82da-4fc7-85c6-67d5955baa61"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5ec03ba0-82da-4fc7-85c6-67d5955baa61"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseCreateDto
            {
                EnterpriseName = "48034cfdf0234f4bb20e2ec",
                ArtificialPerson = "112bad0978c8410b8678d660b39a2139b5b68789b5c24a478bf9",
                EstablishedTime = 2116036478,
                DueTime = 991370213,
                CreditCode = "7d5ab7934fbc440e89fbc7f2fa1ca6e14ae21881b79",
                ArtificialPersonId = "97f12fac20fe433eaed39",
                RegisteredCapital = "acfbccfd8b274a249dbb79aafa9c1c9021a1e3bf660444e8b58c69a1bca7",
                PhoneNumber = "bbdf47fff33e435c891eeb49c0a3cb8d97cd4bccef274e7aaa36",
                CertPhotoPath = "121e459ab39f483a8b0",
                IdPhotoPath1 = "c8f7c34657ea44c69b97ab7694b6f3b349c819974fc34fba8cd6b16e5766af39fd4c48aa58ea",
                IdPhotoPath2 = "6b89959c4dea4d5a8704cfeea17db4adaac7e8bb73174f969df6fa0cfa",
                CertificateStatus = default,
                CertificateTxId = "319d021b2dd1486bace2879614ba0d3dbcd886faa50f4953a1a1c97d160bfae3583db",
                ConfirmCertificateTxId = "d3d7ae908"
            };

            // Act
            var serviceResult = await _enterprisesAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("48034cfdf0234f4bb20e2ec");
            result.ArtificialPerson.ShouldBe("112bad0978c8410b8678d660b39a2139b5b68789b5c24a478bf9");
            result.EstablishedTime.ShouldBe(2116036478);
            result.DueTime.ShouldBe(991370213);
            result.CreditCode.ShouldBe("7d5ab7934fbc440e89fbc7f2fa1ca6e14ae21881b79");
            result.ArtificialPersonId.ShouldBe("97f12fac20fe433eaed39");
            result.RegisteredCapital.ShouldBe("acfbccfd8b274a249dbb79aafa9c1c9021a1e3bf660444e8b58c69a1bca7");
            result.PhoneNumber.ShouldBe("bbdf47fff33e435c891eeb49c0a3cb8d97cd4bccef274e7aaa36");
            result.CertPhotoPath.ShouldBe("121e459ab39f483a8b0");
            result.IdPhotoPath1.ShouldBe("c8f7c34657ea44c69b97ab7694b6f3b349c819974fc34fba8cd6b16e5766af39fd4c48aa58ea");
            result.IdPhotoPath2.ShouldBe("6b89959c4dea4d5a8704cfeea17db4adaac7e8bb73174f969df6fa0cfa");
            result.CertificateStatus.ShouldBe(default);
            result.CertificateTxId.ShouldBe("319d021b2dd1486bace2879614ba0d3dbcd886faa50f4953a1a1c97d160bfae3583db");
            result.ConfirmCertificateTxId.ShouldBe("d3d7ae908");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseUpdateDto()
            {
                EnterpriseName = "0dc3902d39d94b86b6ef6983b1c3598fab4c5dff00dd4ee18faaf2e43a24a7ab3b32c0ce19e4",
                ArtificialPerson = "644e20ee6c7a4b19b3",
                EstablishedTime = 170148743,
                DueTime = 220270986,
                CreditCode = "b00b266d5699444893cbe14befd402a9f96",
                ArtificialPersonId = "40c59cc6855a443ebc2fc9b9860199b315f29bfc3b734a2",
                RegisteredCapital = "8c3006caeffd4f89bce3bc523775c3a02db936c161bf4575ae8eb60c81",
                PhoneNumber = "117f8dc251e44bebae9a0abc4b844ad4127e792c35d547b696db98e1a972a7c13772d09a63f04",
                CertPhotoPath = "923885277b164543b7f568cf3dc89f3e3389fbea8f21478f958509d1a15b",
                IdPhotoPath1 = "15aa812c462149fca4b7433a",
                IdPhotoPath2 = "e6bc8054eef942b08eee2e30a9f14d70166cdcb658e",
                CertificateStatus = default,
                CertificateTxId = "7dc96a68e818417cb1edd1375e677e208e380777dcba41bf9c5047f5d391ed0668f2ee37c4044a51884aa3a52a",
                ConfirmCertificateTxId = "8c7894a3245b470a808862915c9008f8b10cb2d54c234efabd18"
            };

            // Act
            var serviceResult = await _enterprisesAppService.UpdateAsync(Guid.Parse("5ec03ba0-82da-4fc7-85c6-67d5955baa61"), input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("0dc3902d39d94b86b6ef6983b1c3598fab4c5dff00dd4ee18faaf2e43a24a7ab3b32c0ce19e4");
            result.ArtificialPerson.ShouldBe("644e20ee6c7a4b19b3");
            result.EstablishedTime.ShouldBe(170148743);
            result.DueTime.ShouldBe(220270986);
            result.CreditCode.ShouldBe("b00b266d5699444893cbe14befd402a9f96");
            result.ArtificialPersonId.ShouldBe("40c59cc6855a443ebc2fc9b9860199b315f29bfc3b734a2");
            result.RegisteredCapital.ShouldBe("8c3006caeffd4f89bce3bc523775c3a02db936c161bf4575ae8eb60c81");
            result.PhoneNumber.ShouldBe("117f8dc251e44bebae9a0abc4b844ad4127e792c35d547b696db98e1a972a7c13772d09a63f04");
            result.CertPhotoPath.ShouldBe("923885277b164543b7f568cf3dc89f3e3389fbea8f21478f958509d1a15b");
            result.IdPhotoPath1.ShouldBe("15aa812c462149fca4b7433a");
            result.IdPhotoPath2.ShouldBe("e6bc8054eef942b08eee2e30a9f14d70166cdcb658e");
            result.CertificateStatus.ShouldBe(default);
            result.CertificateTxId.ShouldBe("7dc96a68e818417cb1edd1375e677e208e380777dcba41bf9c5047f5d391ed0668f2ee37c4044a51884aa3a52a");
            result.ConfirmCertificateTxId.ShouldBe("8c7894a3245b470a808862915c9008f8b10cb2d54c234efabd18");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterprisesAppService.DeleteAsync(Guid.Parse("5ec03ba0-82da-4fc7-85c6-67d5955baa61"));

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == Guid.Parse("5ec03ba0-82da-4fc7-85c6-67d5955baa61"));

            result.ShouldBeNull();
        }
    }
}