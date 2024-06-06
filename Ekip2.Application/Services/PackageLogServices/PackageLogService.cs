using Ekip2.Application.DTOs.PackageDTOs;
using Ekip2.Application.DTOs.PackageLogDTOs;
using Ekip2.Infrastructure.Repositories.PackageLogRepositories;
using Ekip2.Infrastructure.Repositories.PackageRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.PackageLogServices
{
    public class PackageLogService : IPackageLogService
    {
        private readonly IPackageLogRepository _packageLogRepository;
        private readonly ICompanyRepository _companyRepository;

        public PackageLogService(IPackageLogRepository packageLogRepository, ICompanyRepository companyRepository)
        {
            _packageLogRepository = packageLogRepository;
            _companyRepository = companyRepository;
        }


        public async Task<IResult> AssignSubscription(PackageSelectDTO packageSelectDTO)
        {

            var company = await _companyRepository.GetByIdAsync(packageSelectDTO.CompanyId);
            if (company == null)
            {
                return new ErrorResult("Company not found");
            }

            company.PackageId = packageSelectDTO.PackageId;

            var packageLog = new PackageLog
            {
                PackageId = packageSelectDTO.PackageId,
                CompanyId = packageSelectDTO.CompanyId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMinutes(1),
                IsActive = true,
            };

            await _packageLogRepository.AddAsync(packageLog);
            await _packageLogRepository.SaveChangesAsync();

            return new SuccessResult("Subscription assigned successfully");

        }

        public async Task<IDataResult<List<PackageLogListDTO>>> GetAllAsync()
        {
            var list = await _packageLogRepository.GetAllAsync();
            if (list is null)
            {
                return new ErrorDataResult<List<PackageLogListDTO>>("Listeleme Başarısız.");
            }
            return new SuccessDataResult<List<PackageLogListDTO>>(list.Adapt<List<PackageLogListDTO>>(), "Listeleme Başarılı");
        }
    }
}
