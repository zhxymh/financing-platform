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
                id: Guid.Parse("5ec03ba0-82da-4fc7-85c6-67d5955baa61"),
                enterpriseName: "dd16a023fede4603ac81fe8403ffb431cedf21cd5f80461b81391eeceaa94f8095c02eb0eb0b4ac2994d0558854c67254",
                artificialPerson: "05f5e68a75384f2790716941b2764048265f62e18ba44565bf7b21f352ec0b",
                establishedTime: 2043034036,
                dueTime: 1033484253,
                creditCode: "382169d190274ff5bd2b5d007c69d42a7127365865a044c2ace",
                artificialPersonId: "460dad04f40a4b948100a5abb5e69a6fdec64f2bae9d486f9",
                registeredCapital: "b9724cd61abc4e08a378cb2e12235a4ae571affe0b10463caea11d099ba5f1223518fd4b42c74778bef8eed295",
                phoneNumber: "64949d27a0ef4207ba4504dca19bfc5b90548bbd69ab441c9a7a3208ef75f9203d22417750fb4561bb0cd5d375a071f4c",
                certPhotoPath: "3254d19a84f943c3932c2287536f172",
                idPhotoPath1: "f29190b99c1f4506963f2374f82ba2a1c5eb561e2f6d496189950a8f55719c39c1c",
                idPhotoPath2: "c344a508c05d46cb839a8676c9e0101b",
                certificateStatus: default,
                certificateTxId: "e9524043cc944435956d94ab4ee0b0b5266f8547e29e4e9dae5",
                confirmCertificateTxId: "8c181d5b3ccc4674b2a"
            ));

            await _enterpriseRepository.InsertAsync(new Enterprise
            (
                id: Guid.Parse("401bb822-9cef-4c0c-b676-5f55cdf4ac97"),
                enterpriseName: "1ba59f68db364e3b991",
                artificialPerson: "78ab55ee33894e54aae0580d207d3d2e097c0b00c876446cb1d0ee9f0208bdafbb2",
                establishedTime: 1375055754,
                dueTime: 846643164,
                creditCode: "696d53b7c5ff489598f2dc798",
                artificialPersonId: "1481074f8995429bb0851bd2f002",
                registeredCapital: "0599ace38d814faaa66a9ab06257b6328358bcbd3ed0",
                phoneNumber: "cd80b86a69a54a9bb451320671c1146c0e2f4653c",
                certPhotoPath: "4df8549922544afc9d0a772a966ba1d5a1f5f09fa9cc4762bbf1a7950b242f9cc8b1b133f1c544a1908b",
                idPhotoPath1: "1724fa406fac4b0aa705c8c6dd846a0d993b8e6a2421411282b7e81e77a9876b238932877f6f4364a7ce1b",
                idPhotoPath2: "f6dcad4f27a84355bceec8fb63f021b",
                certificateStatus: default,
                certificateTxId: "50ed0fb2cefd47e497ad3bd6461af8ccb14ab1ea1016404eb628",
                confirmCertificateTxId: "8a046dddc"
            ));
        }
    }
}