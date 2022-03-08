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
                id: Guid.Parse("1106e566-6c97-4b7f-b1e9-f3dd93a1f8c6"),
                enterpriceName: "836d17717e6d425ea2ffc7d1e23b56d31ba1ea2633ad40b48aab88e299155762903a79d6a83e48",
                organization: "047960da6fd544ec805575120a6ff76be8583a39c76148a481df3527775931b159235ee795c",
                productName: "feb03aee0795489bb712b52fd9708d1009c66f647d6b4adc87b5ba375e939565d312853f8a8e431",
                allowance: "e431832aeddc4ebb9238489d",
                aPY: "2832d5d61c3d49aa9c9332dc41341f8ea6fd03185aad4049b2c499c2e85812",
                period: "993c7567539f4161b",
                applyStatus: "5baab723b91d44bfaad9f785adc2f644ad0b366f12c24acdbc4f502b9b4d632ab5022e9d83fa460eb940e",
                guaranteeMethod: "3a575523b9cc413eaf83f267509cb",
                applyTime: "d968eac4b2634692b03e1b577eb0205d4aa0cf66f6fb40fa9384d5238110ae0ba09855896f58442d",
                passedTime: "3b9aa1108062418cba5e3cc137dba36dcc450069e422407699b1ca843d09ffe45f72c06bf678"
            ));

            await _applyRepository.InsertAsync(new Apply
            (
                id: Guid.Parse("2e13c321-c18b-4a3a-8908-f4509be6d26d"),
                enterpriceName: "796bae38f1f344d48c9c00ebcac636d078195614673d4e4d8130a75",
                organization: "4eb90a0df58841d98fe523e7cbb280dc4d1d187412a2443f99f1a6e490bc516653f810fd94224f9597fbf9d01a2d2f78",
                productName: "0e8ab37c0a974e90993fa2700287762e6549f172a2844b4293d58",
                allowance: "ceead413acfc4663a",
                aPY: "7d29951d35b945198f81d9b05ede0eda5c0f9a04f45b403b83032dbbaa7f541a3aa483a45fd649779d235019cea7cf48",
                period: "1ecbb6d3bd5e44ef918d4b2eaf28a660cbe41dfc1c9a4f97856ad0d79d6",
                applyStatus: "ff88948d412c4bd7ac388ca19fb4bafd9b260fb002f04ee5b6939900b5c9d45c96e9de99dfc945bc82324da5ed403d0",
                guaranteeMethod: "e53c48ed3",
                applyTime: "b6192b9325ef4d6e8aedbbb604e93fce91f",
                passedTime: "c645f630745543ba82e274a902ebf5b691436c8206664ba39ed698b7b5b8a581e68c0a9521dd47cd89d10a8"
            ));
        }
    }
}