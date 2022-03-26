using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AElf;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Tank.Financing.Permissions;
using Volo.Abp.Data;

namespace Tank.Financing.EnterpriseDetails
{

    //[Authorize(FinancingPermissions.EnterpriseDetails.Default)]
    public class EnterpriseDetailsAppService : ApplicationService, IEnterpriseDetailsAppService
    {
        private readonly IEnterpriseDetailRepository _enterpriseDetailRepository;
        private readonly IBlockchainAppService _blockchainAppService;
        private readonly EnterpriseDetailExtraInfoOptions _enterpriseDetailExtraInfoOptions;

        public EnterpriseDetailsAppService(IEnterpriseDetailRepository enterpriseDetailRepository, IBlockchainAppService blockchainAppService,IOptionsSnapshot<EnterpriseDetailExtraInfoOptions> enterpriseDetailExtraInfoOptions)
        {
            _enterpriseDetailRepository = enterpriseDetailRepository;
            _blockchainAppService = blockchainAppService;
            _enterpriseDetailExtraInfoOptions = enterpriseDetailExtraInfoOptions.Value;
        }

        public virtual async Task<PagedResultDto<EnterpriseDetailDto>> GetListAsync(GetEnterpriseDetailsInput input)
        {
            var totalCount = await _enterpriseDetailRepository.GetCountAsync(input.FilterText, input.EnterpriseName,
                input.TotalAssets, input.Income, input.EnterpriseType, input.StaffNumberMin, input.StaffNumberMax,
                input.Industry, input.Location, input.RegisteredAddress, input.BusinessAddress, input.BusinessScope,
                input.Description, input.CompleteTxId, input.CommitUserName);
            var items = await _enterpriseDetailRepository.GetListAsync(input.FilterText, input.EnterpriseName,
                input.TotalAssets, input.Income, input.EnterpriseType, input.StaffNumberMin, input.StaffNumberMax,
                input.Industry, input.Location, input.RegisteredAddress, input.BusinessAddress, input.BusinessScope,
                input.Description, input.CompleteTxId, input.CommitUserName, input.Sorting, input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<EnterpriseDetailDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EnterpriseDetail>, List<EnterpriseDetailDto>>(items)
            };
        }

        public virtual async Task<EnterpriseDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(await _enterpriseDetailRepository.GetAsync(id));
        }

        [Authorize(FinancingPermissions.EnterpriseDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _enterpriseDetailRepository.DeleteAsync(id);
        }

        [Authorize(FinancingPermissions.EnterpriseDetails.Create)]
        public virtual async Task<EnterpriseDetailDto> CreateAsync(EnterpriseDetailCreateDto input)
        {
            // Enterprise cannot dup.
            var maybeSameEnterpriseNameList = await GetListAsync(new GetEnterpriseDetailsInput
            {
                EnterpriseName = input.EnterpriseName
            });
            if (maybeSameEnterpriseNameList.TotalCount > 0)
            {
                throw new UserFriendlyException("企业名称已经存在");
            }

            input.CompleteTxId = _blockchainAppService.Complete(input);
            var enterpriseDetail = ObjectMapper.Map<EnterpriseDetailCreateDto, EnterpriseDetail>(input);
            if (input.ExtraInfoFile != null)
            {
                var extraInfo = GetEnterpriseDetailExtraInfo(input.ExtraInfoFile.OpenReadStream());
                enterpriseDetail.SetProperty("EnterpriseDetailExtraInfo", extraInfo);
                enterpriseDetail.ExtraInfoHash = HashHelper.ComputeFrom(JsonConvert.SerializeObject(extraInfo)).ToHex();
            }
            enterpriseDetail = await _enterpriseDetailRepository.InsertAsync(enterpriseDetail, autoSave: true);
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(enterpriseDetail);
        }

        [Authorize(FinancingPermissions.EnterpriseDetails.Edit)]
        public virtual async Task<EnterpriseDetailDto> UpdateAsync(Guid id, EnterpriseDetailUpdateDto input)
        {
            input.CompleteTxId = _blockchainAppService.Complete(new EnterpriseDetailCreateDto
            {
                BusinessAddress = input.BusinessAddress,
                BusinessScope = input.BusinessScope,
                Description = input.Description,
                EnterpriseName = input.EnterpriseName,
                EnterpriseType = input.EnterpriseType,
                Income = input.Income,
                Industry = input.Industry,
                Location = input.Location,
                RegisteredAddress = input.RegisteredAddress,
                StaffNumber = input.StaffNumber,
                TotalAssets = input.TotalAssets,
                CommitUserName = input.CommitUserName
            });
            var enterpriseDetail = await _enterpriseDetailRepository.GetAsync(id);
            ObjectMapper.Map(input, enterpriseDetail);
            if (input.ExtraInfoFile != null)
            {
                var extraInfo = GetEnterpriseDetailExtraInfo(input.ExtraInfoFile.OpenReadStream());
                enterpriseDetail.SetProperty("EnterpriseDetailExtraInfo", extraInfo);
                enterpriseDetail.ExtraInfoHash = HashHelper.ComputeFrom(JsonConvert.SerializeObject(extraInfo)).ToHex();
            }
            enterpriseDetail = await _enterpriseDetailRepository.UpdateAsync(enterpriseDetail, autoSave: true);
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(enterpriseDetail);
        }

        private Dictionary<string, string> GetEnterpriseDetailExtraInfo(Stream stream)
        {
            var extraInfo = new Dictionary<string, string>();
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    while (reader.Read())
                    {
                        var key =  $"{reader.GetString(0)}-{reader.GetString(1)}";
                        if (_enterpriseDetailExtraInfoOptions.EnterpriseDetail.TryGetValue(key,
                                out var enterpriseDetailInfoKey)
                            && !string.IsNullOrWhiteSpace(enterpriseDetailInfoKey))
                        {
                            //if(reader.GetFieldType(3) == typeof(string))
                            var value = reader.GetValue(3);
                            extraInfo[enterpriseDetailInfoKey] = value==null?"":value.ToString();
                        }
                    }
                } while (reader.NextResult());
            }

            return extraInfo;
        }

        public FileResult GetEnterpriseInformationTemplate()
        {
            return new VirtualFileResult("download/企业信息模板.xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}