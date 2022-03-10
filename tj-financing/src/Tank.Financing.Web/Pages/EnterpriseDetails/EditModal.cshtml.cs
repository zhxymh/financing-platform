using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Tank.Financing.EnterpriseDetails;

namespace Tank.Financing.Web.Pages.EnterpriseDetails
{
    public class EditModalModel : FinancingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public EnterpriseDetailUpdateDto EnterpriseDetail { get; set; }

        private readonly IEnterpriseDetailsAppService _enterpriseDetailsAppService;

        public EditModalModel(IEnterpriseDetailsAppService enterpriseDetailsAppService)
        {
            _enterpriseDetailsAppService = enterpriseDetailsAppService;
        }

        public async Task OnGetAsync()
        {
            var enterpriseDetail = await _enterpriseDetailsAppService.GetAsync(Id);
            EnterpriseDetail = ObjectMapper.Map<EnterpriseDetailDto, EnterpriseDetailUpdateDto>(enterpriseDetail);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _enterpriseDetailsAppService.UpdateAsync(Id, EnterpriseDetail);
            return NoContent();
        }
    }
}