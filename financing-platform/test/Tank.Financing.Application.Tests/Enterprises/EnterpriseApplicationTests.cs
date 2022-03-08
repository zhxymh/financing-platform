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
            result.Items.Any(x => x.Id == Guid.Parse("0705d2a1-c20a-4269-9786-dff792c49d3d")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("03df4b2d-ccd0-40b8-8b5a-6dbb93ba3705")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterprisesAppService.GetAsync(Guid.Parse("0705d2a1-c20a-4269-9786-dff792c49d3d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("0705d2a1-c20a-4269-9786-dff792c49d3d"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseCreateDto
            {
                EnterpriseName = "0880389ee4a9435091",
                ArtificialPerson = "3673aa836f194d27b32",
                EstablishedTime = "f4b960cbdfd444708186",
                DueTime = "061b9a81f27d447899eb9e4f22ad96bd1f2a5fdb98cd4f40851a660cb82d0a94fb7d636676d644359cb527dddeb",
                CreditCode = "a2c4202e9c0f43299f30b3bfd470b03589be95207a75437ab15d4809b789863a4c3cffbc48e647e1b2f6ac",
                ArtificialPersonId = "7e49bef74ff24dfd9772c3",
                RegisteredCapital = "1b8900b897",
                PhoneNumber = "b8de9d61b9f94810af44",
                CertPhotoPath = "1cbbe2f9c73a49bfb435adbd96c791412a0749fdf7f5454491ba9a672b534895020881ec6fbf458cae23ee05e675aceb016",
                IdPhotoPath1 = "e4ef5c6311e840c4a25b3299046dc9a8db6b59b0bb884f5ca9ee233ca2b",
                IdPhotoPath2 = "24f05ded3b4f421aa29f8e710ca55addad7652588d714d29bb48d067ff0d93d0d03bde8aab",
                CertificateStatus = default
            };

            // Act
            var serviceResult = await _enterprisesAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("0880389ee4a9435091");
            result.ArtificialPerson.ShouldBe("3673aa836f194d27b32");
            result.EstablishedTime.ShouldBe("f4b960cbdfd444708186");
            result.DueTime.ShouldBe("061b9a81f27d447899eb9e4f22ad96bd1f2a5fdb98cd4f40851a660cb82d0a94fb7d636676d644359cb527dddeb");
            result.CreditCode.ShouldBe("a2c4202e9c0f43299f30b3bfd470b03589be95207a75437ab15d4809b789863a4c3cffbc48e647e1b2f6ac");
            result.ArtificialPersonId.ShouldBe("7e49bef74ff24dfd9772c3");
            result.RegisteredCapital.ShouldBe("1b8900b897");
            result.PhoneNumber.ShouldBe("b8de9d61b9f94810af44");
            result.CertPhotoPath.ShouldBe("1cbbe2f9c73a49bfb435adbd96c791412a0749fdf7f5454491ba9a672b534895020881ec6fbf458cae23ee05e675aceb016");
            result.IdPhotoPath1.ShouldBe("e4ef5c6311e840c4a25b3299046dc9a8db6b59b0bb884f5ca9ee233ca2b");
            result.IdPhotoPath2.ShouldBe("24f05ded3b4f421aa29f8e710ca55addad7652588d714d29bb48d067ff0d93d0d03bde8aab");
            result.CertificateStatus.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseUpdateDto()
            {
                EnterpriseName = "89fc4e1835e944cf9e18db55930838a5e8fba7f399204432952b647f60",
                ArtificialPerson = "e4ef8b7b3e7742d38587d737ca96c6a534ec7b9e48744",
                EstablishedTime = "b611922329334f21828a26e0",
                DueTime = "abf334d7c",
                CreditCode = "341419c6216a44bda450fd0dd80f6c0e5598fb916f5a4148a85ee31fc6ea7ffab8e9b5ff4",
                ArtificialPersonId = "964195edde1548d4a66a132e20314b12cc47de6bf0cf4f15b4e592ba16a9b50cf02c8ecc5c8b4857b6a9100b05d7e0",
                RegisteredCapital = "8fbcd55a7fe14ab1b4da37126268cb554998a09815",
                PhoneNumber = "f5a81cbc170a418fb19853952b998b84a655473b2d074c459741f69b890a057edb12ffd4d25e427",
                CertPhotoPath = "1e072bfcb73e43ebad9fa9133",
                IdPhotoPath1 = "4c894f23601f46d4894de913e164fac1196bc326ae864a04afa0475fc664cc5728",
                IdPhotoPath2 = "5591227422514cab80e8fc8eabacc34b05ee795ca0f346ccb98937ac",
                CertificateStatus = default
            };

            // Act
            var serviceResult = await _enterprisesAppService.UpdateAsync(Guid.Parse("0705d2a1-c20a-4269-9786-dff792c49d3d"), input);

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("89fc4e1835e944cf9e18db55930838a5e8fba7f399204432952b647f60");
            result.ArtificialPerson.ShouldBe("e4ef8b7b3e7742d38587d737ca96c6a534ec7b9e48744");
            result.EstablishedTime.ShouldBe("b611922329334f21828a26e0");
            result.DueTime.ShouldBe("abf334d7c");
            result.CreditCode.ShouldBe("341419c6216a44bda450fd0dd80f6c0e5598fb916f5a4148a85ee31fc6ea7ffab8e9b5ff4");
            result.ArtificialPersonId.ShouldBe("964195edde1548d4a66a132e20314b12cc47de6bf0cf4f15b4e592ba16a9b50cf02c8ecc5c8b4857b6a9100b05d7e0");
            result.RegisteredCapital.ShouldBe("8fbcd55a7fe14ab1b4da37126268cb554998a09815");
            result.PhoneNumber.ShouldBe("f5a81cbc170a418fb19853952b998b84a655473b2d074c459741f69b890a057edb12ffd4d25e427");
            result.CertPhotoPath.ShouldBe("1e072bfcb73e43ebad9fa9133");
            result.IdPhotoPath1.ShouldBe("4c894f23601f46d4894de913e164fac1196bc326ae864a04afa0475fc664cc5728");
            result.IdPhotoPath2.ShouldBe("5591227422514cab80e8fc8eabacc34b05ee795ca0f346ccb98937ac");
            result.CertificateStatus.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterprisesAppService.DeleteAsync(Guid.Parse("0705d2a1-c20a-4269-9786-dff792c49d3d"));

            // Assert
            var result = await _enterpriseRepository.FindAsync(c => c.Id == Guid.Parse("0705d2a1-c20a-4269-9786-dff792c49d3d"));

            result.ShouldBeNull();
        }
    }
}