using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tank.Financing.Enterprises
{
    public interface IEnterpriseRepository : IRepository<Enterprise, Guid>
    {
        Task<List<Enterprise>> GetListAsync(
            string filterText = null,
            string enterpriseName = null,
            string artificialPerson = null,
            string establishedTime = null,
            string dueTime = null,
            string creditCode = null,
            string artificialPersonId = null,
            string registeredCapital = null,
            string phoneNumber = null,
            string certPhotoPath = null,
            string idPhotoPath1 = null,
            string idPhotoPath2 = null,
            string certificateStatus = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string enterpriseName = null,
            string artificialPerson = null,
            string establishedTime = null,
            string dueTime = null,
            string creditCode = null,
            string artificialPersonId = null,
            string registeredCapital = null,
            string phoneNumber = null,
            string certPhotoPath = null,
            string idPhotoPath1 = null,
            string idPhotoPath2 = null,
            string certificateStatus = null,
            CancellationToken cancellationToken = default);
    }
}