using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
            
            var enterpriseDetail = ObjectMapper.Map<EnterpriseDetailCreateDto, EnterpriseDetail>(input);
            Dictionary<string,string> extraInfo = null;
            if (input.ExtraInfoFile != null)
            {
                extraInfo = GetEnterpriseDetailExtraInfo(input.ExtraInfoFile.OpenReadStream());
                enterpriseDetail.SetProperty("EnterpriseDetailExtraInfo", extraInfo);
            }
            enterpriseDetail = doEvaluate(enterpriseDetail, extraInfo);
            enterpriseDetail.CompleteTxId = _blockchainAppService.Complete(enterpriseDetail, extraInfo);
            enterpriseDetail = await _enterpriseDetailRepository.InsertAsync(enterpriseDetail, autoSave: true);
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(enterpriseDetail);
        }

        [Authorize(FinancingPermissions.EnterpriseDetails.Edit)]
        public virtual async Task<EnterpriseDetailDto> UpdateAsync(Guid id, EnterpriseDetailUpdateDto input)
        {
            var enterpriseDetail = await _enterpriseDetailRepository.GetAsync(id);
            ObjectMapper.Map(input, enterpriseDetail);
            Dictionary<string,string> extraInfo = null;
            if (input.ExtraInfoFile != null)
            { 
                extraInfo = GetEnterpriseDetailExtraInfo(input.ExtraInfoFile.OpenReadStream());
                enterpriseDetail.SetProperty("EnterpriseDetailExtraInfo", extraInfo);
            }
            enterpriseDetail = doEvaluate(enterpriseDetail, extraInfo);
            enterpriseDetail.CompleteTxId = _blockchainAppService.Complete(enterpriseDetail, extraInfo);
            enterpriseDetail = await _enterpriseDetailRepository.UpdateAsync(enterpriseDetail, autoSave: true);
            return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailDto>(enterpriseDetail);
        }

        public virtual async Task<EnterpriseDetailEvaluateDto> Evaluate(GetEnterpriseDetailsInput input)
        {
            var items = await _enterpriseDetailRepository.GetListAsync(null, input.EnterpriseName);
            if (items != null && items.Count > 0)
            {
                return ObjectMapper.Map<EnterpriseDetail, EnterpriseDetailEvaluateDto>(items[0]);
            }
            else
            {
                return null;
            }
        }

        private EnterpriseDetail doEvaluate(EnterpriseDetail enterpriseDetail, Dictionary<string, string> extrainfo)
        {
            var marketScore = 0;
            var manageScore = 0;
            var profitScore = 0;
            var financeScore = 0;
            var innovateScore = 0;
            var creditScore = 0;
            if (extrainfo.Count > 0)
            {
                foreach (var item in extrainfo)
                {
                    var prefix = item.Key.Split('-')[0].ToLower();
                    switch (prefix)
                    {
                        case "market":
                            marketScore += 2;
                            break;
                        case "manage":
                            manageScore += 2;
                            break;
                        case "profit":
                            profitScore += 2;
                            break;
                        case "finance":
                            financeScore += 2;
                            break;
                        case "innovate":
                            innovateScore += 2;
                            break;
                        case "credit":
                            creditScore += 2;
                            break;
                        default:
                            break;
                    }
                }
            }
    
            financeScore = financeScore / 120;
            profitScore = profitScore / 189;
            creditScore = creditScore / 118;
            manageScore = manageScore / 188;
            innovateScore = innovateScore / 84;
            marketScore = marketScore / 68;
            
            enterpriseDetail.FinanceScore = financeScore.ToString();
            enterpriseDetail.ProfitScore = profitScore.ToString();
            enterpriseDetail.CreditScore = creditScore.ToString();
            enterpriseDetail.ManageScore = manageScore.ToString();
            enterpriseDetail.InnovateScore = innovateScore.ToString();
            enterpriseDetail.MarketScore = marketScore.ToString();
            
            
            
            return enterpriseDetail;
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