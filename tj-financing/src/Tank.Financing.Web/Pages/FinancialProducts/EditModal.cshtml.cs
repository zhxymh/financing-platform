using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Tank.Financing.FinancialProducts;

namespace Tank.Financing.Web.Pages.FinancialProducts
{
    public class EditModalModel : FinancingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public FinancialProductUpdateDto FinancialProduct { get; set; }

        private readonly IFinancialProductsAppService _financialProductsAppService;

        public EditModalModel(IFinancialProductsAppService financialProductsAppService)
        {
            _financialProductsAppService = financialProductsAppService;
        }

        public async Task OnGetAsync()
        {
            var financialProduct = await _financialProductsAppService.GetAsync(Id);
            FinancialProduct = ObjectMapper.Map<FinancialProductDto, FinancialProductUpdateDto>(financialProduct);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _financialProductsAppService.UpdateAsync(Id, FinancialProduct);
            return NoContent();
        }
    }
}