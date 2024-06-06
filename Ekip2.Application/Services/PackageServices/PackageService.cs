using Ekip2.Application.DTOs.PackageDTOs;
using Ekip2.Infrastructure.Repositories.PackageRepositories;
using Microsoft.EntityFrameworkCore;


namespace Ekip2.Application.Services.PackageServices
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ICompanyRepository _companyRepository;
        public PackageService(IPackageRepository packageRepository, ICompanyRepository companyRepository)
        {
            _packageRepository = packageRepository;
            _companyRepository = companyRepository;
        }
        public async Task<IDataResult<PackageDTO>> CreateAsync(PackageCreateDTO packageCreateDTO)
        {
            var newPackage = packageCreateDTO.Adapt<Package>();
            await _packageRepository.AddAsync(newPackage);
            await _packageRepository.SaveChangesAsync();
            return new SuccessDataResult<PackageDTO>(newPackage.Adapt<PackageDTO>(), "Başarıyla eklendi");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingId = await _packageRepository.GetByIdAsync(id);
            if (deletingId != null)
            {
                await _packageRepository.DeleteAsync(deletingId);
                await _packageRepository.SaveChangesAsync();
                return new SuccessDataResult<PackageDTO>("Silme işlemi başarılı");
            }
            return new ErrorDataResult<PackageDTO>("Veri bulunamadı");
        }

        public async Task<IDataResult<List<PackageListDTO>>> GetAllAsync()
        {
            var list = await _packageRepository.GetAllAsync();
            if (list is null)
            {
                return new ErrorDataResult<List<PackageListDTO>>("Listeleme Başarısız.");
            }
            return new SuccessDataResult<List<PackageListDTO>>(list.Adapt<List<PackageListDTO>>(), "Listeleme Başarılı");
        }

        public async Task<IDataResult<PackageDTO>> GetByIdAsync(Guid id)
        {
            var packageId = await _packageRepository.GetByIdAsync(id);
            if (packageId != null)
                return new SuccessDataResult<PackageDTO>(packageId.Adapt<PackageDTO>(), "Veri başarıyla bulundu");

            return new ErrorDataResult<PackageDTO>();
        }

        public async Task<IDataResult<PackageUpdateDTO>> UpdateAsync(PackageUpdateDTO packageUpdateDTO)
        {
            var updatingPackage = await _packageRepository.GetByIdAsync(packageUpdateDTO.Id);
            if (updatingPackage != null)
            {
                var updatedPackage = packageUpdateDTO.Adapt(updatingPackage);
                await _packageRepository.UpdateAsync(updatedPackage);
                await _packageRepository.SaveChangesAsync();
                return new SuccessDataResult<PackageUpdateDTO>(updatedPackage.Adapt<PackageUpdateDTO>(), "Güncelleme başarılı");
            }
            return new ErrorDataResult<PackageUpdateDTO>("Veri bulunamadı");
        }
        
    }

}

