

using Ekip2.Application.DTOs.AdvanceDTOs;
using Ekip2.Application.Services.AdvanceServices;
using Ekip2.Domain.Entities;
using Microsoft.Extensions.Logging;

using Ekip2.Application.DTOs.PackageLogDTOs;
using Ekip2.Infrastructure.Repositories.PackageLogRepositories;

namespace Ekip2.Application.Services.CompanyServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IAccountService _accountService;
        private readonly ILogger<CompanyService> _logger;
        private readonly IPackageLogRepository _packageLogRepository;

        public CompanyService(ICompanyRepository companyRepository, IManagerRepository managerRepository, IAccountService accountService, IPackageLogRepository packageLogRepository, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _managerRepository = managerRepository;
            _accountService = accountService;
            _packageLogRepository = packageLogRepository;
            _logger = logger;
        }

        public async Task<IDataResult<CompanyDTO>> CreateAsync(CompanyCreateDTO companyCreateDTO)
        {
            DataResult<CompanyDTO> result = new ErrorDataResult<CompanyDTO>();

            var strategy = await _companyRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _companyRepository.BeginTransection().ConfigureAwait(false);
                try
                {
                    var newCompany = companyCreateDTO.Adapt<Company>();
                    await _companyRepository.AddAsync(newCompany);
                    await _companyRepository.SaveChangesAsync();

                    // Paket log kaydını oluşturma
                    var packageLog = new PackageLog
                    {
                        Id = Guid.NewGuid(),
                        Name = "Initial Package",
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(1), 
                        IsActive = true,
                        PackageId = companyCreateDTO.PackageId, 
                        CompanyId = newCompany.Id 
                    };
                    await _packageLogRepository.AddAsync(packageLog);
                    await _packageLogRepository.SaveChangesAsync();

                    result = new SuccessDataResult<CompanyDTO>(newCompany.Adapt<CompanyDTO>(), "Şirket başarıyla eklendi");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {
                    result = new ErrorDataResult<CompanyDTO>("Şirket ekleme başarısız: " + ex.Message);
                    transactionScope.Rollback();
                }
                finally
                {
                    transactionScope.Dispose();
                }
            });

            return result;
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            Result result = new ErrorResult();

            var strategy = await _companyRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _companyRepository.BeginTransection().ConfigureAwait(false);
                try
                {
                    var deletingCompany = await _companyRepository.GetByIdAsync(id);
                    if (deletingCompany == null)
                    {
                        result = new ErrorResult("Veri bulunamadı");
                        transactionScope.Rollback();
                        return;
                    }

                    var deletingManagers = await _managerRepository.GetAllAsync(x => x.CompanyId == deletingCompany.Id);
                    if (deletingManagers != null && deletingManagers.Any())
                    {
                        foreach (var manager in deletingManagers)
                        {
                            var identityResult = await _accountService.DeleteUserAsync(manager.IdentityId);
                            if (!identityResult.Succeeded)
                            {
                                result = new ErrorResult("Silme İşlemi Başarısız: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                                transactionScope.Rollback();
                                return;
                            }

                            await _managerRepository.DeleteAsync(manager);
                            await _managerRepository.SaveChangesAsync();
                        }
                    }

                    await _companyRepository.DeleteAsync(deletingCompany);
                    await _companyRepository.SaveChangesAsync();

                    result = new SuccessResult("Silme işlemi başarılı");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {
                    result = new ErrorResult("Silme işlemi başarısız: " + ex.Message);
                    transactionScope.Rollback();
                }
                finally
                {
                    transactionScope.Dispose();
                }
            });

            return result;
        }

        public async Task<IDataResult<List<CompanyListDTO>>> GetAllAsync()
        {
            var list = await _companyRepository.GetAllAsync();
            if (list is null)
            {
                return new ErrorDataResult<List<CompanyListDTO>>("Listeleme başarısız");
            }
            return new SuccessDataResult<List<CompanyListDTO>>(list.Adapt<List<CompanyListDTO>>(), "Listeleme başarılı");
        }

        public async Task<IDataResult<CompanyDTO>> GetByIdAsync(Guid id)
        {
            var companyId = await _companyRepository.GetByIdAsync(id);
            if (companyId is not null)
            {
                return new SuccessDataResult<CompanyDTO>(companyId.Adapt<CompanyDTO>(), "Company bulundu");
            }
            return new ErrorDataResult<CompanyDTO>();
        }

        public async Task<IDataResult<CompanyDTO>> UpdateAsync(CompanyUpdateDTO companyUpdateDTO)
        {
            var updatingCompany = await _companyRepository.GetByIdAsync(companyUpdateDTO.Id);
            if (updatingCompany is null)
            {
                return new ErrorDataResult<CompanyDTO>("Güncellenecek veri bulunamadı");
            }

            updatingCompany.Address = companyUpdateDTO.Address;
            updatingCompany.PhoneNumber = companyUpdateDTO.PhoneNumber;
            updatingCompany.Name = companyUpdateDTO.Name;
            updatingCompany.PackageId = companyUpdateDTO.PackageId;
            

            // Yeni resim varsa güncelle, yoksa mevcut resmi koru
            if (companyUpdateDTO.Image != null && companyUpdateDTO.Image.Length > 0)
            {
                updatingCompany.Image = companyUpdateDTO.Image;
            }

            try
            {
                await _companyRepository.UpdateAsync(updatingCompany);
                await _companyRepository.SaveChangesAsync();
                return new SuccessDataResult<CompanyDTO>(updatingCompany.Adapt<CompanyDTO>(), "Şirket güncelleme başarılı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket güncellenirken bir hata oluştu.");
                return new ErrorDataResult<CompanyDTO>("Şirket güncelleme sırasında bir hata oluştu.");
            }
        }
    }
}
