


namespace Ekip2.Application.Services.AdminServices;

public class AdminService : IAdminService
{
    private readonly IAccountService _accountService;
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAccountService accountService, IAdminRepository adminRepository)
    {
        _accountService = accountService;
        _adminRepository = adminRepository;
    }
    public async Task<IResult> ChangePasswordAsync(AdminChangePasswordDTO adminChangePasswordDTO)
    {
        var admin = await _adminRepository.GetByIdAsync(adminChangePasswordDTO.Id);
        if (admin == null)
        {
            return new ErrorResult("Admin bulunamadı!");
        }

        var user = await _accountService.FindByIdAsync(admin.IdentityId);
        if (user == null)
        {
            return new ErrorResult("Kullanıcı bulunamadı!");
        }

        var result = await _accountService.ChangePasswordAsyncc(user, adminChangePasswordDTO.OldPassword, adminChangePasswordDTO.NewPassword);
        if (!result.Succeeded)
        {
            return new ErrorResult("Şifre değiştirme başarısız: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return new SuccessResult("Şifre başarıyla değiştirildi.");
    }


    public async Task<IDataResult<AdminDTO>> CreateAsync(AdminCreateDTO adminCreateDTO)
    {
        if (await _accountService.AnyAsync(x => x.Email == adminCreateDTO.Email))
        {
            return new ErrorDataResult<AdminDTO>("Email adresi kullanılmaktadır.");
        }

        IdentityUser identityUser = new()
        {
            Email = adminCreateDTO.Email,
            NormalizedEmail = adminCreateDTO.Email.ToLowerInvariant(),
            UserName = adminCreateDTO.Email,
            NormalizedUserName = adminCreateDTO.Email.ToUpperInvariant(),
            EmailConfirmed = true
        };

        DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>();

        var strategy = await _adminRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepository.BeginTransection().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.CreateUserAsync(identityUser, Roles.Admin);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AdminDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }

                var admin = adminCreateDTO.Adapt<Admin>();
                admin.IdentityId = identityUser.Id;
                await _adminRepository.AddAsync(admin);
                await _adminRepository.SaveChangesAsync();

                result = new SuccessDataResult<AdminDTO>("Kullanıcı Başarıyla Eklendi.");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AdminDTO>("Kullanıcı Ekleme Başarısız" + ex.Message);
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

        var strategy = await _adminRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepository.BeginTransection().ConfigureAwait(false);
            try
            {
                var deletingUser = await _adminRepository.GetByIdAsync(id);
                if (deletingUser == null)
                {
                    result = new ErrorResult("Kullanıcı Mevcut Değil");
                    transactionScope.Rollback();
                    return;
                }

                var identityResult = await _accountService.DeleteUserAsync(deletingUser.IdentityId);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorResult("Silme İşlemi Başarısız: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                    transactionScope.Rollback();
                    return;
                }

                await _adminRepository.DeleteAsync(deletingUser);
                await _adminRepository.SaveChangesAsync();
                result = new SuccessResult("Silme İşlemi Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorResult("Silme İşlemi Başarısız: " + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;


    }

    public async Task<IDataResult<List<AdminListDTO>>> GetAllAsync()
    {
        var admins = await _adminRepository.GetAllAsync();
        var adminDTOs = admins.Adapt<List<AdminListDTO>>();
        if (admins.Count() <= 0)
        {
            return new ErrorDataResult<List<AdminListDTO>>(adminDTOs, "Görüntülenecek Kullanıcı Bulunamadı");
        }
        return new SuccessDataResult<List<AdminListDTO>>(adminDTOs, "Kullanıcılar Listelendi");
    }

    public async Task<IDataResult<AdminDTO>> GetByIdAsync(Guid id)
    {
        try
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            if (admin == null)
            {
                return new ErrorDataResult<AdminDTO>("Kullanıcı Bulunamadı");
            }

            var adminDTO = admin.Adapt<AdminDTO>();

            return new SuccessDataResult<AdminDTO>(adminDTO, "Kullanıcı Başarıyla Getirildi");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<AdminDTO>("Kullanıcı Getirme İşlemi Başarısız: " + ex.Message);
        }
    }

    public async Task<IDataResult<AdminDTO>> GetByIdentityUserIdAsync(string identityUserId)
    {
        var admin = await _adminRepository.GetByIdentityId(identityUserId);
        if (admin == null)
        {
            return new ErrorDataResult<AdminDTO>("Admin bulunamadı!");
        }
        var adminDTO = admin.Adapt<AdminDTO>();
        return new SuccessDataResult<AdminDTO>(adminDTO, "Admin başarıyla bulundu!");

    }

    public async Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO)
    {
        DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>();
        var strategy = await _adminRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepository.BeginTransection().ConfigureAwait(false);
            try
            {

                var updatingUser = await _adminRepository.GetByIdAsync(adminUpdateDTO.Id, false);
                if (updatingUser == null)
                {
                    result = new ErrorDataResult<AdminDTO>("Kullanıcı Bulunamadı");
                    transactionScope.Rollback();
                    return;
                }


                var identityUser = await _accountService.FindByIdAsync(updatingUser.IdentityId);
                if (identityUser == null)
                {
                    result = new ErrorDataResult<AdminDTO>("Güncellenecek Kullanıcıya Ait Kayıt Bulunamadı");
                    transactionScope.Rollback();
                    return;
                }


                updatingUser = adminUpdateDTO.Adapt(updatingUser);
                await _adminRepository.UpdateAsync(updatingUser);


                identityUser.Email = adminUpdateDTO.Email;
                identityUser.UserName = adminUpdateDTO.Email;
                identityUser.NormalizedEmail = adminUpdateDTO.Email.ToUpperInvariant();
                identityUser.NormalizedUserName = adminUpdateDTO.Email.ToUpperInvariant();
                var identityResult = await _accountService.UpdateUserAsync(identityUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AdminDTO>("Bilgileri Güncelleme Başarısız" + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                    transactionScope.Rollback();
                    return;
                }

                await _adminRepository.SaveChangesAsync();
                result = new SuccessDataResult<AdminDTO>(updatingUser.Adapt<AdminDTO>(), "Kullanıcı Güncelleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AdminDTO>("Güncelleme Başarısız: " + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;

    }


}
