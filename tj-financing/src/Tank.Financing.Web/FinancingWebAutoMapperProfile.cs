using Tank.Financing.EnterpriseDetails;
using Tank.Financing.Applies;
using Tank.Financing.FinancialProducts;
using Tank.Financing.Enterprises;
using AutoMapper;

namespace Tank.Financing.Web;

public class FinancingWebAutoMapperProfile : Profile
{
    public FinancingWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project

        CreateMap<EnterpriseDto, EnterpriseUpdateDto>();

        CreateMap<FinancialProductDto, FinancialProductUpdateDto>();

        CreateMap<ApplyDto, ApplyUpdateDto>();

        CreateMap<EnterpriseDetailDto, EnterpriseDetailUpdateDto>();
    }
}