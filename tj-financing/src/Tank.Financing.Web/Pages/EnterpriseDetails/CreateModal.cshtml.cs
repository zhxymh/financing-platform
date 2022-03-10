using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tank.Financing.EnterpriseDetails;

namespace Tank.Financing.Web.Pages.EnterpriseDetails
{
    public class CreateModalModel : FinancingPageModel
    {
        [BindProperty]
        public EnterpriseDetailCreateDto EnterpriseDetail { get; set; }

        private readonly IEnterpriseDetailsAppService _enterpriseDetailsAppService;

        public CreateModalModel(IEnterpriseDetailsAppService enterpriseDetailsAppService)
        {
            _enterpriseDetailsAppService = enterpriseDetailsAppService;
        }

        public async Task OnGetAsync()
        {
            EnterpriseDetail = new EnterpriseDetailCreateDto();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _enterpriseDetailsAppService.CreateAsync(EnterpriseDetail);
            return NoContent();
        }
    }
}