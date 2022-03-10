using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tank.Financing.Applies
{
    public class AppliesAppServiceTests : FinancingApplicationTestBase
    {
        private readonly IAppliesAppService _appliesAppService;
        private readonly IRepository<Apply, Guid> _applyRepository;

        public AppliesAppServiceTests()
        {
            _appliesAppService = GetRequiredService<IAppliesAppService>();
            _applyRepository = GetRequiredService<IRepository<Apply, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _appliesAppService.GetListAsync(new GetAppliesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("770ffbd6-cbb0-4d88-9286-b4a0f1bd7e93")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("f068c4a6-ab5c-46fa-872b-ea8eb52cae0f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _appliesAppService.GetAsync(Guid.Parse("770ffbd6-cbb0-4d88-9286-b4a0f1bd7e93"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("770ffbd6-cbb0-4d88-9286-b4a0f1bd7e93"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ApplyCreateDto
            {
                EnterpriseName = "c2c247e3cc294736a9faadcc2245e2f9ebc426ff282044fbb380421a4914e5697de55f4fe59a4e7bbb7735ac28be45a",
                Organization = "d7ca4227b5234f58ba989e67277996ce7de2dfaf2d524433804e992b8",
                ProductName = "a4e9f08f52fe4602b58a6e412bbb1610a94625b9400",
                Allowance = "f36cf328bd6642eb80a61",
                APR = "f2ca601abcd0445d88f02170a132d6ff1d717f9d120649beaa185ecc6f126a97e6c883943f634c60b0e48be8b1eedfaba7",
                Period = "3cdcbb46a58f4cf593d865ab04c83f326",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 1857755467,
                PassedTime = 829309113,
                ApplyTxId = "2b936b23117d41a3a4302b0ca581",
                OnlineApproveTxId = "04bbd48060e24c87952b0",
                OfflineApproveTxId = "d1a3c0d485784355916f61ecf9d9f056b5d6f565e53d4078b1dbde98eeb5bccc018d59d215cb467ba497d25e675",
                ApproveAllowanceTxId = "38845820c2484d3",
                SetAllowanceTxId = "9fefcddf243b40f79dfbdb510da4b898dcc4b6a1c0f24d10826"
            };

            // Act
            var serviceResult = await _appliesAppService.CreateAsync(input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("c2c247e3cc294736a9faadcc2245e2f9ebc426ff282044fbb380421a4914e5697de55f4fe59a4e7bbb7735ac28be45a");
            result.Organization.ShouldBe("d7ca4227b5234f58ba989e67277996ce7de2dfaf2d524433804e992b8");
            result.ProductName.ShouldBe("a4e9f08f52fe4602b58a6e412bbb1610a94625b9400");
            result.Allowance.ShouldBe("f36cf328bd6642eb80a61");
            result.APR.ShouldBe("f2ca601abcd0445d88f02170a132d6ff1d717f9d120649beaa185ecc6f126a97e6c883943f634c60b0e48be8b1eedfaba7");
            result.Period.ShouldBe("3cdcbb46a58f4cf593d865ab04c83f326");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(1857755467);
            result.PassedTime.ShouldBe(829309113);
            result.ApplyTxId.ShouldBe("2b936b23117d41a3a4302b0ca581");
            result.OnlineApproveTxId.ShouldBe("04bbd48060e24c87952b0");
            result.OfflineApproveTxId.ShouldBe("d1a3c0d485784355916f61ecf9d9f056b5d6f565e53d4078b1dbde98eeb5bccc018d59d215cb467ba497d25e675");
            result.ApproveAllowanceTxId.ShouldBe("38845820c2484d3");
            result.SetAllowanceTxId.ShouldBe("9fefcddf243b40f79dfbdb510da4b898dcc4b6a1c0f24d10826");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ApplyUpdateDto()
            {
                EnterpriseName = "e739568eae604b8a8387d54506c2432fc6b9680604cf478a846135cebc5dbbc6af068ad5319e4a9cbef7",
                Organization = "4883aac5a4df4e8c9adce18dec493a2f428827e0dc5e47b4a5bd3c459f0c0c57880834",
                ProductName = "8de364",
                Allowance = "28f8aceffd4c47c",
                APR = "1501a03c6d5e49698d20de1a3ab75eddcf56f6b4d7084a53818981318a8f00d9c5860c8120f545dd8cdc0fb7",
                Period = "14e9a0ab13de4486b1cfdc1c9f36e91038d6599fdd5e4ef6bd736e333c00b48b11619315",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 1128877262,
                PassedTime = 1260712492,
                ApplyTxId = "3b89b8f8fd9e447d90ef80",
                OnlineApproveTxId = "92a2a35da68f40c0bbab4f84a2b7a47e8c49a5c84e7446cf93f6a94dd6a7f77c48ae83efde944b0db",
                OfflineApproveTxId = "e5d1cc91db994c678c09b61ddcfa8ade6bec5b5ad8354d08abe742c4ef6b7b1427905956304d4cb3a94dc4139f",
                ApproveAllowanceTxId = "856d1b646043410f865e6bb94e2e65bf50c15f66c0de455ea2947863b",
                SetAllowanceTxId = "6e9739ccbd474e4"
            };

            // Act
            var serviceResult = await _appliesAppService.UpdateAsync(Guid.Parse("770ffbd6-cbb0-4d88-9286-b4a0f1bd7e93"), input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("e739568eae604b8a8387d54506c2432fc6b9680604cf478a846135cebc5dbbc6af068ad5319e4a9cbef7");
            result.Organization.ShouldBe("4883aac5a4df4e8c9adce18dec493a2f428827e0dc5e47b4a5bd3c459f0c0c57880834");
            result.ProductName.ShouldBe("8de364");
            result.Allowance.ShouldBe("28f8aceffd4c47c");
            result.APR.ShouldBe("1501a03c6d5e49698d20de1a3ab75eddcf56f6b4d7084a53818981318a8f00d9c5860c8120f545dd8cdc0fb7");
            result.Period.ShouldBe("14e9a0ab13de4486b1cfdc1c9f36e91038d6599fdd5e4ef6bd736e333c00b48b11619315");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(1128877262);
            result.PassedTime.ShouldBe(1260712492);
            result.ApplyTxId.ShouldBe("3b89b8f8fd9e447d90ef80");
            result.OnlineApproveTxId.ShouldBe("92a2a35da68f40c0bbab4f84a2b7a47e8c49a5c84e7446cf93f6a94dd6a7f77c48ae83efde944b0db");
            result.OfflineApproveTxId.ShouldBe("e5d1cc91db994c678c09b61ddcfa8ade6bec5b5ad8354d08abe742c4ef6b7b1427905956304d4cb3a94dc4139f");
            result.ApproveAllowanceTxId.ShouldBe("856d1b646043410f865e6bb94e2e65bf50c15f66c0de455ea2947863b");
            result.SetAllowanceTxId.ShouldBe("6e9739ccbd474e4");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _appliesAppService.DeleteAsync(Guid.Parse("770ffbd6-cbb0-4d88-9286-b4a0f1bd7e93"));

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == Guid.Parse("770ffbd6-cbb0-4d88-9286-b4a0f1bd7e93"));

            result.ShouldBeNull();
        }
    }
}