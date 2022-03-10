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
                id: Guid.Parse("b35c4b44-f6c4-46f1-aed1-9293284ef995"),
                enterpriseName: "40858ab8e0064bc19608fff57a5b5fd36e5a348ab7fe483d804cfed5dd32d85b2827995",
                organization: "dbbd95e3232c4dec9670084bfdffd75c",
                productName: "6da64d40cc9b46a9a39c43135bb44a841170924aefd34b4c8",
                allowance: "afc4f39729a649549883da",
                aPR: "534f09fd288e4a55a2d89d285c942934176929e6998349b697130040412d4a9e4c21edc169164d7da6e8ec9",
                period: "0a3bc436e5844b64ba201",
                applyStatus: default,
                guaranteeMethod: default,
                applyTime: 63628792,
                passedTime: 1439945525
            ));

            await _applyRepository.InsertAsync(new Apply
            (
                id: Guid.Parse("f7906e73-3331-483d-a591-920f0b14cfc5"),
                enterpriseName: "6c057b4e55c5",
                organization: "3d1b4cd952674486a58b6ea5f23da311b8e909206c8f432b84a3eb41e146b17698",
                productName: "e1b63a67ed7945538a1758d1d16610600079cf0dee3f4ef48c8b8d63949fcae26168660121e749bf930a5956dcf1169",
                allowance: "1bbef625910e474490e8f5fd677ca58",
                aPR: "c935d097bdfb4b8d96b6d8a8f4a0fda2cf3048bc4f6046d5ad1460bd",
                period: "961003338dcf4c60b7451a10b06c1d718fa4c3ac62934241902b9e0dc5c5a6bb141353573c5f4e07",
                applyStatus: default,
                guaranteeMethod: default,
                applyTime: 1800895825,
                passedTime: 1922070632
            ));
        }
    }
}