using Google.Protobuf.WellKnownTypes;

namespace Tank.Contracts.Financing
{
    public partial class FinancingContract
    {
        public override AddressList GetAdminAddressList(Empty input)
        {
            return State.Admins.Value;
        }

        public override AddressList GetOrganizationAddressList(Empty input)
        {
            return State.Organizations.Value;
        }

        public override AddressList GetEnterpriseAddressList(Empty input)
        {
            return State.Enterprises.Value;
        }

        public override FinancingProductList GetFinancingProductList(Empty input)
        {
            return State.FinancingProductList.Value;
        }

        public override EnterpriseInfo GetEnterpriseInfo(StringValue input)
        {
            var virtualAddress = State.EnterpriseVirtualAddressMap[input.Value];
            return virtualAddress == null ? new EnterpriseInfo() : State.EnterpriseInfoMap[virtualAddress];
        }

        public override ApplyRecord GetApplyRecord(GetApplyRecordInput input)
        {
            var virtualAddress = State.EnterpriseVirtualAddressMap[input.EnterpriseName];
            var productHash = CalculateProductHash(input.Organization, input.ProductName);
            return State.ApplyRecordMap[virtualAddress][productHash];
        }

        public override ApplyRecordList GetApplyRecordList(GetApplyRecordListInput input)
        {
            var virtualAddress = State.EnterpriseVirtualAddressMap[input.EnterpriseName];
            var productHashList = State.ApplyRecordListMap[virtualAddress];
            var applyRecordList = new ApplyRecordList();
            foreach (var productHash in productHashList.Value)
            {
                var applyRecord = State.ApplyRecordMap[virtualAddress][productHash];
                if (FilterApplyRecord(input, applyRecord))
                {
                    applyRecordList.Value.Add(applyRecord);
                }
            }

            return applyRecordList;
        }

        private bool FilterApplyRecord(GetApplyRecordListInput input, ApplyRecord applyRecord)
        {
            if (!string.IsNullOrEmpty(input.Organization))
            {
                if (applyRecord.Organization != input.Organization)
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(input.ProductName))
            {
                if (applyRecord.ProductName != input.ProductName)
                {
                    return false;
                }
            }

            if (input.ApplyStatus != ApplyStatus.NotApplied)
            {
                if (applyRecord.ApplyStatus != input.ApplyStatus)
                {
                    return false;
                }
            }

            if (input.StartTime != null)
            {
                if (applyRecord.ApplyTime < input.StartTime)
                {
                    return false;
                }
            }

            if (input.EndTime != null)
            {
                if (applyRecord.ApplyTime > input.EndTime)
                {
                    return false;
                }
            }

            return true;
        }
    }
}