using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Tank.Financing.Applies;

namespace Tank.Financing.Applies
{
    public class AppliesDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IApplyRepository _applyRepository;

        public AppliesDataSeedContributor(IApplyRepository applyRepository)
        {
            _applyRepository = applyRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _applyRepository.InsertAsync(new Apply
            (
                id: Guid.Parse("ebb7b728-053d-49d6-bb38-2c6404b0da19"),
                enterpriseName: "e08f0ec4691c4c69b235fbdd24624179827a2db033dc4fb19a6a54d7f9af08269",
                organization: "ef541897bf814e76aea8b0f8afba33468b58409",
                productName: "7a9e07",
                allowance: "3c2e7679d00e4cd08ddf22542ccab33610b4d16fabc94fde9875de47717e18d5e3e3bccc3cff4a4f8dc0",
                aPR: "f85262ae420f4384990ac9385c",
                period: "697b1e57ef5346f9a9f75",
                applyStatus: default,
                guaranteeMethod: default,
                applyTime: 1949348462,
                passedTime: 1333991544
            ));

            await _applyRepository.InsertAsync(new Apply
            (
                id: Guid.Parse("75e56cf5-a6ec-4e18-b777-2a87b9d9ea12"),
                enterpriseName: "589610b48bd94189925",
                organization: "c2154e8344f94fcca2411a199b17c97028a3b7cb0ef446a7",
                productName: "4511f2a71922497d841912",
                allowance: "3ac2c1aac5a34e3c8b634879f5b060f7a66",
                aPR: "18d8801afe614920972fe9ea6",
                period: "f662080de29047fa82d2215138ef751ed",
                applyStatus: default,
                guaranteeMethod: default,
                applyTime: 1184435493,
                passedTime: 455110207
            ));
        }
    }
}