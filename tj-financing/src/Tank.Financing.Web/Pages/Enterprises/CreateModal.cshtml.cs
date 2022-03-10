using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tank.Financing.Enterprises;

namespace Tank.Financing.Web.Pages.Enterprises
{
    public class CreateModalModel : FinancingPageModel
    {
        [BindProperty]
        public EnterpriseCreateDto Enterprise { get; set; }

        private readonly IEnterprisesAppService _enterprisesAppService;

        public CreateModalModel(IEnterprisesAppService enterprisesAppService)
        {
            _enterprisesAppService = enterprisesAppService;
        }

        public async Task OnGetAsync()
        {
            Enterprise = new EnterpriseCreateDto();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _enterprisesAppService.CreateAsync(Enterprise);
            return NoContent();
        }
    }
}