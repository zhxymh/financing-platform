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
            result.Items.Any(x => x.Id == Guid.Parse("182f027c-7825-4bf5-ad49-f4a8d78f5969")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("8c950318-ded1-493f-ac4b-53a2fc4388e1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterprisesAppService.GetAsync(Guid.Parse("182f027c-7825-4bf5-ad49-f4a8d78f5969"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("182f027c-7825-4bf5-ad49-f4a8d78f5969"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseCreateDto
            {
                EnterpriseName = "37fd1ba91d8f4aaea73f4d86d29770df7537cdfe047f4382af04097e455fa9fa866489e261824760ac60",
                ArtificialPerson = "5dd16e2367b24b57a460f",
                EstablishedTime = 1641496478,
                DueTime = 118469393,
                CreditCode = "470c40f6af0340cab4acbee70858019ff7eeee958dad499ba7ec1ba4dc13457f86692548c05b4e6c81055342a03a5",
                ArtificialPersonId = "1e47754ffd9c4d0f8871388490db6cc3ec46171ad7da4ea1a2ae814b3b989fa9",
                RegisteredCapital = "b250a7c97a2946478bd3fecd916c184581d0105c3fb34c0ca125df557aaed87894918dc29b0d4290b3",
                PhoneNumber = "e5438a7544824529ad61",
                CertPhotoPath = "df8d29469d0d4859bd",
                IdPhotoPath1 = "74ca31871c384226a2b463f515e0c7a41695926b7c20450aafe30f51a399106c82900d",
                IdPhotoPath2 = "5c337caff92e4789a4c445cf214216afd02e07612b8a417892fd2",
                CertificateStatus = default
            };

            // Act
            var serviceResult = await _enterprisesAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("37fd1ba91d8f4aaea73f4d86d29770df7537cdfe047f4382af04097e455fa9fa866489e261824760ac60");
            result.ArtificialPerson.ShouldBe("5dd16e2367b24b57a460f");
            result.EstablishedTime.ShouldBe(1641496478);
            result.DueTime.ShouldBe(118469393);
            result.CreditCode.ShouldBe("470c40f6af0340cab4acbee70858019ff7eeee958dad499ba7ec1ba4dc13457f86692548c05b4e6c81055342a03a5");
            result.ArtificialPersonId.ShouldBe("1e47754ffd9c4d0f8871388490db6cc3ec46171ad7da4ea1a2ae814b3b989fa9");
            result.RegisteredCapital.ShouldBe("b250a7c97a2946478bd3fecd916c184581d0105c3fb34c0ca125df557aaed87894918dc29b0d4290b3");
            result.PhoneNumber.ShouldBe("e5438a7544824529ad61");
            result.CertPhotoPath.ShouldBe("df8d29469d0d4859bd");
            result.IdPhotoPath1.ShouldBe("74ca31871c384226a2b463f515e0c7a41695926b7c20450aafe30f51a399106c82900d");
            result.IdPhotoPath2.ShouldBe("5c337caff92e4789a4c445cf214216afd02e07612b8a417892fd2");
            result.CertificateStatus.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseUpdateDto()
            {
                EnterpriseName = "546b59ecfde14eb988c40bfa40a0a05b86b0d52b8105490b8c31df1a3cac5ecd349dd490615c424196",
                ArtificialPerson = "6da520b59a3d4aa78685b0ba6b728ecacfe082af5ad445a2aa2b15c54aaadd8aa16e65329ce94d11b31259199aac48f",
                EstablishedTime = 673106177,
                DueTime = 65797844,
                CreditCode = "649480d2ed2d49e6ac1e7bec8e515e8a16bbf566366f4a459007c51fd6e222028597bbc25a04",
                ArtificialPersonId = "d5603b7cced",
                RegisteredCapital = "ad80232784eb46e490c88c092970ae",
                PhoneNumber = "7692cf55832840fba318fad9c67cfd48",
                CertPhotoPath = "5cb904e26b7446d29b16957ae31cd1c5d85a65b2121c4bdf",
                IdPhotoPath1 = "8864c8e7e79d4b06a56cc4e5b1a72a16ad4f3c33bb92473892854291485ae108b29e8b32a0684",
                IdPhotoPath2 = "b3a18b58d6644f78a00a0109fcb75cfef005b0eaa8124dff8a3f9f1a201bc1c89cb6880041084d2aad6df0b7e692fbe49",
                CertificateStatus = default
            };

            // Act
            var serviceResult = await _enterprisesAppService.UpdateAsync(Guid.Parse("182f027c-7825-4bf5-ad49-f4a8d78f5969"), input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("546b59ecfde14eb988c40bfa40a0a05b86b0d52b8105490b8c31df1a3cac5ecd349dd490615c424196");
            result.ArtificialPerson.ShouldBe("6da520b59a3d4aa78685b0ba6b728ecacfe082af5ad445a2aa2b15c54aaadd8aa16e65329ce94d11b31259199aac48f");
            result.EstablishedTime.ShouldBe(673106177);
            result.DueTime.ShouldBe(65797844);
            result.CreditCode.ShouldBe("649480d2ed2d49e6ac1e7bec8e515e8a16bbf566366f4a459007c51fd6e222028597bbc25a04");
            result.ArtificialPersonId.ShouldBe("d5603b7cced");
            result.RegisteredCapital.ShouldBe("ad80232784eb46e490c88c092970ae");
            result.PhoneNumber.ShouldBe("7692cf55832840fba318fad9c67cfd48");
            result.CertPhotoPath.ShouldBe("5cb904e26b7446d29b16957ae31cd1c5d85a65b2121c4bdf");
            result.IdPhotoPath1.ShouldBe("8864c8e7e79d4b06a56cc4e5b1a72a16ad4f3c33bb92473892854291485ae108b29e8b32a0684");
            result.IdPhotoPath2.ShouldBe("b3a18b58d6644f78a00a0109fcb75cfef005b0eaa8124dff8a3f9f1a201bc1c89cb6880041084d2aad6df0b7e692fbe49");
            result.CertificateStatus.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterprisesAppService.DeleteAsync(Guid.Parse("182f027c-7825-4bf5-ad49-f4a8d78f5969"));

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == Guid.Parse("182f027c-7825-4bf5-ad49-f4a8d78f5969"));

            result.ShouldBeNull();
        }
    }
}