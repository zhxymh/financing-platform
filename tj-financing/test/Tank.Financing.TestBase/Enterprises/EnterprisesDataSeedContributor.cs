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
                id: Guid.Parse("4b826053-560b-4e05-8560-2e6614ec7d71"),
                enterpriseName: "a35492ae8cd341f287a1eaf281a492ecec87735131ad48fd8c6792f95a4b0006c8eb43d856184cf4ae5b",
                artificialPerson: "fefdef5fee604609a3660d32f766431edfa39a6ead074efcbbf39a1bfcc3dfe",
                establishedTime: 424769377,
                dueTime: 516081424,
                creditCode: "97c05d9aa9a84dd49ea2831da28c57c9d30ba99e197546619527b5cd3dfe5f8",
                artificialPersonId: "f821430646374477a7b5f8f89f04bb5147e78badc04f42b292513823138f69",
                registeredCapital: "9c7b377e8ea643438d15de777dd782810",
                phoneNumber: "905891ccae334a7590f8152afb5b5df0b98a0b9497784fe8b931fbc9ada",
                certPhotoPath: "a6fa97431e8c45bcb088f05e010abc33e75cc465ef3b45",
                idPhotoPath1: "0cd6b829aa44498aac48b8149488a3d7689681e6e1394f41a88b59ea52c013fd9c0bec9c0109427990f4",
                idPhotoPath2: "4a9f700fc58e4f31b421f2a5191b7674aaf1b21f6b344e76a3c8f23e7f8dc7f2212b8fbb8d7f4899bf",
                certificateStatus: default
            ));

            await _enterpriseRepository.InsertAsync(new Enterprise
            (
                id: Guid.Parse("8cce2673-4bad-4744-b1cd-4a1fa3b81586"),
                enterpriseName: "1c08ee6fd5924d9a83b9e74b2bce2c0c1dc6b2967f604ca6a231d1203b5c4180797428a9e4cd49de85bdd39b59e28352",
                artificialPerson: "6e92170990e",
                establishedTime: 1956499670,
                dueTime: 1881130829,
                creditCode: "55422166bb8143febe8cf2de63c9d6df8f7ee26bda024b97a1b56aa772f81d35dc39a603cb1240a393240d",
                artificialPersonId: "62f887190f5442c184bd7bd76e0ad37066e94074e8c648b6b5e51cff2b58b284548d527ad623424b8579782",
                registeredCapital: "8509ad7a09e54c98a422f3d86f795c019343188e331741fe90f7d546c",
                phoneNumber: "91011ae1271c411fabd69cc48f34e132832e1cfec7ae41",
                certPhotoPath: "4ec113c8f59f4ae58950343baa",
                idPhotoPath1: "8448308b88e7458b9c82c530aa69d2",
                idPhotoPath2: "9e72a2e74a224c27870db48904372d4a6ff11e6949e547a3b1",
                certificateStatus: default
            ));
        }
    }
}