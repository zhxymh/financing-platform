using System;
using System.Linq;
using System.Threading.Tasks;
using AElf;
using AElf.Contracts.Delegator;
using AElf.ContractTestBase.ContractTestKit;
using AElf.Types;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Shouldly;
using Xunit;

namespace Tank.Contracts.Financing
{
    public class FinancingContractTests : FinancingContractTestBase
    {
        [Fact]
        public async Task Test()
        {
            var adminAccount = SampleAccount.Accounts.First();
            var organizationAccount = SampleAccount.Accounts.Skip(1).First();
            var enterpriseAccount = SampleAccount.Accounts.Skip(2).First();

            var financingStub = GetFinancingContractStub(adminAccount.KeyPair);

            var admin = GetDelegatorContractStub(adminAccount.KeyPair);
            var organization = GetDelegatorContractStub(organizationAccount.KeyPair);
            var enterprise = GetDelegatorContractStub(enterpriseAccount.KeyPair);

            await financingStub.Initialize.SendAsync(new InitializeInput
            {
                DelegatorContractAddress = DelegatorContractAddress,
                Admins = { adminAccount.Address },
                FinancingOrganizations = { organizationAccount.Address },
                Enterprises = { enterpriseAccount.Address }
            });

            #region Check Permission

            {
                var isPermitted = await admin.IsPermittedAddress.CallAsync(new IsPermittedAddressInput
                {
                    ToAddress = DAppContractAddress,
                    ScopeId = "Admin",
                    Address = adminAccount.Address
                });
                isPermitted.Value.ShouldBeTrue();
            }

            {
                var isPermitted = await admin.IsPermittedAddress.CallAsync(new IsPermittedAddressInput
                {
                    ToAddress = DAppContractAddress,
                    ScopeId = "Admin",
                    Address = enterpriseAccount.Address
                });
                isPermitted.Value.ShouldBeFalse();
            }

            {
                var isPermitted = await admin.IsPermittedMethod.CallAsync(new IsPermittedMethodInput
                {
                    ToAddress = DAppContractAddress,
                    ScopeId = "Admin",
                    MethodName = "AddFinancingProduct"
                });
                isPermitted.Value.ShouldBeTrue();
            }

            {
                var isPermitted = await admin.IsPermittedMethod.CallAsync(new IsPermittedMethodInput
                {
                    ToAddress = DAppContractAddress,
                    ScopeId = "Enterprise",
                    MethodName = "AddFinancingProduct"
                });
                isPermitted.Value.ShouldBeFalse();
            }

            #endregion

            {
                var executionResult = await financingStub.AddFinancingProduct.SendWithExceptionAsync(
                    new AddFinancingProductInput
                    {
                        Organization = "中国银行",
                        ProductName = "科技贷",
                        Url = "kejidai.com"
                    });
                executionResult.TransactionResult.Error.ShouldContain("Forward check failed.");
            }

            await admin.Forward.SendAsync(new ForwardInput
            {
                ToAddress = DAppContractAddress,
                FromId = "中国银行",
                MethodName = "AddFinancingProduct",
                Parameter = new AddFinancingProductInput
                {
                    Organization = "中国银行",
                    ProductName = "科技贷",
                    Url = "kejidai.com"
                }.ToByteString(),
                ScopeId = "Admin"
            });

            var productList = await financingStub.GetFinancingProductList.CallAsync(new Empty());
            productList.Value.Count.ShouldBe(1);
            productList.Value.First().Organization.ShouldBe("中国银行");

            #region Permission cases

            {
                var executionResult = await admin.Forward.SendWithExceptionAsync(new ForwardInput
                {
                    ToAddress = DAppContractAddress,
                    FromId = "天津引元信息科技有限公司",
                    MethodName = "Apply",
                    Parameter = new ApplyInput
                    {
                        EnterpriseName = "天津引元信息科技有限公司",
                        Organization = "中国银行",
                        ProductName = "科技贷"
                    }.ToByteString(),
                    ScopeId = "Enterprise"
                });
                executionResult.TransactionResult.Error.ShouldContain("[Sender] No permission.");
            }

            {
                var executionResult = await admin.Forward.SendWithExceptionAsync(new ForwardInput
                {
                    ToAddress = DAppContractAddress,
                    FromId = "天津引元信息科技有限公司",
                    MethodName = "Apply",
                    Parameter = new ApplyInput
                    {
                        EnterpriseName = "天津引元信息科技有限公司",
                        Organization = "中国银行",
                        ProductName = "科技贷"
                    }.ToByteString(),
                    ScopeId = "Admin"
                });
                executionResult.TransactionResult.Error.ShouldContain("[MethodName] No permission.");
            }

            #endregion

            {
                // Try to apply before certificated.
                var executionResult = await enterprise.Forward.SendWithExceptionAsync(new ForwardInput
                {
                    ToAddress = DAppContractAddress,
                    FromId = "天津引元信息科技有限公司",
                    MethodName = "Apply",
                    Parameter = new ApplyInput
                    {
                        EnterpriseName = "天津引元信息科技有限公司",
                        Organization = "中国银行",
                        ProductName = "科技贷"
                    }.ToByteString(),
                    ScopeId = "Enterprise"
                });
                executionResult.TransactionResult.Error.ShouldContain("Enterprise information not found.");
            }

            await enterprise.Forward.SendAsync(new ForwardInput
            {
                ToAddress = DAppContractAddress,
                FromId = "天津引元信息科技有限公司",
                MethodName = "Certificate",
                Parameter = new EnterpriseBasicInfo
                {
                    Name = "天津引元信息科技有限公司",
                    ArtificialPerson = "nw",
                    ArtificialPersonId = HashHelper.ComputeFrom("12345"),
                    CreditCode = "91120103MA07DC9C29",
                    RegisteredCapital = "10000000",
                    PhoneNumber = "13111111111",
                    EstablishedTime = "2016-01-01",
                    PhotoHashes =
                    {
                        HashHelper.ComputeFrom("test1"),
                        HashHelper.ComputeFrom("test2"),
                        HashHelper.ComputeFrom("test3"),
                    }
                }.ToByteString(),
                ScopeId = "Enterprise"
            });

            {
                // Try to apply before certificate confirmed.
                var executionResult = await enterprise.Forward.SendWithExceptionAsync(new ForwardInput
                {
                    ToAddress = DAppContractAddress,
                    FromId = "天津引元信息科技有限公司",
                    MethodName = "Apply",
                    Parameter = new ApplyInput
                    {
                        EnterpriseName = "天津引元信息科技有限公司",
                        Organization = "中国银行",
                        ProductName = "科技贷"
                    }.ToByteString(),
                    ScopeId = "Enterprise"
                });
                executionResult.TransactionResult.Error.ShouldContain("Enterprise certificate not confirmed.");
            }

            await admin.Forward.SendAsync(new ForwardInput
            {
                ToAddress = DAppContractAddress,
                FromId = "中国银行",
                MethodName = "ConfirmCertificate",
                Parameter = new ConfirmCertificateInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    IsConfirm = true
                }.ToByteString(),
                ScopeId = "Admin"
            });

