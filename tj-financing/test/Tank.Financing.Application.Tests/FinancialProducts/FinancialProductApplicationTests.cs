using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductsAppServiceTests : FinancingApplicationTestBase
    {
        private readonly IFinancialProductsAppService _financialProductsAppService;
        private readonly IRepository<FinancialProduct, Guid> _financialProductRepository;

        public FinancialProductsAppServiceTests()
        {
            _financialProductsAppService = GetRequiredService<IFinancialProductsAppService>();
            _financialProductRepository = GetRequiredService<IRepository<FinancialProduct, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _financialProductsAppService.GetListAsync(new GetFinancialProductsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("5e2532b0-b7ae-478a-af0b-f0e4386e414f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("51d741db-b36f-436a-ae73-d245f9ce8ae0")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _financialProductsAppService.GetAsync(Guid.Parse("5e2532b0-b7ae-478a-af0b-f0e4386e414f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5e2532b0-b7ae-478a-af0b-f0e4386e414f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FinancialProductCreateDto
            {
                ProductName = "75aef6c9cea54d4db5ea4d9972",
                Organization = "08476fb7f3604438a81f650098352ba33bd176d803394aa68b61c1db0ec0b33907227ca71",
                Period = 2006147402,
                GuaranteeMethod = default,
                AppliedNumber = 1502550660,
                APR = "28a57624e5ec49e3ac668680b7c923b8408c",
                Rating = "9a5ab87290f145beb6",
                CreditCeiling = 460593136,
                AddFinancingProductTxId = "674a131a8fab4efb90e62d15b6c6a54cf539f7cfb0374cc3bb4432ab7f861296fc5cb",
                url_logo1 = "42599c2aa1e14d0a9768a483691155f16c1565847c794feeabf6c6eaa34cd72312cfe89e09094ea2839667ec062",
                url_logo2 = "c9f1f5ee2c414960b0fd6d1586ec2ffbe1a68fa0bfaa47999594b8d6",
                url_logo3 = "08f6f7c5b854459b90e3b0f6939c0795b0e92bd9553c43a",
                url_logo4 = "07114d1018254425a2f0fbe5c14435c6fa3b039e8426425e",
                url_logo5 = "f79679cfccf14bd09a97552589111cc1f55c33955341497d83a058c4609009c42e2729c",
                features = "6af631023e2"
            };

            // Act
            var serviceResult = await _financialProductsAppService.CreateAsync(input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProductName.ShouldBe("75aef6c9cea54d4db5ea4d9972");
            result.Organization.ShouldBe("08476fb7f3604438a81f650098352ba33bd176d803394aa68b61c1db0ec0b33907227ca71");
            result.Period.ShouldBe(2006147402);
            result.GuaranteeMethod.ShouldBe(default);
            result.AppliedNumber.ShouldBe(1502550660);
            result.APR.ShouldBe("28a57624e5ec49e3ac668680b7c923b8408c");
            result.Rating.ShouldBe("9a5ab87290f145beb6");
            result.CreditCeiling.ShouldBe(460593136);
            result.AddFinancingProductTxId.ShouldBe("674a131a8fab4efb90e62d15b6c6a54cf539f7cfb0374cc3bb4432ab7f861296fc5cb");
            result.url_logo1.ShouldBe("42599c2aa1e14d0a9768a483691155f16c1565847c794feeabf6c6eaa34cd72312cfe89e09094ea2839667ec062");
            result.url_logo2.ShouldBe("c9f1f5ee2c414960b0fd6d1586ec2ffbe1a68fa0bfaa47999594b8d6");
            result.url_logo3.ShouldBe("08f6f7c5b854459b90e3b0f6939c0795b0e92bd9553c43a");
            result.url_logo4.ShouldBe("07114d1018254425a2f0fbe5c14435c6fa3b039e8426425e");
            result.url_logo5.ShouldBe("f79679cfccf14bd09a97552589111cc1f55c33955341497d83a058c4609009c42e2729c");
            result.features.ShouldBe("6af631023e2");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FinancialProductUpdateDto()
            {
                ProductName = "40470de995fc4a4ab5935638fb020c23",
                Organization = "2cf583c035f241f0af410aa81f4f138863754f3398e84ff3a249ec7e890b142a65ff47809b",
                Period = 925048386,
                GuaranteeMethod = default,
                AppliedNumber = 740807450,
                APR = "cf81784448c24271898dfb8ae1fca703c58ef5d9655641a19b0ec92e33c69f9b7077bcbeac6a44adba",
                Rating = "3ba64f1d2e73414ea72",
                CreditCeiling = 1174239333,
                AddFinancingProductTxId = "ba8eaa2b76814ec79245f9a54c63d835c789da5790124838a35def2a40",
                url_logo1 = "4ee5bf08be264b16a4a8d254f9edba99f3c696975cca4de3bdd5e03edb409629b811b",
                url_logo2 = "b8b05310634545d9beb194473de3b3db9527eb845dc14f49b3c5507aeb18ecbf6172d9673e9e45d5809b",
                url_logo3 = "be2988b4447446d6a8d822e4689484f52d983630b1874",
                url_logo4 = "8911e45560b6402e83ae1e179ed46b82ac277d74e55f488286c6f1064d84c2b348f87",
                url_logo5 = "74556abbcc4b40fa9e2dedcda4e15fb819963dc596fc4ceda95f02c423c2781105d97f5b68",
                features = "11e619d647444b289fb3371ff82d2e5cd695afe8ed984b5ea0f81a9b896119f48f52ee1293ff43389c91a728d"
            };

            // Act
            var serviceResult = await _financialProductsAppService.UpdateAsync(Guid.Parse("5e2532b0-b7ae-478a-af0b-f0e4386e414f"), input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProductName.ShouldBe("40470de995fc4a4ab5935638fb020c23");
            result.Organization.ShouldBe("2cf583c035f241f0af410aa81f4f138863754f3398e84ff3a249ec7e890b142a65ff47809b");
            result.Period.ShouldBe(925048386);
            result.GuaranteeMethod.ShouldBe(default);
            result.AppliedNumber.ShouldBe(740807450);
            result.APR.ShouldBe("cf81784448c24271898dfb8ae1fca703c58ef5d9655641a19b0ec92e33c69f9b7077bcbeac6a44adba");
            result.Rating.ShouldBe("3ba64f1d2e73414ea72");
            result.CreditCeiling.ShouldBe(1174239333);
            result.AddFinancingProductTxId.ShouldBe("ba8eaa2b76814ec79245f9a54c63d835c789da5790124838a35def2a40");
            result.url_logo1.ShouldBe("4ee5bf08be264b16a4a8d254f9edba99f3c696975cca4de3bdd5e03edb409629b811b");
            result.url_logo2.ShouldBe("b8b05310634545d9beb194473de3b3db9527eb845dc14f49b3c5507aeb18ecbf6172d9673e9e45d5809b");
            result.url_logo3.ShouldBe("be2988b4447446d6a8d822e4689484f52d983630b1874");
            result.url_logo4.ShouldBe("8911e45560b6402e83ae1e179ed46b82ac277d74e55f488286c6f1064d84c2b348f87");
            result.url_logo5.ShouldBe("74556abbcc4b40fa9e2dedcda4e15fb819963dc596fc4ceda95f02c423c2781105d97f5b68");
            result.features.ShouldBe("11e619d647444b289fb3371ff82d2e5cd695afe8ed984b5ea0f81a9b896119f48f52ee1293ff43389c91a728d");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _financialProductsAppService.DeleteAsync(Guid.Parse("5e2532b0-b7ae-478a-af0b-f0e4386e414f"));

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == Guid.Parse("5e2532b0-b7ae-478a-af0b-f0e4386e414f"));

            result.ShouldBeNull();
        }
    }
}