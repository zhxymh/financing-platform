using Tank.Financing.FinancialProducts;

using System;
using Volo.Abp.Application.Dtos;

namespace Tank.Financing.FinancialProducts
{
    public class FinancialProductWithNavigationPropertiesDto
    {
        public FinancialProductDto FinancialProduct { get; set; }

        public FinancialProductDto FinancialProduct1 { get; set; }

    }
}