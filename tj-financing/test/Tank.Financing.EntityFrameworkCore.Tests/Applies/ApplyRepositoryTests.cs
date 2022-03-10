using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tank.Financing.Applies;
using Tank.Financing.EntityFrameworkCore;
using Xunit;

namespace Tank.Financing.Applies
{
    public class ApplyRepositoryTests : FinancingEntityFrameworkCoreTestBase
    {
        private readonly IApplyRepository _applyRepository;

        public ApplyRepositoryTests()
        {
            _applyRepository = GetRequiredService<IApplyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _applyRepository.GetListAsync(
                    enterpriseName: "1a89570afd6544f299004a800144d3ccc0726ae3ca0b4f48b6bac045faee8c6f956c8dffb118457",
                    organization: "5137fddf7e8444959594f1f22706a5d1e98583d4ed63",
                    productName: "2bdf1fef7355476a875deec6b8a0af013222c1aa8a2c46279cb25e45b6f4975f64736de6f",
                    allowance: "c6a9150f5b8e46e59ce4e61027c90efb622714af2b5345df9b898cec70e980acb115e69106b34873ba96e9088990987",
                    aPR: "c5e1399bcdaf4d05832339663a14ccf6d075d20ec",
                    period: "267d5df84b414844b17d954c25b47f4257e342ff084640589552c",
                    applyStatus: default,
                    guaranteeMethod: default,
                    applyTxId: "56ca21c6f7724eb093aec4322772c2ccf72e2",
                    onlineApproveTxId: "59999c5788224d9da81212a0a0ab9a700da1dd42174543e788de",
                    offlineApproveTxId: "12c1ba94286e47cda22308c048bfda3245188b6d000140d7ba68",
                    approveAllowanceTxId: "f81561fdf68b461d97bb4f4a",
                    setAllowanceTxId: "45143133bfbe4d41b938778b9d7703a727e7d261682445bd92dc84"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("770ffbd6-cbb0-4d88-9286-b4a0f1bd7e93"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _applyRepository.GetCountAsync(
                    enterpriseName: "5aeb55c5ac55487181a4f57f13a3830df1d707f15f5140b2ac90c74e",
                    organization: "47dd883b92c446",
                    productName: "222f1f01ea044b83a639bc29496e591",
                    allowance: "87e1659d56a247ca85a47d4faa9bc1cd89e58231fceb467aa67c28ed",
                    aPR: "b1400983a7d34277a0a9ec9984e23d0432a8732a734f4b4ab899e6ec4c34a",
                    period: "fa9f81eed8d8408cb66c2fc7fb7832400d0e14ae2c1b45f2aeb8e3dbbb938807248f76901f514c5fb44624",
                    applyStatus: default,
                    guaranteeMethod: default,
                    applyTxId: "9f86f08d003a4913aaf2698da77c1a41ce21b85d88cd4b318484f238f99ee",
                    onlineApproveTxId: "a1fdf51fe8a9467c93038",
                    offlineApproveTxId: "97deecc7dfce4c8f997adf0e16f5c931b89eb89385324dfb89022e7489a69541d01a696aa07f4e968ca490b2e1227338",
                    approveAllowanceTxId: "f09f352c5d654bf6afefa42a012bf66ec00845633a50477d8e058d1f1ee6cfabf0e7decb0",
                    setAllowanceTxId: "12db6163f8384f7cbfc22ab368d"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}