using AElf.Sdk.CSharp;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;

namespace Tank.Contracts.Financing
{
    public partial class FinancingContract
    {
        public override Empty AddFinancingProduct(AddFinancingProductInput input)
        {
            AssertSenderIsDelegatorContract();
            var financingProduct = new FinancingProduct
            {
                Organization = input.Organization,
                ProductName = input.ProductName,
                Url = input.Url
            };
            var productList = State.FinancingProductList.Value ?? new FinancingProductList();
            productList.Value.Add(financingProduct.Clone());
            State.FinancingProductList.Value = productList;

            State.FinancingProductMap[CalculateProductHash(input.Organization, input.ProductName)] = financingProduct;

            return new Empty();
        }

        public override Empty RemoveFinancingProduct(RemoveFinancingProductInput input)
        {
            AssertSenderIsDelegatorContract();
            var productHash = CalculateProductHash(input.Organization, input.ProductName);
            var financingProduct = State.FinancingProductMap[productHash];
            if (financingProduct == null)
            {
                throw new AssertionException("Financing product not found.");
            }
            var productList = State.FinancingProductList.Value;
            productList.Value.Remove(financingProduct);
            State.FinancingProductList.Value = productList;

            State.FinancingProductMap.Remove(productHash);

            return new Empty();
        }

        public override Empty ConfirmCertificate(ConfirmCertificateInput input)
        {
            AssertSenderIsDelegatorContract();
            var virtualAddress = State.EnterpriseVirtualAddressMap[input.EnterpriseName];
            if (virtualAddress == null)
            {
                throw new AssertionException($"Information of {input.EnterpriseName} not found.");
            }

            var enterpriseInfo = State.EnterpriseInfoMap[virtualAddress];
            if (enterpriseInfo == null)
            {
                throw new AssertionException($"Information of {input.EnterpriseName} not found.");
            }

            enterpriseInfo.CertificateStatus =
                input.IsConfirm ? CertificateStatus.Confirmed : CertificateStatus.Rejected;
            return new Empty();
        }

        public override Empty RemoveEnterpriseInfo(StringValue input)
        {
            AssertSenderIsDelegatorContract();
            var virtualAddress = State.EnterpriseVirtualAddressMap[input.Value];
            if (virtualAddress == null)
            {
                throw new AssertionException($"Information of {input.Value} not found.");
            }

            var enterpriseInfo = State.EnterpriseInfoMap[virtualAddress];
            if (enterpriseInfo == null)
            {
                throw new AssertionException($"Information of {input.Value} not found.");
            }

            State.EnterpriseVirtualAddressMap.Remove(input.Value);
            State.EnterpriseInfoMap.Remove(virtualAddress);
            return new Empty();
        }
    }
}