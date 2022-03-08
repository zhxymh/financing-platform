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
                id: Guid.Parse("704ce5e8-2fa7-4116-a085-5e067b182bad"),
                enterpriseName: "86d94c9db6274af",
                organization: "6614412fb0c7432b9adb55dbc6b887",
                productName: "30200ee14aa3490d805c1ba4886d680f17f32cae322a4ac",
                allowance: "1a3e746779874fd2ac0eadc",
                aPY: "9ea00ab60c2e4105a335ddd19e9c5093da12a299eeed4b80ba2b82543f9f",
                period: "ca0baee377ec41b684c0ff8e31ddaab164fc9fe724c84a5dbb9af0704605fc52d35edb7bc84446649d",
                applyStatus: default,
                guaranteeMethod: default,
                applyTime: "d23d02e9cfd14ea2937eeb3fff5260ba1e5b25f2",
                passedTime: "2f1bbccea1164467a1f28cab8e171ab4a81d7649b24843a9b3dc8cbb54901927"
            ));

            await _applyRepository.InsertAsync(new Apply
            (
                id: Guid.Parse("51d4c0e3-d5cc-4974-a530-0432d405f5af"),
                enterpriseName: "64234251821f412789a198ceea12b167",
                organization: "9f6380f5a22640b4a991ee3770a7037827b5a506030",
                productName: "f5e0cb6f39a44d5ca7dffea7abff5e2ded28e0e81231",
                allowance: "1804e39b3ea44ec9a4b3cedb676f1b22d2feebdd49494b69b7f5",
                aPY: "7a48a2ec708d4956b202753700e28f2a9fbc1afeee4c4fefbf852",
                period: "28b5b990053c425b99",
                applyStatus: default,
                guaranteeMethod: default,
                applyTime: "d838800",
                passedTime: "aeb1cc5c2be040b28dabec1a83961932e5cf7bc7c8c64e888f2fd03e6aea00fccf350c4024224e64a69e3c3f510ac46"
            ));
        }
    }
}