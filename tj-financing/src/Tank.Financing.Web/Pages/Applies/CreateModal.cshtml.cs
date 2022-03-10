using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tank.Financing.Applies;

namespace Tank.Financing.Web.Pages.Applies
{
    public class CreateModalModel : FinancingPageModel
    {
        [BindProperty]
        public ApplyCreateDto Apply { get; set; }

        private readonly IAppliesAppService _appliesAppService;

        public CreateModalModel(IAppliesAppService appliesAppService)
        {
            _appliesAppService = appliesAppService;
        }

        public async Task OnGetAsync()
        {
            Apply = new ApplyCreateDto();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _appliesAppService.CreateAsync(Apply);
            return NoContent();
        }
    }
}