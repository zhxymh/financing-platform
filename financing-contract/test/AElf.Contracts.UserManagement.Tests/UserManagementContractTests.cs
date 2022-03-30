using System;
using System.Linq;
using System.Threading.Tasks;
using AElf.Contracts.Delegator;
using AElf.ContractTestBase.ContractTestKit;
using AElf.Sdk.CSharp;
using AElf.Types;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Shouldly;
using Xunit;

namespace AElf.Contracts.UserManagement
{
    public class UserManagementContractTests : UserManagementContractTestBase
    {
        [Fact]
        public async Task Test()
        {
            var ownerAccount = SampleAccount.Accounts.First();
            var adminAccount = SampleAccount.Accounts.Skip(1).First();
            var userAccount = SampleAccount.Accounts.Skip(2).First();

            var userManagementStub = GetUserManagementContractStub(ownerAccount.KeyPair);

            var adminDelegatorStub = GetDelegatorContractStub(adminAccount.KeyPair);
            var userDelegatorStub = GetDelegatorContractStub(userAccount.KeyPair);

            await userManagementStub.Initialize.SendAsync(new InitializeInput
            {
                Owner = ownerAccount.Address
            });
            await userManagementStub.SetDelegatorContract.SendAsync(DelegatorContractAddress);
            await userManagementStub.SetAdminDelegators.SendAsync(new AddressList {Value = {adminAccount.Address}});
            await userManagementStub.SetUserDelegators.SendAsync(new AddressList {Value = {userAccount.Address}});

            var user = new UserInfo
            {
                UserName = "user-name",
                Name = "name",
                Email = "email@com"
            };

            var result = await adminDelegatorStub.Forward.SendWithExceptionAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Register",
                ScopeId = "User",
                ToAddress = DAppContractAddress,
                Parameter = user.ToByteString()
            });
            result.TransactionResult.Error.ShouldContain("No permission");
            
