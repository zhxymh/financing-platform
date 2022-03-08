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
                id: Guid.Parse("4c390729-1064-4b40-92b0-8058ffdff0e0"),
                enterpriseName: "5f05f9bd4a1245e7a85257ad6974f49d5c90587557a944248eb20676959bd5b08702a25fe4be40",
                artificialPerson: "704b5b75d0c444b491e262846dc503033d619fce456b4b9c9425b7",
                establishedTime: "ec2a05c6671348bc84b41585c3cc2c2bf9e83cd42bbe40be8d6cda41a0608b05fac4d712cac",
                dueTime: "48994ca869624b09821850077f8782c881da442cb57c4ba4ba54a01996bebcc34ec0a53544",
                creditCode: "72f3a928e2e24f69ac54e34cc0a9efaa4fc0709c4f8a449a87fd829619d611f97e237b901fef42d89faa1d",
                artificialPersonId: "86c9b44cd45048af90b57de1b815bb9b5dc9705e84b744ada307b6b2d6aeaf94abf",
                registeredCapital: "338710919180432c9439aa1d069a3237fdd81ea617f942e0aff0a693a9d7225211f0923fa7bf4d71a67c91a",
                phoneNumber: "c32419c11e584257bdc58d9fd59e15f21f744ed",
                certPhotoPath: "a2aabf04474548",
                idPhotoPath1: "8d1981d14e0b4ed68d628aaaf695e549cdceab3843bf447a85b771c9277ea1c",
                idPhotoPath2: "8b896fbad1a548988f3a43186e60a09bbb30ff315cfa46b0880098",
                certificateStatus: "feda696bc11246eda3bf0b1320ea056e5"
            ));

            await _enterpriseRepository.InsertAsync(new Enterprise
            (
                id: Guid.Parse("3edecb90-a45c-465b-937d-3cb7e96803e9"),
                enterpriseName: "0b5924eedff54322bd586b6979291d84889960d66d094518a9d90d7e07d517290193933551e144afb3c915874859a74d",
                artificialPerson: "ea72d243d49f45afbe0646fde542f68d8a9c9cae3fb34758a4b064e9268549e8c45937fe500c492ab3998",
                establishedTime: "02de7a78d",
                dueTime: "e1da9f4eb6324",
                creditCode: "05eac69dbe6644c1bae28e7976c9bdbb36eadc239f204c0a96d50f99a",
                artificialPersonId: "c773037",
                registeredCapital: "60bbd1554b824af48bf3187574fd3115b414476cd57e4d48b4ad0d622f7d732",
                phoneNumber: "ff3e608edd8146e587c88a180d145eb4e25de71a2175484bb27",
                certPhotoPath: "d5009c5fe37742eca998ff912e19ba5cb2d08c53899e40eb9bea566a87374099cf60d236a424",
                idPhotoPath1: "f3c7ebf1b579489f9c6638356e00879d",
                idPhotoPath2: "f4ccece0b11f4e67a41f964f16391958894893a2f4514f86bf604f8f6449b",
                certificateStatus: "5ee0ca8424f440a0bda8b1e7e9e1bba8a27066a66"
            ));
        }
    }
}