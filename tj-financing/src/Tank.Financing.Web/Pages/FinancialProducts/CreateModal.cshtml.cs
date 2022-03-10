using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tank.Financing.FinancialProducts;

namespace Tank.Financing.Web.Pages.FinancialProducts
{
    public class CreateModalModel : FinancingPageModel
    {
        [BindProperty]
        public FinancialProductCreateDto FinancialProduct { get; set; }

        private readonly IFinancialProductsAppService _financialProductsAppService;

        public CreateModalModel(IFinancialProductsAppService financialProductsAppService)
        {
            _financialProductsAppService = financialProductsAppService;
        }

        public async Task OnGetAsync()
        {
            FinancialProduct = new FinancialProductCreateDto();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _financialProductsAppService.CreateAsync(FinancialProduct);
            return NoContent();
        }
    }
}