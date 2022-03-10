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
            result.Items.Any(x => x.Id == Guid.Parse("b35c4b44-f6c4-46f1-aed1-9293284ef995")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("f7906e73-3331-483d-a591-920f0b14cfc5")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _appliesAppService.GetAsync(Guid.Parse("b35c4b44-f6c4-46f1-aed1-9293284ef995"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b35c4b44-f6c4-46f1-aed1-9293284ef995"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ApplyCreateDto
            {
                EnterpriseName = "990bc7b6b0a4456",
                Organization = "21ba734b3f80473cbd428aaacb6e0ac895b8d17a054e45e090059acfe5dbe739cdae0e2746",
                ProductName = "cc7efc7c72a94267acbb2e070728dcbf3264a6a1291a447e8805e7e81b72c2d88f112636c96a4648a4acf40bf8002c21",
                Allowance = "b71292becd944c38a94fbc9536f2ad4b08675da91d9247cd8e7bb340a",
                APR = "21fc30a5b6bc417386c32c67bef782faff626c19867e4a00b72da282537935a9fc3eb200e86c4797a9703490ed49cd",
                Period = "9823c03bf3244be6b60de1745f3d8b14d3ac6156c04b44948bb6c03474e8d9549851d2067c0f4114baca604",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 1062869369,
                PassedTime = 775495076
            };

            // Act
            var serviceResult = await _appliesAppService.CreateAsync(input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("990bc7b6b0a4456");
            result.Organization.ShouldBe("21ba734b3f80473cbd428aaacb6e0ac895b8d17a054e45e090059acfe5dbe739cdae0e2746");
            result.ProductName.ShouldBe("cc7efc7c72a94267acbb2e070728dcbf3264a6a1291a447e8805e7e81b72c2d88f112636c96a4648a4acf40bf8002c21");
            result.Allowance.ShouldBe("b71292becd944c38a94fbc9536f2ad4b08675da91d9247cd8e7bb340a");
            result.APR.ShouldBe("21fc30a5b6bc417386c32c67bef782faff626c19867e4a00b72da282537935a9fc3eb200e86c4797a9703490ed49cd");
            result.Period.ShouldBe("9823c03bf3244be6b60de1745f3d8b14d3ac6156c04b44948bb6c03474e8d9549851d2067c0f4114baca604");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(1062869369);
            result.PassedTime.ShouldBe(775495076);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ApplyUpdateDto()
            {
                EnterpriseName = "e40210a1d9f04b358ae8",
                Organization = "e7dced59c0b54d50a2a883c0e890fca0bfa8527fd3a645a98c4e5a4c5c63",
                ProductName = "6c82ba57de1f46a792557dcaaef2178b1fca639ac5974778b1484e456410814079dd94ecbb63412",
                Allowance = "bbf238cec5c143d2825b538ee3da18c403c780a8a105459b9940af57f",
                APR = "8dba76b4c24147a1984a16b6614cedd18af",
                Period = "963a61ecfe5a45c5bcdf1056d1d",
                ApplyStatus = default,
                GuaranteeMethod = default,
                ApplyTime = 316527520,
                PassedTime = 903650910
            };

            // Act
            var serviceResult = await _appliesAppService.UpdateAsync(Guid.Parse("b35c4b44-f6c4-46f1-aed1-9293284ef995"), input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("e40210a1d9f04b358ae8");
            result.Organization.ShouldBe("e7dced59c0b54d50a2a883c0e890fca0bfa8527fd3a645a98c4e5a4c5c63");
            result.ProductName.ShouldBe("6c82ba57de1f46a792557dcaaef2178b1fca639ac5974778b1484e456410814079dd94ecbb63412");
            result.Allowance.ShouldBe("bbf238cec5c143d2825b538ee3da18c403c780a8a105459b9940af57f");
            result.APR.ShouldBe("8dba76b4c24147a1984a16b6614cedd18af");
            result.Period.ShouldBe("963a61ecfe5a45c5bcdf1056d1d");
            result.ApplyStatus.ShouldBe(default);
            result.GuaranteeMethod.ShouldBe(default);
            result.ApplyTime.ShouldBe(316527520);
            result.PassedTime.ShouldBe(903650910);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _appliesAppService.DeleteAsync(Guid.Parse("b35c4b44-f6c4-46f1-aed1-9293284ef995"));

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == Guid.Parse("b35c4b44-f6c4-46f1-aed1-9293284ef995"));

            result.ShouldBeNull();
        }
    }
}