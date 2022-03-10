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
using Tank.Financing.Enterprises;
using Tank.Financing.Shared;

namespace Tank.Financing.Web.Pages.Enterprises
{
    public class IndexModel : AbpPageModel
    {
        public string EnterpriseNameFilter { get; set; }
        public string ArtificialPersonFilter { get; set; }
        public long? EstablishedTimeFilterMin { get; set; }

        public long? EstablishedTimeFilterMax { get; set; }
        public long? DueTimeFilterMin { get; set; }

        public long? DueTimeFilterMax { get; set; }
        public string CreditCodeFilter { get; set; }
        public string ArtificialPersonIdFilter { get; set; }
        public string RegisteredCapitalFilter { get; set; }
        public string PhoneNumberFilter { get; set; }
        public string CertPhotoPathFilter { get; set; }
        public string IdPhotoPath1Filter { get; set; }
        public string IdPhotoPath2Filter { get; set; }
        public CertificateStatus? CertificateStatusFilter { get; set; }
        public string CertificateTxIdFilter { get; set; }
        public string ConfirmCertificateTxIdFilter { get; set; }

        private readonly IEnterprisesAppService _enterprisesAppService;

        public IndexModel(IEnterprisesAppService enterprisesAppService)
        {
            _enterprisesAppService = enterprisesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}