using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Tank.Financing.Enterprises;

namespace Tank.Financing.Enterprises
{
    public class EnterprisesDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IEnterpriseRepository _enterpriseRepository;

        public EnterprisesDataSeedContributor(IEnterpriseRepository enterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _enterpriseRepository.InsertAsync(new Enterprise
            (
                id: Guid.Parse("182f027c-7825-4bf5-ad49-f4a8d78f5969"),
                enterpriseName: "0e9d816e010740968edbae3c6fad3d56e4398d2ddad645e291f2e97b77607779447282ecc63740008d",
                artificialPerson: "a95ded070c844cb0b1cf9436ac9da421e18214025d6a4",
                establishedTime: 387049905,
                dueTime: 1274250541,
                creditCode: "4273a432a3634f6d9817bccc2f8fb2714d1ceb83da1a4c0694b3fcb7a3065043b746cc46f5da4fb0bc43562d287a23c0cd5",
                artificialPersonId: "c0ddc000824e4f0ab7c9b3f3ed833401db3d6870631",
                registeredCapital: "5bc4c984310142168acc0192811186f21adf3d5ae4",
                phoneNumber: "d861a28a3b0b4ff59929e0b0c46d0ebb56e4bc2c935e48ec8dfda1914",
                certPhotoPath: "40032c1d1558434bb42f24721637e7ad929b51e8c2d24844b260b8712ed00e",
                idPhotoPath1: "0911757a86fc4f9498046dd6497232cb403",
                idPhotoPath2: "f4510a801c774674bd7fa37aed8f05be459915cacc7d4d8994193ce4b3d49ce2e67bb364eaf34a3",
                certificateStatus: default
            ));

            await _enterpriseRepository.InsertAsync(new Enterprise
            (
                id: Guid.Parse("8c950318-ded1-493f-ac4b-53a2fc4388e1"),
                enterpriseName: "3e8acff4dc1f4670a2f5a08c89d3ccae603e87dd0e144595912e322a5541e1",
                artificialPerson: "017ea7c93c574f9ead282a29e088d9f55384678ce5694cc9a8a5d15f09d66df71e6ba58381824afa8",
                establishedTime: 968446144,
                dueTime: 1216316083,
                creditCode: "0e9065d0db7d4e47ae08b1338ae8ece70788d5ee8e3a4cdb9d64f2869edf462c51a93bedb61e40219277a43a",
                artificialPersonId: "7363453112b1408bbbb7f2ab117f9b723d3d1f6b8e3b4f37a6fd0cebb73e850dcde68cda3fa64ba283ac",
                registeredCapital: "bf4f7b80dc8f45848c55cf61ead4302650813968089d43bc9d1105985be9ed8dfa74058dffb146a4827e0dc",
                phoneNumber: "5e14f1803e8849e2b6f44f9c9154b258ef6f97cfb6674ef2921e7707790d6201af10e26c41c640f98cc8e2f5b",
                certPhotoPath: "a99f03598ee84d919f955e0b",
                idPhotoPath1: "45ca4e3bece248eeb2fdb4e32e7ce89dd5bb2cbbe89b41e",
                idPhotoPath2: "3dc55c75d5514da8b4bb0c3af05",
                certificateStatus: default
            ));
        }
    }
}