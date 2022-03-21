using Volo.Abp.Application.Dtos;
using System;

namespace Tank.Financing.EnterpriseDetails
{
    public class GetEnterpriseDetailsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string EnterpriseName { get; set; }
        public string TotalAssets { get; set; }
        public string Income { get; set; }
        public string EnterpriseType { get; set; }
        public int? StaffNumberMin { get; set; }
        public int? StaffNumberMax { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string RegisteredAddress { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessScope { get; set; }
        public string Description { get; set; }
        public string CompleteTxId { get; set; }
        public string CommitUserName { get; set; }

        public GetEnterpriseDetailsInput()
        {

        }
    }
}