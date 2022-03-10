using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Tank.Financing.Applies;

namespace Tank.Financing.Web.Pages.Applies
{
    public class EditModalModel : FinancingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ApplyUpdateDto Apply { get; set; }

        private readonly IAppliesAppService _appliesAppService;

        public EditModalModel(IAppliesAppService appliesAppService)
        {
            _appliesAppService = appliesAppService;
        }

        public async Task OnGetAsync()
        {
            var apply = await _appliesAppService.GetAsync(Id);
            Apply = ObjectMapper.Map<ApplyDto, ApplyUpdateDto>(apply);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _appliesAppService.UpdateAsync(Id, Apply);
            return NoContent();
        }
    }
}