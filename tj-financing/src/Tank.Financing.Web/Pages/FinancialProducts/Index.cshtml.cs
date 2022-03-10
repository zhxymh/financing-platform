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
using Tank.Financing.FinancialProducts;
using Tank.Financing.Shared;

namespace Tank.Financing.Web.Pages.FinancialProducts
{
    public class IndexModel : AbpPageModel
    {
        public string ProductNameFilter { get; set; }
        public string OrganizationFilter { get; set; }
        public int? PeriodFilterMin { get; set; }

        public int? PeriodFilterMax { get; set; }
        public GuaranteeMethod? GuaranteeMethodFilter { get; set; }
        public int? AppliedNumberFilterMin { get; set; }

        public int? AppliedNumberFilterMax { get; set; }
        public string APRFilter { get; set; }
        public string RatingFilter { get; set; }
        public long? CreditCeilingFilterMin { get; set; }

        public long? CreditCeilingFilterMax { get; set; }
        public string AddFinancingProductTxIdFilter { get; set; }

        private readonly IFinancialProductsAppService _financialProductsAppService;

        public IndexModel(IFinancialProductsAppService financialProductsAppService)
        {
            _financialProductsAppService = financialProductsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}