using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tank.Financing.Enterprises
{
    public interface IEnterprisesAppService : IApplicationService
    {
        Task<PagedResultDto<EnterpriseDto>> GetListAsync(GetEnterprisesInput input);

        Task<EnterpriseDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<EnterpriseDto> CreateAsync(EnterpriseCreateDto input);

        Task<EnterpriseDto> UpdateAsync(Guid id, EnterpriseUpdateDto input);
    }
}