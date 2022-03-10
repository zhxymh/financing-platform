using Tank.Financing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Tank.Financing.Applies;
using Tank.Financing.Shared;

namespace Tank.Financing.Web.Pages.Applies
{
    public class IndexModel : AbpPageModel
    {
        public string EnterpriseNameFilter { get; set; }
        public string OrganizationFilter { get; set; }
        public string ProductNameFilter { get; set; }
        public string AllowanceFilter { get; set; }
        public string APRFilter { get; set; }
        public string PeriodFilter { get; set; }
        public ApplyStatus? ApplyStatusFilter { get; set; }
        public GuaranteeMethod? GuaranteeMethodFilter { get; set; }
        public long? ApplyTimeFilterMin { get; set; }

        public long? ApplyTimeFilterMax { get; set; }
        public long? PassedTimeFilterMin { get; set; }

        public long? PassedTimeFilterMax { get; set; }
        public string ApplyTxIdFilter { get; set; }
        public string OnlineApproveTxIdFilter { get; set; }
        public string OfflineApproveTxIdFilter { get; set; }
        public string ApproveAllowanceTxIdFilter { get; set; }
        public string SetAllowanceTxIdFilter { get; set; }

        private readonly IAppliesAppService _appliesAppService;

        public IndexModel(IAppliesAppService appliesAppService)
        {
            _appliesAppService = appliesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}