using AElf.Sdk.CSharp.State;
using AElf.Types;

namespace Tank.Contracts.Financing
{
    public partial class FinancingContractState : ContractState
    {
        public SingletonState<Address> Owner { get; set; }

        public SingletonState<AddressList> Admins { get; set; }
        public SingletonState<AddressList> Organizations { get; set; }
        public SingletonState<AddressList> Enterprises { get; set; }

        /// <summary>
        /// Hash of Organization Name & Financing Product Name -> Product Info.
        /// </summary>
        public MappedState<Hash, FinancingProduct> FinancingProductMap { get; set; }

        /// <summary>
        /// Some basic infos.
        /// </summary>
        public SingletonState<FinancingProductList> FinancingProductList { get; set; }

        /// <summary>
        /// Sender Address (Enterprise Virtual Address) -> Enterprise Info.
        /// </summary>
        public MappedState<Address, EnterpriseInfo> EnterpriseInfoMap { get; set; }

        public MappedState<string, Address> EnterpriseVirtualAddressMap { get; set; }

        /// <summary>
        /// Enterprise Virtual Address -> Hash of Organization Name & Financing Product Name -> Apply Record.
        /// </summary>
        public MappedState<Address, Hash, ApplyRecord> ApplyRecordMap { get; set; }

        /// <summary>
        /// Enterprise Virtual Address -> Product Hash List.
        /// </summary>
        public MappedState<Address, HashList> ApplyRecordListMap { get; set; }
    }
}