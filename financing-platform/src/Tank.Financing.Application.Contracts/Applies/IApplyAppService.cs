using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tank.Financing.Applies
{
    public interface IAppliesAppService : IApplicationService
    {
        Task<PagedResultDto<ApplyDto>> GetListAsync(GetAppliesInput input);

        Task<ApplyDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ApplyDto> CreateAsync(ApplyCreateDto input);

        Task<ApplyDto> UpdateAsync(Guid id, ApplyUpdateDto input);
    }
}