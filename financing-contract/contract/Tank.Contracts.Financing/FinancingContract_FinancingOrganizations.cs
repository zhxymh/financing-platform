using AElf.Sdk.CSharp;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;

namespace Tank.Contracts.Financing
{
    public partial class FinancingContract
    {
        public override Empty SetAllowance(SetAllowanceInput input)
        {
            AssertSenderIsDelegatorContract();
            TryGetApplyRecord(input.EnterpriseName, input.Organization, input.ProductName, out var applyRecord,
                out var virtualAddress);
            Assert(applyRecord.ApplyStatus != ApplyStatus.Passed, "");
            applyRecord.Allowance = input.Allowance;
            applyRecord.Apr = input.Apr;
            applyRecord.Period = input.Period;
            applyRecord.GuaranteeMethod = input.GuaranteeMethod;

            var productHash = CalculateProductHash(input.Organization, input.ProductName);
            State.ApplyRecordMap[virtualAddress][productHash] = applyRecord;
            return new Empty();
        }

        public override Empty OnlineApprove(ApproveInput input)
        {
            TryGetApplyRecord(input.EnterpriseName, input.Organization, input.ProductName, out var applyRecord,
                out var virtualAddress);
            Assert(applyRecord.ApplyStatus == ApplyStatus.Pending, "Incorrect status");
            applyRecord.ApplyStatus = ApplyStatus.OnlinePassed;

            var productHash = CalculateProductHash(input.Organization, input.ProductName);
            State.ApplyRecordMap[virtualAddress][productHash] = applyRecord;
            return new Empty();
        }

        public override Empty OfflineApprove(ApproveInput input)
        {
            TryGetApplyRecord(input.EnterpriseName, input.Organization, input.ProductName, out var applyRecord,
                out var virtualAddress);
            Assert(applyRecord.ApplyStatus == ApplyStatus.OnlinePassed, "Incorrect status");
            applyRecord.ApplyStatus = ApplyStatus.OfflinePassed;

            var productHash = CalculateProductHash(input.Organization, input.ProductName);
            State.ApplyRecordMap[virtualAddress][productHash] = applyRecord;
            return new Empty();
        }

        private void TryGetApplyRecord(string enterpriseName, string organization, string productName,
            out ApplyRecord applyRecord, out Address virtualAddress)
        {
            virtualAddress = State.EnterpriseVirtualAddressMap[enterpriseName];
            if (virtualAddress == null)
            {
                throw new AssertionException($"Information of {enterpriseName} not found.");
            }

            var productHash = CalculateProductHash(organization, productName);
            applyRecord = State.ApplyRecordMap[virtualAddress][productHash];
            if (applyRecord == null)
            {
                throw new AssertionException($"Apply record not found.");
            }
        }

        public override Empty ApproveAllowance(ApproveAllowanceInput input)
        {
            TryGetApplyRecord(input.EnterpriseName, input.Organization, input.ProductName, out var applyRecord,
                out var virtualAddress);
            Assert(applyRecord.ApplyStatus == ApplyStatus.OfflinePassed, "Incorrect status");
            applyRecord.ApplyStatus = ApplyStatus.Passed;
            applyRecord.PassedTime = Context.CurrentBlockTime;

            if (!string.IsNullOrEmpty(input.Allowance))
            {
                applyRecord.Allowance = input.Allowance;
            }

            if (!string.IsNullOrEmpty(input.Apr))
            {
                applyRecord.Apr = input.Apr;
            }

            if (!string.IsNullOrEmpty(input.GuaranteeMethod))
            {
                applyRecord.GuaranteeMethod = input.GuaranteeMethod;
            }

            if (!string.IsNullOrEmpty(input.Period))
            {
                applyRecord.Period = input.Period;
            }

            var productHash = CalculateProductHash(input.Organization, input.ProductName);
            State.ApplyRecordMap[virtualAddress][productHash] = applyRecord;

            Context.Fire(new ApplyPassed
            {
                EnterpriseName = applyRecord.EnterpriseName,
                Organization = applyRecord.Organization,
                ProductName = applyRecord.ProductName,
                Allowance = applyRecord.Allowance,
                ApplyStatus = applyRecord.ApplyStatus,
                ApplyTime = applyRecord.ApplyTime,
                Apr = applyRecord.Apr,
                GuaranteeMethod = applyRecord.GuaranteeMethod,
                PassedTime = applyRecord.PassedTime,
                Period = applyRecord.Period
            });
            return new Empty();
        }
    }
}