using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetailsAppServiceTests : FinancingApplicationTestBase
    {
        private readonly IEnterpriseDetailsAppService _enterpriseDetailsAppService;
        private readonly IRepository<EnterpriseDetail, Guid> _enterpriseDetailRepository;

        public EnterpriseDetailsAppServiceTests()
        {
            _enterpriseDetailsAppService = GetRequiredService<IEnterpriseDetailsAppService>();
            _enterpriseDetailRepository = GetRequiredService<IRepository<EnterpriseDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _enterpriseDetailsAppService.GetListAsync(new GetEnterpriseDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("98cef5d2-574e-429e-92f1-c317905b39f6")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d171d866-5730-4bcc-ae88-e40661c6710c")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterpriseDetailsAppService.GetAsync(Guid.Parse("98cef5d2-574e-429e-92f1-c317905b39f6"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("98cef5d2-574e-429e-92f1-c317905b39f6"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseDetailCreateDto
            {
                EnterpriseName = "bae09b6772344a0bbecf3b43e1ca78f8d435cc16b0ec4753a09a244a544de6bde083d519514f4",
                TotalAssets = "edc23fe6c3a542b8b3517d29f750b7240",
                Income = "8f167699e6f54b46a4dfbcd1903cc0ce1b69cc3cefaf405f8a419811be0b1f1bd2",
                EnterpriseType = "43e23e3c22",
                StaffNumber = 472460684,
                Industry = "d9f204386cd1",
                Location = "7d670d1125554c1ebebc0edb83d0fe532adf1e7abae643a9989d4bc18a33237998690bf870be483884",
                RegisteredAddress = "b8cf91c686224ab99",
                BusinessAddress = "77d13cd5a3fb4669878dafddfce775a25e4684d9dd824d899d1396d74e400c3bf6dd7c0ddadf4069a",
                BusinessScope = "83e3abec879e4fe8959f962c08312f07de5351775df0411e960bfbb6e63fb060a498a3c3088443",
                Description = "1f901ab0625c4688a1ded90d"
            };

            // Act
            var serviceResult = await _enterpriseDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("bae09b6772344a0bbecf3b43e1ca78f8d435cc16b0ec4753a09a244a544de6bde083d519514f4");
            result.TotalAssets.ShouldBe("edc23fe6c3a542b8b3517d29f750b7240");
            result.Income.ShouldBe("8f167699e6f54b46a4dfbcd1903cc0ce1b69cc3cefaf405f8a419811be0b1f1bd2");
            result.EnterpriseType.ShouldBe("43e23e3c22");
            result.StaffNumber.ShouldBe(472460684);
            result.Industry.ShouldBe("d9f204386cd1");
            result.Location.ShouldBe("7d670d1125554c1ebebc0edb83d0fe532adf1e7abae643a9989d4bc18a33237998690bf870be483884");
            result.RegisteredAddress.ShouldBe("b8cf91c686224ab99");
            result.BusinessAddress.ShouldBe("77d13cd5a3fb4669878dafddfce775a25e4684d9dd824d899d1396d74e400c3bf6dd7c0ddadf4069a");
            result.BusinessScope.ShouldBe("83e3abec879e4fe8959f962c08312f07de5351775df0411e960bfbb6e63fb060a498a3c3088443");
            result.Description.ShouldBe("1f901ab0625c4688a1ded90d");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseDetailUpdateDto()
            {
                EnterpriseName = "cbd5fc79e76a4cb48881edcbb168ea699c7dcfc58d8449858ca242bda06cbb98878a62146a344596b0ca84d5425",
                TotalAssets = "287147eb352c478890569f84a2165a8a49430e4c1d92438e8c5deedd2fabeb7fb6b9741",
                Income = "3afc32ed7a304e90b2c81fc87d84de1ec11756853f364",
                EnterpriseType = "dc733265ca0a44f0848d2d5b",
                StaffNumber = 1460840259,
                Industry = "edf805af63",
                Location = "a67f24531761",
                RegisteredAddress = "2bf5ed451f5645759973439",
                BusinessAddress = "03583b1f18754b76b96b8b3046f",
                BusinessScope = "7042aec629d04f82acef6d9fdd2d26b3cfed893e9d50406995c6e56893b2e3f37e91b5",
                Description = "86cc55732669421db9f6cf00963362674d22605f3ee5445aa2645c88719606d5315496"
            };

            // Act
            var serviceResult = await _enterpriseDetailsAppService.UpdateAsync(Guid.Parse("98cef5d2-574e-429e-92f1-c317905b39f6"), input);

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("cbd5fc79e76a4cb48881edcbb168ea699c7dcfc58d8449858ca242bda06cbb98878a62146a344596b0ca84d5425");
            result.TotalAssets.ShouldBe("287147eb352c478890569f84a2165a8a49430e4c1d92438e8c5deedd2fabeb7fb6b9741");
            result.Income.ShouldBe("3afc32ed7a304e90b2c81fc87d84de1ec11756853f364");
            result.EnterpriseType.ShouldBe("dc733265ca0a44f0848d2d5b");
            result.StaffNumber.ShouldBe(1460840259);
            result.Industry.ShouldBe("edf805af63");
            result.Location.ShouldBe("a67f24531761");
            result.RegisteredAddress.ShouldBe("2bf5ed451f5645759973439");
            result.BusinessAddress.ShouldBe("03583b1f18754b76b96b8b3046f");
            result.BusinessScope.ShouldBe("7042aec629d04f82acef6d9fdd2d26b3cfed893e9d50406995c6e56893b2e3f37e91b5");
            result.Description.ShouldBe("86cc55732669421db9f6cf00963362674d22605f3ee5445aa2645c88719606d5315496");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterpriseDetailsAppService.DeleteAsync(Guid.Parse("98cef5d2-574e-429e-92f1-c317905b39f6"));

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == Guid.Parse("98cef5d2-574e-429e-92f1-c317905b39f6"));

            result.ShouldBeNull();
        }
    }
}