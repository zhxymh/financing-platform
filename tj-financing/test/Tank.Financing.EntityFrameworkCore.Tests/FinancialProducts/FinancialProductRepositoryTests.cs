using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tank.Financing.FinancialProducts;
using Tank.Financing.EntityFrameworkCore;
using Xunit;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductRepositoryTests : FinancingEntityFrameworkCoreTestBase
    {
        private readonly IFinancialProductRepository _financialProductRepository;

        public FinancialProductRepositoryTests()
        {
            _financialProductRepository = GetRequiredService<IFinancialProductRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _financialProductRepository.GetListAsync(
                    productName: "a286231716154298bbef53d5f85cb693b9877c534a12",
                    organization: "031d68771896484fa2b5a85248abae2da4b96e3de1994e8e",
                    guaranteeMethod: default,
                    aPR: "4cce074f4fce488cb61bb45380d",
                    rating: "138e8449008e49b988741549e64bbfcf409fd15864ec43bd88ced0987e27e6c203b3e6b44a1f45",
                    addFinancingProductTxId: "6cf5fbaac4a740ccb28da87ae7d3256e0074c",
                    url_logo1: "a5d08ed6931a49c492f0a4fb0ebdf1c5b2885e63678740",
                    url_logo2: "3a7166c",
                    url_logo3: "ec19ef8ca3ca40",
                    url_logo4: "7926ab6",
                    url_logo5: "9b4fe15c98f6443b8689759e3d6925254e61b229149",
                    features: "8b31536db05f4a5fa29692c474f4299b6e951b3957934000b1aa4e443350a05b1ceef534f049405887bd7a6610"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("5e2532b0-b7ae-478a-af0b-f0e4386e414f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _financialProductRepository.GetCountAsync(
                    productName: "2951d5f10c4b40e19388551e62e897c6566809e6b9034c82ada2c",
                    organization: "99751f8d439b41c688c8f568f20dcb7ce25645f4ec1641b7a88652fd87124fbbae3f9694ff5442d399c5e3bea",
                    guaranteeMethod: default,
                    aPR: "23e4e40171fa421bbb2adf6543bd60ac83127e77482f4",
                    rating: "f18dbf060865477aafbcc551f",
                    addFinancingProductTxId: "ee44ecb1d03f41d9ae36512bcffb1a069af30fb5b3b943a0b35a6e3b176c582b2ab1eed34b1546d9a",
                    url_logo1: "dab3e98466794484a7e7a93bc78c909667df232f876043359c288",
                    url_logo2: "4a88bd939daa4922",
                    url_logo3: "736b0a55e9c84e4ba4476b0e9505ac772ee6c3897b5b451e9b723c24b685acac9196f6f1eac943248aab41a155d36a57",
                    url_logo4: "8954ccc",
                    url_logo5: "f5c37eec22b64b",
                    features: "0f3446e87082488e9489d8d8a8b80f09c381"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}