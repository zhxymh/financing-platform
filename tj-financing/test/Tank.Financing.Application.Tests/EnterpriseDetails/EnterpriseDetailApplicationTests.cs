using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetailsAppServiceTests : FinancingApplicationTestBase
    {
        private readonly IEnterpriseDetailsAppService _enterpriseDetailsAppService;
        private readonly IRepository<EnterpriseDetail, Guid> _enterpriseDetailRepository;

        public EnterpriseDetailsAppServiceTests()
        {
            _enterpriseDetailsAppService = GetRequiredService<IEnterpriseDetailsAppService>();
            _enterpriseDetailRepository = GetRequiredService<IRepository<EnterpriseDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _enterpriseDetailsAppService.GetListAsync(new GetEnterpriseDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("089227f9-ded5-4ee3-8baf-f4973e762b10")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("668c6902-6dc3-4ce1-afeb-654e5608f972")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enterpriseDetailsAppService.GetAsync(Guid.Parse("089227f9-ded5-4ee3-8baf-f4973e762b10"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("089227f9-ded5-4ee3-8baf-f4973e762b10"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnterpriseDetailCreateDto
            {
                EnterpriseName = "838f8398db6e485e98f77445137e28cb1c55ed04c3e042d9bde412736c9a3660",
                TotalAssets = "c05a6ae454044661b461ec1669de93cd7754f1c",
                Income = "cd1dcfe0de1b4364b25bdb362f90673cde99884cb3544370bf36806636d72e83bb6f6f5a35e44d92995e4d2",
                EnterpriseType = "3b1ea0b584d14",
                StaffNumber = 287093359,
                Industry = "18849b81f44b4acb9805ed76700799f6452a9cf5c77d4bbabfa86391c9d340fd45875912b29045ad9da4c",
                Location = "b22143fe562c",
                RegisteredAddress = "807e043194124d788795b81b9240b0a5a8cf02ee385445ca813ff3f6eeae4990031be8c5ae424",
                BusinessAddress = "b267c530bee04550b71b12ddb67431f81f36273b",
                BusinessScope = "bdf2341a4cb44c",
                Description = "7dccf94c9f024e14aff658ab4cc174b525417a25e9f34ffeb4c0106afd1a1b4f2d97d7cce",
                CompleteTxId = "30994ffc397544e39caf35c8c5cb12914af4c7",
                CommitUserName = "ed43ae271dd34ec7828afa0740a75f1c5f8e578dea8944cb955519f955f76f63dc8019c1f735406aa0a3d145"
            };

            // Act
            var serviceResult = await _enterpriseDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("838f8398db6e485e98f77445137e28cb1c55ed04c3e042d9bde412736c9a3660");
            result.TotalAssets.ShouldBe("c05a6ae454044661b461ec1669de93cd7754f1c");
            result.Income.ShouldBe("cd1dcfe0de1b4364b25bdb362f90673cde99884cb3544370bf36806636d72e83bb6f6f5a35e44d92995e4d2");
            result.EnterpriseType.ShouldBe("3b1ea0b584d14");
            result.StaffNumber.ShouldBe(287093359);
            result.Industry.ShouldBe("18849b81f44b4acb9805ed76700799f6452a9cf5c77d4bbabfa86391c9d340fd45875912b29045ad9da4c");
            result.Location.ShouldBe("b22143fe562c");
            result.RegisteredAddress.ShouldBe("807e043194124d788795b81b9240b0a5a8cf02ee385445ca813ff3f6eeae4990031be8c5ae424");
            result.BusinessAddress.ShouldBe("b267c530bee04550b71b12ddb67431f81f36273b");
            result.BusinessScope.ShouldBe("bdf2341a4cb44c");
            result.Description.ShouldBe("7dccf94c9f024e14aff658ab4cc174b525417a25e9f34ffeb4c0106afd1a1b4f2d97d7cce");
            result.CompleteTxId.ShouldBe("30994ffc397544e39caf35c8c5cb12914af4c7");
            result.CommitUserName.ShouldBe("ed43ae271dd34ec7828afa0740a75f1c5f8e578dea8944cb955519f955f76f63dc8019c1f735406aa0a3d145");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnterpriseDetailUpdateDto()
            {
                EnterpriseName = "e54627c919cf41b983e29f2fcd4ba5884919fa0070564c2994c354c02c2df131f",
                TotalAssets = "9510eb72e28141fc8b9408c4affc4b82fd841b39aad641599db56b547",
                Income = "337d832db53e4a28b64de81ab503e5637f811966dec24ded9fd493e615d639f4692a50b61af0483f950c",
                EnterpriseType = "3e10d1f1c9ba4a7094b0b932364e081e850c6d34833c4dd594ba7bbf47af645e4ef7",
                StaffNumber = 1610138079,
                Industry = "69afa57d422f4bd9b803d4",
                Location = "03439b70365e44328651f3e45cfe8defed6e192a8d124180b49bd8",
                RegisteredAddress = "c42f0f6bd432400687cdde23709e0293424be8d50caa4",
                BusinessAddress = "e27a9b4e02c748098998d1001f7d8d71b1d04963c8ad4ac2a65506ae7e7",
                BusinessScope = "9914c9d879704d3bb33abe4c8a64be19937ae687b4b74daf81b37a5199fbf7014e6795b4ce35488dbb24fcde3947696",
                Description = "b534f837bd6142d58869b0cd44dc63ab584824de9bb648cba2dee4b49c6a30a0f7434aa44a114c48",
                CompleteTxId = "0ded53c284b84a1e8d3fef70ae870e701bb0666d065a43d3862127d551e0b6f3",
                CommitUserName = "376952bf59f84ab5bd2556ba7ad12b758aea34111a2b4182b1e61620001e404515e5a67c"
            };

            // Act
            var serviceResult = await _enterpriseDetailsAppService.UpdateAsync(Guid.Parse("089227f9-ded5-4ee3-8baf-f4973e762b10"), input);

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnterpriseName.ShouldBe("e54627c919cf41b983e29f2fcd4ba5884919fa0070564c2994c354c02c2df131f");
            result.TotalAssets.ShouldBe("9510eb72e28141fc8b9408c4affc4b82fd841b39aad641599db56b547");
            result.Income.ShouldBe("337d832db53e4a28b64de81ab503e5637f811966dec24ded9fd493e615d639f4692a50b61af0483f950c");
            result.EnterpriseType.ShouldBe("3e10d1f1c9ba4a7094b0b932364e081e850c6d34833c4dd594ba7bbf47af645e4ef7");
            result.StaffNumber.ShouldBe(1610138079);
            result.Industry.ShouldBe("69afa57d422f4bd9b803d4");
            result.Location.ShouldBe("03439b70365e44328651f3e45cfe8defed6e192a8d124180b49bd8");
            result.RegisteredAddress.ShouldBe("c42f0f6bd432400687cdde23709e0293424be8d50caa4");
            result.BusinessAddress.ShouldBe("e27a9b4e02c748098998d1001f7d8d71b1d04963c8ad4ac2a65506ae7e7");
            result.BusinessScope.ShouldBe("9914c9d879704d3bb33abe4c8a64be19937ae687b4b74daf81b37a5199fbf7014e6795b4ce35488dbb24fcde3947696");
            result.Description.ShouldBe("b534f837bd6142d58869b0cd44dc63ab584824de9bb648cba2dee4b49c6a30a0f7434aa44a114c48");
            result.CompleteTxId.ShouldBe("0ded53c284b84a1e8d3fef70ae870e701bb0666d065a43d3862127d551e0b6f3");
            result.CommitUserName.ShouldBe("376952bf59f84ab5bd2556ba7ad12b758aea34111a2b4182b1e61620001e404515e5a67c");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enterpriseDetailsAppService.DeleteAsync(Guid.Parse("089227f9-ded5-4ee3-8baf-f4973e762b10"));

            // Assert
            var result = await _enterpriseDetailRepository.FindAsync(c => c.Id == Guid.Parse("089227f9-ded5-4ee3-8baf-f4973e762b10"));

            result.ShouldBeNull();
        }
    }
}