using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tank.Financing.EnterpriseDetails;
using Tank.Financing.EntityFrameworkCore;
using Xunit;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetailRepositoryTests : FinancingEntityFrameworkCoreTestBase
    {
        private readonly IEnterpriseDetailRepository _enterpriseDetailRepository;

        public EnterpriseDetailRepositoryTests()
        {
            _enterpriseDetailRepository = GetRequiredService<IEnterpriseDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _enterpriseDetailRepository.GetListAsync(
                    enterpriseName: "a7055909f2a24964b9f11208a8919702b3260e800f24446298e1739",
                    totalAssets: "8b2959f90a174a73b5672e",
                    income: "eeb66bad0b8747f59f034734",
                    enterpriseType: "154f3538bbfd4652b30658a8329f7db74340729f4dab4d1eb39980f5c7b",
                    industry: "48da34ac520844e5b9250677962cb081184da77f12ea42cd8989c4f3f4f46904a77c9de551cd4410912973",
                    location: "a2f74d1ac2db429c8cc309d834",
                    registeredAddress: "98857246e",
                    businessAddress: "dc74f0578afe47df8a6e78a57",
                    businessScope: "ce194e5e89fe423abdd5f7d48b3a74ec497d9014caf84a9e",
                    description: "a9f5f371cfef4d2cb2381dbfacf28ea",
                    completeTxId: "e5fa190f8bb54447af72f2dd4fa1"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("8de2be16-5016-449b-bab0-34d82b0ad7d5"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _enterpriseDetailRepository.GetCountAsync(
                    enterpriseName: "dd77714f6dd345438ed6d7438e959f277f2",
                    totalAssets: "c6562a5100eb44a79b548cd0f11f931ee1de63d9bf3a40e0a718e6711ef2a34b60eb5b7dafee4339baf",
                    income: "484e5aac07a74e9f8416b49818b5fae3b15742869eb345ebaf76824a8673ccd14d9dda78e8524232b6",
                    enterpriseType: "7cf229551",
                    industry: "f3467be0d99b466890c2fd256d98f9f990cf25a9afc04230a22addbf9c1b2b947237e60331004b8d95",
                    location: "19b8fefa594e4150914e18b564d0f9a76ee8bf165e834fd1a129d2733de2a69e0f56f72f8",
                    registeredAddress: "22eb4e1b918649fcbb5e79f250b627165308d944bb2d4967bb1f1ba49d4122a2290e2777694249db9",
                    businessAddress: "e4de7b65513e494dbf16680166948",
                    businessScope: "f9829aca3f9047389ace85fce10c9109ef24058f4a26429ca19b03e97c899eba4f1ec51920e74c1fa7d3670587ef",
                    description: "955b5edf1ddb4f749dd6a78ce65db5721a17e288ba8f49338917f8a3be40f6ac4e2d63837eea497c8dfb73504",
                    completeTxId: "ea5f8c896b0d4973854d58044f100c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}