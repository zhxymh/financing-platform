using AElf.Sdk.CSharp.State;
using AElf.Types;

namespace AElf.Contracts.Delegator
{
    public class DelegatorContractState : ContractState
    {
        /// <summary>
        /// To Address -> Scope Id -> Sender Address -> Is Permitted.
        /// </summary>
        public MappedState<Address, string, Address, bool> IsPermittedAddressMap { get; set; }

        /// <summary>
        /// To Address -> Scope Id -> Method Name -> Is Permitted.
        /// </summary>
        public MappedState<Address, string, string, bool> IsPermittedMethodNameMap { get; set; }

        /// <summary>
        /// Tx Id -> ForwardRecord.
        /// </summary>
        public MappedState<Hash, ForwardRecord> ForwardRecordMap { get; set; }

        public MappedState<Hash, bool> TemporaryTxIdMap { get; set; }
    }
}