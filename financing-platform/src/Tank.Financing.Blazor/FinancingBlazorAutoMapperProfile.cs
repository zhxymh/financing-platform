using Tank.Financing.FinancialProducts;
using AutoMapper;

namespace Tank.Financing.Blazor;

public class FinancingBlazorAutoMapperProfile : Profile
{
    public FinancingBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.

        CreateMap<FinancialProductDto, FinancialProductUpdateDto>();
    }
}