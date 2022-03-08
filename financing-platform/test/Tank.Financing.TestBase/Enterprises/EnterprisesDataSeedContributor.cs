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
                id: Guid.Parse("0705d2a1-c20a-4269-9786-dff792c49d3d"),
                enterpriseName: "23d5e1a00c5d4cbc9988810dafdca2d7fba39966e2d44987ad431fe48347833",
                artificialPerson: "6356dc5fae63438bbcde0b430",
                establishedTime: "da4621af757b4c569e36b82ac7",
                dueTime: "f8029068e8dd4d95b2c12213756d350c0ba0309039af4a3094571b496a6",
                creditCode: "470c6b8d35f54eb6b0a4b61453ec88751572dd1af66b418fb2aa45ee7aded1e7b61",
                artificialPersonId: "d530e7fd64e64861a",
                registeredCapital: "5a179e1e739f4eae8e6cfe65f949674208a44e325e6e4549a815f0e139becce1d96acecf173e413",
                phoneNumber: "4f89fbc4e8c24632b027a75db99c6f38b4780c3669964b4081bb2aeb1e6932c633197c5813",
                certPhotoPath: "6339042e3323475a9d7c5e7f80f5f9dc0778020bdc244abc9d6222a9cfb7df21a0780b5fe83c4385b3e46f7e034726a",
                idPhotoPath1: "ae35256a5d144cd1b1a39ee064f94da9d381ea9b70a44a7a9ec3325f524ef79b8a94c1e2b1e44c53bf9bbcc15029a165",
                idPhotoPath2: "e88f6439fc8c4f229be38cd05a5c70e",
                certificateStatus: default
            ));

            await _enterpriseRepository.InsertAsync(new Enterprise
            (
                id: Guid.Parse("03df4b2d-ccd0-40b8-8b5a-6dbb93ba3705"),
                enterpriseName: "d32f0ab958c34e108e44370d9d7b5fa243ca3518e0fd4c08b7ae6f2c",
                artificialPerson: "4dac2f9276844a5b96dea16394083a6c515881b06a094ef1a188b5bb61294fdcb171c4cd7c4e4f34b",
                establishedTime: "b2def3f",
                dueTime: "a1a5f466c03e4779a7835162a67e05973fee2b970fd942ee8ab0e88ad91fda4c6626",
                creditCode: "0dcd1e2c69834b76ac6c8a7bcb536feb0ea343f93c95",
                artificialPersonId: "eb14792a3162405eb256890670c0b89c5b39d108c47d44f59fef7ed22ee6ae",
                registeredCapital: "5a3de0bf38644a21b667c98a382cae3418ecdde9be01454ab2901e1c1b09c5",
                phoneNumber: "fc720a878f4d46c3ae3533a2cea17adee3f60a87ab024e5eb71cf64093d9a47c32ceb6bf49",
                certPhotoPath: "acb412ffaa1c40a08590bc417b8cd6fd0def2f4c3bf64ef7b9e3d47e1ea0a8ecda77d98880e243fd9496",
                idPhotoPath1: "bbb7722c559f4025a95a205ae555e566b8be5c0ef21e4bd48993399ed168774ff98f66e517914b09be",
                idPhotoPath2: "a714ecf49dbf48ada666cb2465585f59c3c65263f9044b7aa628432f002e3fa64df091adc31048f58833",
                certificateStatus: default
            ));
        }
    }
}