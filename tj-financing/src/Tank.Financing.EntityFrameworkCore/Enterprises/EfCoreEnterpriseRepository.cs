using Tank.Financing;
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
            long? establishedTimeMin = null,
            long? establishedTimeMax = null,
            long? dueTimeMin = null,
            long? dueTimeMax = null,
            string creditCode = null,
            string artificialPersonId = null,
            string registeredCapital = null,
            string phoneNumber = null,
            string certPhotoPath = null,
            string idPhotoPath1 = null,
            string idPhotoPath2 = null,
            CertificateStatus? certificateStatus = null,
            string certificateTxId = null,
            string confirmCertificateTxId = null,
            string commitUserName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, enterpriseName, artificialPerson, establishedTimeMin, establishedTimeMax, dueTimeMin, dueTimeMax, creditCode, artificialPersonId, registeredCapital, phoneNumber, certPhotoPath, idPhotoPath1, idPhotoPath2, certificateStatus, certificateTxId, confirmCertificateTxId, commitUserName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EnterpriseConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string enterpriseName = null,
            string artificialPerson = null,
            long? establishedTimeMin = null,
            long? establishedTimeMax = null,
            long? dueTimeMin = null,
            long? dueTimeMax = null,
            string creditCode = null,
            string artificialPersonId = null,
            string registeredCapital = null,
            string phoneNumber = null,
            string certPhotoPath = null,
            string idPhotoPath1 = null,
            string idPhotoPath2 = null,
            CertificateStatus? certificateStatus = null,
            string certificateTxId = null,
            string confirmCertificateTxId = null,
            string commitUserName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, enterpriseName, artificialPerson, establishedTimeMin, establishedTimeMax, dueTimeMin, dueTimeMax, creditCode, artificialPersonId, registeredCapital, phoneNumber, certPhotoPath, idPhotoPath1, idPhotoPath2, certificateStatus, certificateTxId, confirmCertificateTxId, commitUserName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Enterprise> ApplyFilter(
            IQueryable<Enterprise> query,
            string filterText,
            string enterpriseName = null,
            string artificialPerson = null,
            long? establishedTimeMin = null,
            long? establishedTimeMax = null,
            long? dueTimeMin = null,
            long? dueTimeMax = null,
            string creditCode = null,
            string artificialPersonId = null,
            string registeredCapital = null,
            string phoneNumber = null,
            string certPhotoPath = null,
            string idPhotoPath1 = null,
            string idPhotoPath2 = null,
            CertificateStatus? certificateStatus = null,
            string certificateTxId = null,
            string confirmCertificateTxId = null,
            string commitUserName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EnterpriseName.Contains(filterText) || e.ArtificialPerson.Contains(filterText) || e.CreditCode.Contains(filterText) || e.ArtificialPersonId.Contains(filterText) || e.RegisteredCapital.Contains(filterText) || e.PhoneNumber.Contains(filterText) || e.CertPhotoPath.Contains(filterText) || e.IdPhotoPath1.Contains(filterText) || e.IdPhotoPath2.Contains(filterText) || e.CertificateTxId.Contains(filterText) || e.ConfirmCertificateTxId.Contains(filterText) || e.CommitUserName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(enterpriseName), e => e.EnterpriseName.Contains(enterpriseName))
                    .WhereIf(!string.IsNullOrWhiteSpace(artificialPerson), e => e.ArtificialPerson.Contains(artificialPerson))
                    .WhereIf(establishedTimeMin.HasValue, e => e.EstablishedTime >= establishedTimeMin.Value)
                    .WhereIf(establishedTimeMax.HasValue, e => e.EstablishedTime <= establishedTimeMax.Value)
                    .WhereIf(dueTimeMin.HasValue, e => e.DueTime >= dueTimeMin.Value)
                    .WhereIf(dueTimeMax.HasValue, e => e.DueTime <= dueTimeMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(creditCode), e => e.CreditCode.Contains(creditCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(artificialPersonId), e => e.ArtificialPersonId.Contains(artificialPersonId))
                    .WhereIf(!string.IsNullOrWhiteSpace(registeredCapital), e => e.RegisteredCapital.Contains(registeredCapital))
                    .WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), e => e.PhoneNumber.Contains(phoneNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(certPhotoPath), e => e.CertPhotoPath.Contains(certPhotoPath))
                    .WhereIf(!string.IsNullOrWhiteSpace(idPhotoPath1), e => e.IdPhotoPath1.Contains(idPhotoPath1))
                    .WhereIf(!string.IsNullOrWhiteSpace(idPhotoPath2), e => e.IdPhotoPath2.Contains(idPhotoPath2))
                    .WhereIf(certificateStatus.HasValue, e => e.CertificateStatus == certificateStatus)
                    .WhereIf(!string.IsNullOrWhiteSpace(certificateTxId), e => e.CertificateTxId.Contains(certificateTxId))
                    .WhereIf(!string.IsNullOrWhiteSpace(confirmCertificateTxId), e => e.ConfirmCertificateTxId.Contains(confirmCertificateTxId))
                    .WhereIf(!string.IsNullOrWhiteSpace(commitUserName), e => e.CommitUserName.Contains(commitUserName));
        }
    }
}