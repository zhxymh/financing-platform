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
            result.Items.Any(x => x.Id == Guid.Parse("1106e566-6c97-4b7f-b1e9-f3dd93a1f8c6")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("2e13c321-c18b-4a3a-8908-f4509be6d26d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _appliesAppService.GetAsync(Guid.Parse("1106e566-6c97-4b7f-b1e9-f3dd93a1f8c6"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1106e566-6c97-4b7f-b1e9-f3dd93a1f8c6"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ApplyCreateDto
            {
                EnterpriseName = "bfbf33cbb49b45c2a8d8aa00be24b2071e2ec1561331483cb9f95b66",
                Organization = "de56fd078cd946a5ab9a0803747fd88b19fb2640d45f4d3fbcdc4bf069d09ecce2e674502d5c4f73",
                ProductName = "eb69e6c50a774afc92f61b89a7d9564b9377186e8e2141b5b5cb7b6f8b58867",
                Allowance = "fb73cbd08961453783663c6e3ea04e888501b091b020496988c7eac5534b9a5",
                APY = "947e2ca055a84482b7",
                Period = "83a3b5446e8f4db39ed78fa104d0e4f0fef293d3ec564b07bc4aececda71a1685db46f5776a04e12a12038",
                ApplyStatus = "8cef4cb8a58844e596a6d77f8a22b30e14054",
                GuaranteeMethod = "4ce372ddd59d43f0b6cbd594052",
                ApplyTime = "a49d56b058ca44de",
                PassedTime = "f274999248dd4d2c8bf22fd8fbe65c2e9a4f3b4b783447f"
            };

            // Act
            var serviceResult = await _appliesAppService.CreateAsync(input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriceName.ShouldBe("bfbf33cbb49b45c2a8d8aa00be24b2071e2ec1561331483cb9f95b66");
            result.Organization.ShouldBe("de56fd078cd946a5ab9a0803747fd88b19fb2640d45f4d3fbcdc4bf069d09ecce2e674502d5c4f73");
            result.ProductName.ShouldBe("eb69e6c50a774afc92f61b89a7d9564b9377186e8e2141b5b5cb7b6f8b58867");
            result.Allowance.ShouldBe("fb73cbd08961453783663c6e3ea04e888501b091b020496988c7eac5534b9a5");
            result.APY.ShouldBe("947e2ca055a84482b7");
            result.Period.ShouldBe("83a3b5446e8f4db39ed78fa104d0e4f0fef293d3ec564b07bc4aececda71a1685db46f5776a04e12a12038");
            result.ApplyStatus.ShouldBe("8cef4cb8a58844e596a6d77f8a22b30e14054");
            result.GuaranteeMethod.ShouldBe("4ce372ddd59d43f0b6cbd594052");
            result.ApplyTime.ShouldBe("a49d56b058ca44de");
            result.PassedTime.ShouldBe("f274999248dd4d2c8bf22fd8fbe65c2e9a4f3b4b783447f");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ApplyUpdateDto()
            {
                EnterpriseName = "ef6b8008c7bb4b7c87c433d3c079",
                Organization = "4884ae6c110e493c91c9ebe03d27aee372cba8b",
                ProductName = "aa86270fab0b4ecbb6b0cf472c71357c7a374f7465b",
                Allowance = "ab392b01869f4add8b3678f8b6eb6c185022ed038d6f4102b4a496be8b5b99e396d1093da4434e8fbef",
                APY = "4e550eeaf30646a99726bfcde8a817403bab4b3b10a147fa840927a044f1cb7f4eeef8511a",
                Period = "d133ec676d4647b4816b994ffde74de8fa07f9eeeb63475b87265767d9b28fa0cd5beeba3b9546",
                ApplyStatus = "809e05c7bc704648b22a1d8d",
                GuaranteeMethod = "2d64c974121e4f7491fd",
                ApplyTime = "0f7fa7e7dae34d2f917ca75014886a7a3534dacc783f41f69a46ef05227199b9671d8347800b459684b7924aaacd8725ac",
                PassedTime = "6ef8c1f97e0b43a7bb021287d8c2af87f5154f3e6ccd4fa7bb4e6e65e7b9899271f4bb7175a04097b8be84506"
            };

            // Act
            var serviceResult = await _appliesAppService.UpdateAsync(Guid.Parse("1106e566-6c97-4b7f-b1e9-f3dd93a1f8c6"), input);

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriceName.ShouldBe("ef6b8008c7bb4b7c87c433d3c079");
            result.Organization.ShouldBe("4884ae6c110e493c91c9ebe03d27aee372cba8b");
            result.ProductName.ShouldBe("aa86270fab0b4ecbb6b0cf472c71357c7a374f7465b");
            result.Allowance.ShouldBe("ab392b01869f4add8b3678f8b6eb6c185022ed038d6f4102b4a496be8b5b99e396d1093da4434e8fbef");
            result.APY.ShouldBe("4e550eeaf30646a99726bfcde8a817403bab4b3b10a147fa840927a044f1cb7f4eeef8511a");
            result.Period.ShouldBe("d133ec676d4647b4816b994ffde74de8fa07f9eeeb63475b87265767d9b28fa0cd5beeba3b9546");
            result.ApplyStatus.ShouldBe("809e05c7bc704648b22a1d8d");
            result.GuaranteeMethod.ShouldBe("2d64c974121e4f7491fd");
            result.ApplyTime.ShouldBe("0f7fa7e7dae34d2f917ca75014886a7a3534dacc783f41f69a46ef05227199b9671d8347800b459684b7924aaacd8725ac");
            result.PassedTime.ShouldBe("6ef8c1f97e0b43a7bb021287d8c2af87f5154f3e6ccd4fa7bb4e6e65e7b9899271f4bb7175a04097b8be84506");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _appliesAppService.DeleteAsync(Guid.Parse("1106e566-6c97-4b7f-b1e9-f3dd93a1f8c6"));

            // Assert
            var result = await _applyRepository.FindAsync(c => c.Id == Guid.Parse("1106e566-6c97-4b7f-b1e9-f3dd93a1f8c6"));

            result.ShouldBeNull();
        }
    }
}