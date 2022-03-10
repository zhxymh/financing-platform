using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Tank.Financing.EnterpriseDetails;
using Tank.Financing.Shared;

namespace Tank.Financing.Web.Pages.EnterpriseDetails
{
    public class IndexModel : AbpPageModel
    {
        public string EnterpriseNameFilter { get; set; }
        public string TotalAssetsFilter { get; set; }
        public string IncomeFilter { get; set; }
        public string EnterpriseTypeFilter { get; set; }
        public int? StaffNumberFilterMin { get; set; }

        public int? StaffNumberFilterMax { get; set; }
        public string IndustryFilter { get; set; }
        public string LocationFilter { get; set; }
        public string RegisteredAddressFilter { get; set; }
        public string BusinessAddressFilter { get; set; }
        public string BusinessScopeFilter { get; set; }
        public string DescriptionFilter { get; set; }
        public string CompleteTxIdFilter { get; set; }

        private readonly IEnterpriseDetailsAppService _enterpriseDetailsAppService;

        public IndexModel(IEnterpriseDetailsAppService enterpriseDetailsAppService)
        {
            _enterpriseDetailsAppService = enterpriseDetailsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}