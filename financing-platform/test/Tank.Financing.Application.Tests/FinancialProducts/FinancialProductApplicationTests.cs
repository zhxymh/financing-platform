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
            result.Items.Any(x => x.Id == Guid.Parse("d9266c7b-4663-4c1a-bbc4-f30fb15bc60a")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("5b5cef93-3657-46b2-8791-fa1502d032c0")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _financialProductsAppService.GetAsync(Guid.Parse("d9266c7b-4663-4c1a-bbc4-f30fb15bc60a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d9266c7b-4663-4c1a-bbc4-f30fb15bc60a"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FinancialProductCreateDto
            {
                TimeLimit = 2757,
                GuaranteeMethod = default,
                CreditCeiling = "5b34ae55f1744969",
                Organization = "5d395583a02e4af089a101493c99b0b1691a27b04cef4e6cbb35881e3efbce5d488abc1e0a0d495a9f7bbb7eb89a89b47ca3",
                AppliedNumber = 451142152,
                APR = "a84e60f2a3e6423da0c7d95c7ceddedc51222f75dfaa413c834c37abf732e5872ba9b905b1154007bf8f0205882",
                Rating = "b41516b8c76849cc83ee70c72c86b7e5",
                Name = "1acc9d15f6ff430a865709013c15d8f7e48117"
            };

            // Act
            var serviceResult = await _financialProductsAppService.CreateAsync(input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.TimeLimit.ShouldBe(2757);
            result.GuaranteeMethod.ShouldBe(default);
            result.CreditCeiling.ShouldBe("5b34ae55f1744969");
            result.Organization.ShouldBe("5d395583a02e4af089a101493c99b0b1691a27b04cef4e6cbb35881e3efbce5d488abc1e0a0d495a9f7bbb7eb89a89b47ca3");
            result.AppliedNumber.ShouldBe(451142152);
            result.APR.ShouldBe("a84e60f2a3e6423da0c7d95c7ceddedc51222f75dfaa413c834c37abf732e5872ba9b905b1154007bf8f0205882");
            result.Rating.ShouldBe("b41516b8c76849cc83ee70c72c86b7e5");
            result.Name.ShouldBe("1acc9d15f6ff430a865709013c15d8f7e48117");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FinancialProductUpdateDto()
            {
                TimeLimit = 1367,
                GuaranteeMethod = default,
                CreditCeiling = "faad403eb91d49aa9b41b7d704166ef8862b7b8a1af54b24a30473f08034e4530fa40a05590345da89f7",
                Organization = "1d309a4501ca4166bd1874256018d9204e4129198ec743f59a03d537c4c2c2ca7d62ee7e92664323bb5478b42e21a54fbe4a",
                AppliedNumber = 538211865,
                APR = "89a96c7a92f641a18d9d1a57b3eb44bac2bcb9a344c7473897c410d78",
                Rating = "f04bfcdaf08c40e3b67a32820c7057c1c24aaf6ced",
                Name = "28f6c85de3834a91be1761893fb9cb00c3c8a5ab360e405eb357a9fca6765753392db3bec32648"
            };

            // Act
            var serviceResult = await _financialProductsAppService.UpdateAsync(Guid.Parse("d9266c7b-4663-4c1a-bbc4-f30fb15bc60a"), input);

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.TimeLimit.ShouldBe(1367);
            result.GuaranteeMethod.ShouldBe(default);
            result.CreditCeiling.ShouldBe("faad403eb91d49aa9b41b7d704166ef8862b7b8a1af54b24a30473f08034e4530fa40a05590345da89f7");
            result.Organization.ShouldBe("1d309a4501ca4166bd1874256018d9204e4129198ec743f59a03d537c4c2c2ca7d62ee7e92664323bb5478b42e21a54fbe4a");
            result.AppliedNumber.ShouldBe(538211865);
            result.APR.ShouldBe("89a96c7a92f641a18d9d1a57b3eb44bac2bcb9a344c7473897c410d78");
            result.Rating.ShouldBe("f04bfcdaf08c40e3b67a32820c7057c1c24aaf6ced");
            result.Name.ShouldBe("28f6c85de3834a91be1761893fb9cb00c3c8a5ab360e405eb357a9fca6765753392db3bec32648");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _financialProductsAppService.DeleteAsync(Guid.Parse("d9266c7b-4663-4c1a-bbc4-f30fb15bc60a"));

            // Assert
            var result = await _financialProductRepository.FindAsync(c => c.Id == Guid.Parse("d9266c7b-4663-4c1a-bbc4-f30fb15bc60a"));

            result.ShouldBeNull();
        }
    }
}