            result = await adminDelegatorStub.Forward.SendWithExceptionAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Register",
                ScopeId = "Admin",
                ToAddress = DAppContractAddress,
                Parameter = user.ToByteString()
            });
            result.TransactionResult.Error.ShouldContain("No permission");
            
            await userDelegatorStub.Forward.SendAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Register",
                ScopeId = "User",
                ToAddress = DAppContractAddress,
                Parameter = user.ToByteString()
            });

            var approvalList = await userManagementStub.GetApprovalList.CallAsync(new Empty());
            approvalList.Value.Count.ShouldBe(1);
            approvalList.Value[0].UserName.ShouldBe(user.UserName);
            approvalList.Value[0].Name.ShouldBe(user.Name);
            approvalList.Value[0].Email.ShouldBe(user.Email);
            
            result = await userDelegatorStub.Forward.SendWithExceptionAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Approve",
                ScopeId = "Admin",
                ToAddress = DAppContractAddress,
                Parameter = approvalList.Value[0].Id.ToByteString()
            });
            result.TransactionResult.Error.ShouldContain("No permission");
            
            await adminDelegatorStub.Forward.SendAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Approve",
                ScopeId = "Admin",
                ToAddress = DAppContractAddress,
                Parameter = approvalList.Value[0].Id.ToByteString()
            });
            
            approvalList = await userManagementStub.GetApprovalList.CallAsync(new Empty());
            approvalList.Value.Count.ShouldBe(0);
            
            var userResult = await userManagementStub.GetUser.CallAsync(new StringValue{Value = user.UserName});
            userResult.UserName.ShouldBe(user.UserName);
            userResult.Name.ShouldBe(user.Name);
            userResult.Email.ShouldBe(user.Email);

            user.Name = "NewName";
            result = await adminDelegatorStub.Forward.SendWithExceptionAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "ChangeUserInfo",
                ScopeId = "User",
                ToAddress = DAppContractAddress,
                Parameter = user.ToByteString()
            });
            result.TransactionResult.Error.ShouldContain("No permission");
            
            await userDelegatorStub.Forward.SendAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "ChangeUserInfo",
                ScopeId = "User",
                ToAddress = DAppContractAddress,
                Parameter = user.ToByteString()
            });
            userResult = await userManagementStub.GetUser.CallAsync(new StringValue{Value = user.UserName});
            userResult.UserName.ShouldBe(user.UserName);
            userResult.Name.ShouldBe(user.Name);
            userResult.Email.ShouldBe(user.Email);
            
            await userDelegatorStub.Forward.SendAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Register",
                ScopeId = "User",
                ToAddress = DAppContractAddress,
                Parameter = user.ToByteString()
            });
            approvalList = await userManagementStub.GetApprovalList.CallAsync(new Empty());
            
            result = await userDelegatorStub.Forward.SendWithExceptionAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Reject",
                ScopeId = "Admin",
                ToAddress = DAppContractAddress,
                Parameter = approvalList.Value[0].Id.ToByteString()
            });
            result.TransactionResult.Error.ShouldContain("No permission");
            
            await adminDelegatorStub.Forward.SendAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Reject",
                ScopeId = "Admin",
                ToAddress = DAppContractAddress,
                Parameter = approvalList.Value[0].Id.ToByteString()
            });
            approvalList = await userManagementStub.GetApprovalList.CallAsync(new Empty());
            approvalList.Value.Count.ShouldBe(0);
        }

        [Fact]
        public async Task DirectCallTest()
        {
            var ownerAccount = SampleAccount.Accounts.First();
            var adminAccount = SampleAccount.Accounts.Skip(1).First();
            var userAccount = SampleAccount.Accounts.Skip(2).First();

            var userManagementStub = GetUserManagementContractStub(ownerAccount.KeyPair);

            var adminDelegatorStub = GetDelegatorContractStub(adminAccount.KeyPair);
            var userDelegatorStub = GetDelegatorContractStub(userAccount.KeyPair);

            await userManagementStub.Initialize.SendAsync(new InitializeInput
            {
                Owner = ownerAccount.Address
            });
            await userManagementStub.SetDelegatorContract.SendAsync(DelegatorContractAddress);
            await userManagementStub.SetAdminDelegators.SendAsync(new AddressList {Value = {adminAccount.Address}});
            await userManagementStub.SetUserDelegators.SendAsync(new AddressList {Value = {userAccount.Address}});
            
            var user = new UserInfo
            {
                UserName = "user-name",
                Name = "name",
                Email = "email@com"
            };

            {
                var executiveResult = await userManagementStub.Register.SendWithExceptionAsync(user);
                executiveResult.TransactionResult.Error.ShouldContain("Forward check failed");
            }
            
            await userDelegatorStub.Forward.SendAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Register",
                ScopeId = "User",
                ToAddress = DAppContractAddress,
                Parameter = user.ToByteString()
            });
            var approvalList = await userManagementStub.GetApprovalList.CallAsync(new Empty());

            {
                var executiveResult = await userManagementStub.Approve.SendWithExceptionAsync(approvalList.Value[0].Id);
                executiveResult.TransactionResult.Error.ShouldContain("Forward check failed");
            }
            {
                var executiveResult = await userManagementStub.Reject.SendWithExceptionAsync(approvalList.Value[0].Id);
                executiveResult.TransactionResult.Error.ShouldContain("Forward check failed");
            }
            
            await adminDelegatorStub.Forward.SendAsync(new ForwardInput
            {
                FromId = Guid.NewGuid().ToString(),
                MethodName = "Approve",
                ScopeId = "Admin",
                ToAddress = DAppContractAddress,
                Parameter = approvalList.Value[0].Id.ToByteString()
            });
            {
                user.Name = "NewName";
                var executiveResult = await userManagementStub.ChangeUserInfo.SendWithExceptionAsync(user);
                executiveResult.TransactionResult.Error.ShouldContain("Forward check failed");
            }
        }

        [Fact]
        public async Task SetDelegatorTest()
        {
            var ownerAccount = SampleAccount.Accounts.First();
            var adminAccount = SampleAccount.Accounts.Skip(1).First();
            var userAccount = SampleAccount.Accounts.Skip(2).First();

            var userManagementStub = GetUserManagementContractStub(ownerAccount.KeyPair);

            var adminDelegatorStub = GetDelegatorContractStub(adminAccount.KeyPair);
            var userDelegatorStub = GetDelegatorContractStub(userAccount.KeyPair);
            
            await userManagementStub.Initialize.SendAsync(new InitializeInput
            {
                Owner = ownerAccount.Address
            });
            await userManagementStub.SetDelegatorContract.SendAsync(DelegatorContractAddress);
            
            await userManagementStub.SetAdminDelegators.SendAsync(new AddressList {Value = {adminAccount.Address}});

            var result = await adminDelegatorStub.IsPermittedAddress.CallAsync(new IsPermittedAddressInput
            {
                Address = adminAccount.Address,
                ScopeId = "Admin",
                ToAddress = DAppContractAddress
            });
            result.Value.ShouldBeTrue();
            
            await userManagementStub.SetAdminDelegators.SendAsync(new AddressList {Value = {adminAccount.Address, ownerAccount.Address}});
            result = await adminDelegatorStub.IsPermittedAddress.CallAsync(new IsPermittedAddressInput
            {
                Address = adminAccount.Address,
                ScopeId = "Admin",
                ToAddress = DAppContractAddress
            });
            result.Value.ShouldBeTrue();
            
            result = await adminDelegatorStub.IsPermittedAddress.CallAsync(new IsPermittedAddressInput
            {
                Address = ownerAccount.Address,
                ScopeId = "Admin",
                ToAddress = DAppContractAddress
            });
            result.Value.ShouldBeTrue();
            
            await userManagementStub.SetUserDelegators.SendAsync(new AddressList {Value = {userAccount.Address}});
            
            result = await adminDelegatorStub.IsPermittedAddress.CallAsync(new IsPermittedAddressInput
            {
                Address = userAccount.Address,
                ScopeId = "User",
                ToAddress = DAppContractAddress
            });
            result.Value.ShouldBeTrue();
            
            await userManagementStub.SetUserDelegators.SendAsync(new AddressList {Value = {userAccount.Address, ownerAccount.Address}});
            result = await adminDelegatorStub.IsPermittedAddress.CallAsync(new IsPermittedAddressInput
            {
                Address = userAccount.Address,
                ScopeId = "User",
                ToAddress = DAppContractAddress
            });
            result.Value.ShouldBeTrue();
            
            result = await adminDelegatorStub.IsPermittedAddress.CallAsync(new IsPermittedAddressInput
            {
                Address = ownerAccount.Address,
                ScopeId = "User",
                ToAddress = DAppContractAddress
            });
            result.Value.ShouldBeTrue();
        }
    }
}