using System;
using Volo.Abp.Application.Dtos;

namespace Tank.Financing.EnterpriseDetails
{
    public class EnterpriseDetailDto : FullAuditedEntityDto<Guid>
    {
        public string EnterpriseName { get; set; }
        public string TotalAssets { get; set; }
        public string Income { get; set; }
        public string EnterpriseType { get; set; }
        public int StaffNumber { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string RegisteredAddress { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessScope { get; set; }
        public string Description { get; set; }
        public string CompleteTxId { get; set; }
        public string CommitUserName { get; set; }
    }
}