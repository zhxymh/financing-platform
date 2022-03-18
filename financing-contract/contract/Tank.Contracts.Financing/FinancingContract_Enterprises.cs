using AElf.Sdk.CSharp;
using Google.Protobuf.WellKnownTypes;

namespace Tank.Contracts.Financing
{
    public partial class FinancingContract
    {
        public override Empty Certificate(EnterpriseBasicInfo input)
        {
            AssertSenderIsDelegatorContract();
            Assert(
                State.EnterpriseVirtualAddressMap[input.Name] == null ||
                State.EnterpriseVirtualAddressMap[input.Name] == Context.Sender,
                $"Enterprise {input.Name} bound to another account.");
            var enterpriseInfo = State.EnterpriseInfoMap[Context.Sender] ?? new EnterpriseInfo();
            enterpriseInfo.BasicInfo = input;
            enterpriseInfo.EnterpriseVirtualAddress = Context.Sender;

            // Set status to Waiting everytime basic information updated.
            enterpriseInfo.CertificateStatus = CertificateStatus.Waiting;

            State.EnterpriseInfoMap[Context.Sender] = enterpriseInfo;
            State.EnterpriseVirtualAddressMap[input.Name] = Context.Sender;
            return new Empty();
        }

        public override Empty Complete(EnterpriseFurtherInfo input)
        {
            AssertSenderIsDelegatorContract();
            var enterpriseInfo = State.EnterpriseInfoMap[Context.Sender];
            if (enterpriseInfo?.BasicInfo == null)
            {
                throw new AssertionException("Enterprise basic information not found.");
            }

            enterpriseInfo.FurtherInfo = input;
            State.EnterpriseInfoMap[Context.Sender] = enterpriseInfo;
            return new Empty();
        }

        public override Empty Apply(ApplyInput input)
        {
            AssertSenderIsDelegatorContract();
            // Check certificate.
            var enterpriseInfo = State.EnterpriseInfoMap[Context.Sender];
            if (enterpriseInfo?.BasicInfo == null)
            {
                throw new AssertionException($"Enterprise information of {input.EnterpriseName} not found.");
            }

            Assert(enterpriseInfo.CertificateStatus == CertificateStatus.Confirmed,
                "Enterprise certificate not confirmed.");

            var productHash = CalculateProductHash(input.Organization, input.ProductName);
            Assert(State.FinancingProductMap[productHash] != null, $"Product {input.ProductName} not found.");
            State.ApplyRecordMap[Context.Sender][productHash] = new ApplyRecord
            {
                EnterpriseName = input.EnterpriseName,
                Organization = input.Organization,
                ProductName = input.ProductName,
                ApplyStatus = ApplyStatus.Pending,
                ApplyTime = Context.CurrentBlockTime
            };

            var hashList = State.ApplyRecordListMap[Context.Sender] ?? new HashList();
            hashList.Value.Add(productHash);
            State.ApplyRecordListMap[Context.Sender] = hashList;

            return new Empty();
        }

        public override Empty Cancel(CancelInput input)
        {
            AssertSenderIsDelegatorContract();
            var productHash = CalculateProductHash(input.Organization, input.ProductName);
            State.ApplyRecordMap[Context.Sender].Remove(productHash);
            var hashList = State.ApplyRecordListMap[Context.Sender];
            hashList.Value.Remove(productHash);
            State.ApplyRecordListMap[Context.Sender] = hashList;
            return new Empty();
        }
    }
}