using System;
using Tank.Financing.Shared;
using Volo.Abp.AutoMapper;
using Tank.Financing.FinancialProducts;
using AutoMapper;

namespace Tank.Financing;

public class FinancingApplicationAutoMapperProfile : Profile
{
    public FinancingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<FinancialProductCreateDto, FinancialProduct>().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<FinancialProductUpdateDto, FinancialProduct>().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
        CreateMap<FinancialProduct, FinancialProductDto>();

        CreateMap<FinancialProductWithNavigationProperties, FinancialProductWithNavigationPropertiesDto>();
        CreateMap<FinancialProduct, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}