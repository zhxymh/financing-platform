using Tank.Financing.EnterpriseDetails;
using Tank.Financing.Applies;
using Tank.Financing.FinancialProducts;
using System;
using Tank.Financing.Shared;
using Volo.Abp.AutoMapper;
using Tank.Financing.Enterprises;
using AutoMapper;

namespace Tank.Financing;

public class FinancingApplicationAutoMapperProfile : Profile
{
    public FinancingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<EnterpriseCreateDto, Enterprise>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<EnterpriseUpdateDto, Enterprise>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<Enterprise, EnterpriseDto>();

        CreateMap<FinancialProductCreateDto, FinancialProduct>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<FinancialProductUpdateDto, FinancialProduct>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<FinancialProduct, FinancialProductDto>();

        CreateMap<ApplyCreateDto, Apply>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<ApplyUpdateDto, Apply>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<Apply, ApplyDto>();

        CreateMap<EnterpriseDetailCreateDto, EnterpriseDetail>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<EnterpriseDetailUpdateDto, EnterpriseDetail>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<EnterpriseDetail, EnterpriseDetailDto>();
    }
}