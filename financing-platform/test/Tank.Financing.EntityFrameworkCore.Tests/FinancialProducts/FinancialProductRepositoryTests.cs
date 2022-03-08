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
                    guaranteeMethod: default,
                    creditCeiling: "509bab9ae30343959c95436d48cf16f5d1d",
                    organization: "1c82860d7a564998b0414a381c352208a3a0c6a0bfca46e8a65e29699f551c3281f5ed5044d94d788665c475b34b69300e21",
                    aPR: "3e3e8a80badc45c4bd2a3b2ec9cd9ff29c2d3feffe6248a8bd0a468e553044a60412952a5f574992b50e762fb595",
                    rating: "de22345c858c4e2393fa7af009b5a52d037d7f33d46c41f88bd2e925ad2935c549299dff",
                    name: "3a04986c531b418997a12c892eb"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d9266c7b-4663-4c1a-bbc4-f30fb15bc60a"));
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
                    guaranteeMethod: default,
                    creditCeiling: "b7a6784734ab456bbfadd7a905c51fb709d4a8418a0d4778af66453cc2b23b",
                    organization: "5ec71e97db1b4f7b8220888949d008567b2bf19f71b04f218dbc299c7fdf8db3442b6c6972144975a12ed51459d2231e8e47",
                    aPR: "c8134975ec1f4298bf144f01506745b8ae9159ca874443379567b878e80a08eae5b5c4082fbe456bb1366520f676064",
                    rating: "cbed5f3f4fe6",
                    name: "8102b014f7844841b9e83dcdf6996554fc0a43ae"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}