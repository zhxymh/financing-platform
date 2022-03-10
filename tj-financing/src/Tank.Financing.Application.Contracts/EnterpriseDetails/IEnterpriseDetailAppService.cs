using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tank.Financing.EnterpriseDetails
{
    public interface IEnterpriseDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<EnterpriseDetailDto>> GetListAsync(GetEnterpriseDetailsInput input);

        Task<EnterpriseDetailDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<EnterpriseDetailDto> CreateAsync(EnterpriseDetailCreateDto input);

        Task<EnterpriseDetailDto> UpdateAsync(Guid id, EnterpriseDetailUpdateDto input);
    }
}