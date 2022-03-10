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
            result.Items.Any(x => x.Id == Guid.Parse("8de2be16-5016-449b-bab0-34d82b0ad7d5")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("2ae7b95d-97bc-4035-9a51-59863a3f9ba6")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterpriseDetailsAppService.GetAsync(Guid.Parse("8de2be16-5016-449b-bab0-34d82b0ad7d5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("8de2be16-5016-449b-bab0-34d82b0ad7d5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseDetailCreateDto
            {
                EnterpriseName = "ef316dec5ac34a729173b7d62489e",
                TotalAssets = "8c8683d398584dc2af3875a8",
                Income = "2e5adb3507354eeba71b609873b38e721e8b1bce767d40328b97e5",
                EnterpriseType = "214143416f90450eabcc6fb11318b43b2",
                StaffNumber = 1307243550,
                Industry = "dccb9ef5cf6b4c978dd53d3be1353be31eb43390e98b4e349a0f8f5a89e4db6302a0",
                Location = "8734010164634",
                RegisteredAddress = "17d78a1cf304463ab42fd466cbb9",
                BusinessAddress = "aa85a0e3f79e49",
                BusinessScope = "09b53ff1e2164328b66b592b5bb8da5aece8eaa28886415782ea45",
                Description = "6673072b801f401382af9e8945d42efb60d02aa9",
                CompleteTxId = "dfd452e5293f435386f9242577"
            };

            // Act
            var serviceResult = await _enterpriseDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("ef316dec5ac34a729173b7d62489e");
            result.TotalAssets.ShouldBe("8c8683d398584dc2af3875a8");
            result.Income.ShouldBe("2e5adb3507354eeba71b609873b38e721e8b1bce767d40328b97e5");
            result.EnterpriseType.ShouldBe("214143416f90450eabcc6fb11318b43b2");
            result.StaffNumber.ShouldBe(1307243550);
            result.Industry.ShouldBe("dccb9ef5cf6b4c978dd53d3be1353be31eb43390e98b4e349a0f8f5a89e4db6302a0");
            result.Location.ShouldBe("8734010164634");
            result.RegisteredAddress.ShouldBe("17d78a1cf304463ab42fd466cbb9");
            result.BusinessAddress.ShouldBe("aa85a0e3f79e49");
            result.BusinessScope.ShouldBe("09b53ff1e2164328b66b592b5bb8da5aece8eaa28886415782ea45");
            result.Description.ShouldBe("6673072b801f401382af9e8945d42efb60d02aa9");
            result.CompleteTxId.ShouldBe("dfd452e5293f435386f9242577");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseDetailUpdateDto()
            {
                EnterpriseName = "21bd39245667410b96",
                TotalAssets = "b5af37126f8a405cb7c8b56de724dbe61c18ffc0fa4a4412977046d9e76",
                Income = "43dbcca5efb046c8a9421344a1ab457ef85c55ca7ad1491d940372ec0c65472bdb7",
                EnterpriseType = "1741190684b348c4b53d41938e3bd8be5f3bb59f00a14668ba0d554edc73869b3bf7c3fa5e23407897b424cc2f239a88",
                StaffNumber = 226818290,
                Industry = "77af099439024645bb45b01b832f",
                Location = "3bd93e28038445e197",
                RegisteredAddress = "cb08ee00dbd349e4923a4d10794b09573b667a00f55d4fa4b4",
                BusinessAddress = "e33424c5c0624c08809543",
                BusinessScope = "8a401f08187c44718d38179719107179f86a5cb1922a4531b6fddd98524ac",
                Description = "9413abdc25",
                CompleteTxId = "36df0a97473c48428247d63"
            };

            // Act
            var serviceResult = await _enterpriseDetailsAppService.UpdateAsync(Guid.Parse("8de2be16-5016-449b-bab0-34d82b0ad7d5"), input);

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("21bd39245667410b96");
            result.TotalAssets.ShouldBe("b5af37126f8a405cb7c8b56de724dbe61c18ffc0fa4a4412977046d9e76");
            result.Income.ShouldBe("43dbcca5efb046c8a9421344a1ab457ef85c55ca7ad1491d940372ec0c65472bdb7");
            result.EnterpriseType.ShouldBe("1741190684b348c4b53d41938e3bd8be5f3bb59f00a14668ba0d554edc73869b3bf7c3fa5e23407897b424cc2f239a88");
            result.StaffNumber.ShouldBe(226818290);
            result.Industry.ShouldBe("77af099439024645bb45b01b832f");
            result.Location.ShouldBe("3bd93e28038445e197");
            result.RegisteredAddress.ShouldBe("cb08ee00dbd349e4923a4d10794b09573b667a00f55d4fa4b4");
            result.BusinessAddress.ShouldBe("e33424c5c0624c08809543");
            result.BusinessScope.ShouldBe("8a401f08187c44718d38179719107179f86a5cb1922a4531b6fddd98524ac");
            result.Description.ShouldBe("9413abdc25");
            result.CompleteTxId.ShouldBe("36df0a97473c48428247d63");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterpriseDetailsAppService.DeleteAsync(Guid.Parse("8de2be16-5016-449b-bab0-34d82b0ad7d5"));

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == Guid.Parse("8de2be16-5016-449b-bab0-34d82b0ad7d5"));

            result.ShouldBeNull();
        }
    }
}