using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Tank.Financing.Enterprises;

namespace Tank.Financing.Web.Pages.Enterprises
{
    public class EditModalModel : FinancingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public EnterpriseUpdateDto Enterprise { get; set; }

        private readonly IEnterprisesAppService _enterprisesAppService;

        public EditModalModel(IEnterprisesAppService enterprisesAppService)
        {
            _enterprisesAppService = enterprisesAppService;
        }

        public async Task OnGetAsync()
        {
            var enterprise = await _enterprisesAppService.GetAsync(Id);
            Enterprise = ObjectMapper.Map<EnterpriseDto, EnterpriseUpdateDto>(enterprise);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _enterprisesAppService.UpdateAsync(Id, Enterprise);
            return NoContent();
        }
    }
}