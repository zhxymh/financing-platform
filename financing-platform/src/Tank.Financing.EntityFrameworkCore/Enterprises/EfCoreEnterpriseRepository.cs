using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Tank.Financing.EntityFrameworkCore;

namespace Tank.Financing.Enterprises
{
    public class EfCoreEnterpriseRepository : EfCoreRepository<FinancingDbContext, Enterprise, Guid>, IEnterpriseRepository
    {
        public EfCoreEnterpriseRepository(IDbContextProvider<FinancingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Enterprise>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, enterpriseName, artificialPerson, establishedTime, dueTime, creditCode, artificialPersonId, registeredCapital, phoneNumber, certPhotoPath, idPhotoPath1, idPhotoPath2, certificateStatus);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EnterpriseConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, enterpriseName, artificialPerson, establishedTime, dueTime, creditCode, artificialPersonId, registeredCapital, phoneNumber, certPhotoPath, idPhotoPath1, idPhotoPath2, certificateStatus);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Enterprise> ApplyFilter(
            IQueryable<Enterprise> query,
            string filterText,
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
            string certificateStatus = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EnterpriseName.Contains(filterText) || e.ArtificialPerson.Contains(filterText) || e.EstablishedTime.Contains(filterText) || e.DueTime.Contains(filterText) || e.CreditCode.Contains(filterText) || e.ArtificialPersonId.Contains(filterText) || e.RegisteredCapital.Contains(filterText) || e.PhoneNumber.Contains(filterText) || e.CertPhotoPath.Contains(filterText) || e.IdPhotoPath1.Contains(filterText) || e.IdPhotoPath2.Contains(filterText) || e.CertificateStatus.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(enterpriseName), e => e.EnterpriseName.Contains(enterpriseName))
                    .WhereIf(!string.IsNullOrWhiteSpace(artificialPerson), e => e.ArtificialPerson.Contains(artificialPerson))
                    .WhereIf(!string.IsNullOrWhiteSpace(establishedTime), e => e.EstablishedTime.Contains(establishedTime))
                    .WhereIf(!string.IsNullOrWhiteSpace(dueTime), e => e.DueTime.Contains(dueTime))
                    .WhereIf(!string.IsNullOrWhiteSpace(creditCode), e => e.CreditCode.Contains(creditCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(artificialPersonId), e => e.ArtificialPersonId.Contains(artificialPersonId))
                    .WhereIf(!string.IsNullOrWhiteSpace(registeredCapital), e => e.RegisteredCapital.Contains(registeredCapital))
                    .WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), e => e.PhoneNumber.Contains(phoneNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(certPhotoPath), e => e.CertPhotoPath.Contains(certPhotoPath))
                    .WhereIf(!string.IsNullOrWhiteSpace(idPhotoPath1), e => e.IdPhotoPath1.Contains(idPhotoPath1))
                    .WhereIf(!string.IsNullOrWhiteSpace(idPhotoPath2), e => e.IdPhotoPath2.Contains(idPhotoPath2))
                    .WhereIf(!string.IsNullOrWhiteSpace(certificateStatus), e => e.CertificateStatus.Contains(certificateStatus));
        }
    }
}