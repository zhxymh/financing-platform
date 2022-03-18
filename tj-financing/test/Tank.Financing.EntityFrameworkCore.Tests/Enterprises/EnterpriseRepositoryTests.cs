using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tank.Financing.Enterprises;
using Tank.Financing.EntityFrameworkCore;
using Xunit;

namespace Tank.Financing.Enterprises
{
    public class EnterpriseRepositoryTests : FinancingEntityFrameworkCoreTestBase
    {
        private readonly IEnterpriseRepository _enterpriseRepository;

        public EnterpriseRepositoryTests()
        {
            _enterpriseRepository = GetRequiredService<IEnterpriseRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _enterpriseRepository.GetListAsync(
                    enterpriseName: "33ad5045d7b44d3aade0750773fc867e61deb95e90b94127b9abf57d5872ab9b6f794c855565",
                    artificialPerson: "6ed6a145340943d1becdd542470d472fe68959581ee441f5823f0df4e8cea4296b5de8df1ce94ad59f6c50ad9b4f5c",
                    creditCode: "48fe29cbe2b0452a84f32d835beaad6f8438593",
                    artificialPersonId: "43b1a8b3e7884e2887e0c293372a892c3b6a4f54074e420b97d5a4b341dab16120",
                    registeredCapital: "6439426220614531bc78bb1fb474b757e99e40cc3b2a489898af15e9a3b7508ca30c0aa27c8e4dce879f5fe2a881962b9",
                    phoneNumber: "eca80a32313d445fb49c802271213a5e61c329fc05be427d8c87ac269f16278e3004a447ad424a69b14f",
                    certPhotoPath: "667c191476c44e0b91e8ceba44b4a8167c3d",
                    idPhotoPath1: "3e4f9d389dc041379b0f276032fa9f0dda30046059174e2c8b85f33d191bc0332d9435aabd3346b3b40c8da90be7",
                    idPhotoPath2: "787d5b1",
                    certificateStatus: default,
                    certificateTxId: "65cab9b278b141c0b7f909725cc44dbab3133a51e3114",
                    confirmCertificateTxId: "78f4a6fd3b5d4af98c7fde92c6fa616e7307be9f35bd4a",
                    commitUserName: "cdfd41b7f8db4638ab41c994db367d32d531f1cac0e848a9aba5c2a95f40c75da48876ef79fa4830b5a0099c2a9645aea28"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("de3a0132-54e0-422e-9bab-d7baf1e06286"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _enterpriseRepository.GetCountAsync(
                    enterpriseName: "5cd8bcdd7fb742b1aff7919c2bcf3c72485917a132fa45c8bb7fb3948f8893",
                    artificialPerson: "d44dd7c5c7874206a4ff343a8354ca396b0d4f584b4f4b7cafd6501182ffbf0e227672238fda4dc1b598157",
                    creditCode: "8dbaa03fd6e942b7ad1a9d1",
                    artificialPersonId: "b0c17863ad1444f6b1d306c2752d19d1bcb87a51862842baa7f10e275807d4b4eee34d71b77c4515bd04a28b050eace31",
                    registeredCapital: "eb9d74cdd3e94447a461658877349fa962bd5f7655574472b6e175123ec72a9b9e059f99e6d849d29b67ac7",
                    phoneNumber: "a904fb280d9b44f5af4920251941f1c423277e8442584270ae93b070a189986a791fb57",
                    certPhotoPath: "30a915be86984281814a5e2fbe96100",
                    idPhotoPath1: "885b5558deb8469b88a7295eb0ce45498e414f8d9bc74e0c86287fa5ae1e8f4cb3",
                    idPhotoPath2: "d6aebfdba55c45a3aadb6772d6c6d2a79257284df6ee4acdb89640af87f06653b025ec25bab84",
                    certificateStatus: default,
                    certificateTxId: "b0fae6fa1efb4c12a49de5667073136149396fdc51fa43949ee75b99f58aad8707dd4871b502424eb480",
                    confirmCertificateTxId: "bfdc4234c99c4c08ab372f79a76439c1a290c80f8f214488b63d55aa6b4ffbbd3fc6bf5cb76144cb98299",
                    commitUserName: "0c8b88d051ec4cf8b23ae76a28bb605c5a0c6f52c74a4e5eb25ba1be05a69fe9b9b5e4"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}