            await enterprise.Forward.SendAsync(new ForwardInput
            {
                ToAddress = DAppContractAddress,
                FromId = "天津引元信息科技有限公司",
                MethodName = "Apply",
                Parameter = new ApplyInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    Organization = "中国银行",
                    ProductName = "科技贷"
                }.ToByteString(),
                ScopeId = "Enterprise"
            });

            {
                var applyRecord = await financingStub.GetApplyRecord.CallAsync(new GetApplyRecordInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    Organization = "中国银行",
                    ProductName = "科技贷"
                });
                applyRecord.EnterpriseName.ShouldBe("天津引元信息科技有限公司");
            }

            await organization.Forward.SendAsync(new ForwardInput
            {
                ToAddress = DAppContractAddress,
                FromId = "中国银行",
                MethodName = "SetAllowance",
                Parameter = new SetAllowanceInput
                {
                    Allowance = "10000000",
                    Apy = "10%",
                    EnterpriseName = "天津引元信息科技有限公司",
                    GuaranteeMethod = "抵押",
                    Organization = "中国银行",
                    ProductName = "科技贷",
                    Period = "1-12"
                }.ToByteString(),
                ScopeId = "FinancingOrganization"
            });

            {
                var applyRecord = await financingStub.GetApplyRecord.CallAsync(new GetApplyRecordInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    Organization = "中国银行",
                    ProductName = "科技贷"
                });
                applyRecord.Allowance.ShouldBe("10000000");
                applyRecord.Period.ShouldBe("1-12");
                applyRecord.ApplyStatus.ShouldBe(ApplyStatus.Pending);
            }
            
            await organization.Forward.SendAsync(new ForwardInput
            {
                ToAddress = DAppContractAddress,
                FromId = "中国银行",
                MethodName = "OnlineApprove",
                Parameter = new ApproveInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    Organization = "中国银行",
                    ProductName = "科技贷",
                }.ToByteString(),
                ScopeId = "FinancingOrganization"
            });
            
            {
                var applyRecord = await financingStub.GetApplyRecord.CallAsync(new GetApplyRecordInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    Organization = "中国银行",
                    ProductName = "科技贷"
                });
                applyRecord.ApplyStatus.ShouldBe(ApplyStatus.OnlinePassed);
            }
            
            await organization.Forward.SendAsync(new ForwardInput
            {
                ToAddress = DAppContractAddress,
                FromId = "中国银行",
                MethodName = "OfflineApprove",
                Parameter = new ApproveInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    Organization = "中国银行",
                    ProductName = "科技贷",
                }.ToByteString(),
                ScopeId = "FinancingOrganization"
            });
            
            {
                var applyRecord = await financingStub.GetApplyRecord.CallAsync(new GetApplyRecordInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    Organization = "中国银行",
                    ProductName = "科技贷"
                });
                applyRecord.ApplyStatus.ShouldBe(ApplyStatus.OfflinePassed);
            }

            {
                var applyRecordList = await financingStub.GetApplyRecordList.CallAsync(new GetApplyRecordListInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                });
                applyRecordList.Value.Count.ShouldBe(1);
            }
            
            {
                var applyRecordList = await financingStub.GetApplyRecordList.CallAsync(new GetApplyRecordListInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    ApplyStatus = ApplyStatus.Passed
                });
                applyRecordList.Value.Count.ShouldBe(0);
            }
            
            await organization.Forward.SendAsync(new ForwardInput
            {
                ToAddress = DAppContractAddress,
                FromId = "中国银行",
                MethodName = "ApproveAllowance",
                Parameter = new ApproveInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    Organization = "中国银行",
                    ProductName = "科技贷",
                }.ToByteString(),
                ScopeId = "FinancingOrganization"
            });
            
            {
                var applyRecordList = await financingStub.GetApplyRecordList.CallAsync(new GetApplyRecordListInput
                {
                    EnterpriseName = "天津引元信息科技有限公司",
                    ApplyStatus = ApplyStatus.Passed
                });
                applyRecordList.Value.Count.ShouldBe(1);
            }
        }
    }
}