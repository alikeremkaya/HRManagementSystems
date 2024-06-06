using Ekip2.Application.DTOs.PackageDTOs;
using Ekip2.Application.Services.ExpenseServices;
using Ekip2.Infrastructure.Repositories.AdvanceRepositories;
using Ekip2.Infrastructure.Repositories.ExpenseRepositories;
using Ekip2.Infrastructure.Repositories.LeaveRepositories;

namespace Ekip2.Application.Services.ManagerServices
{
    public class ManagerService : IManagerService
    {
        private readonly IAccountService _accountService;
        private readonly IManagerRepository _managerRepository;
        private readonly IMailService _mailService;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IAdvanceRepository _advanceRepository;
        private readonly IExpenseRepository _expenseRepository;

        public ManagerService(IAccountService accountService, IManagerRepository managerRepository, IMailService mailService, IPasswordHasher<IdentityUser> passwordHasher, ILeaveRepository leaveRepository, IAdvanceRepository advanceRepository, IExpenseRepository expenseRepository)
        {
            _accountService = accountService;
            _managerRepository = managerRepository;
            _mailService = mailService;
            _passwordHasher = passwordHasher;
            _leaveRepository = leaveRepository;
            _advanceRepository = advanceRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<IResult> ChangePasswordAsync(ManagerChangePasswordDTO managerChangePasswordDTO)
        {
            var manager = await _managerRepository.GetByIdAsync(managerChangePasswordDTO.Id);
            if (manager == null)
            {
                return new ErrorResult("Manager bulunamadı!");
            }

            var user = await _accountService.FindByIdAsync(manager.IdentityId);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı!");
            }

            var result = await _accountService.ChangePasswordAsyncc(user, managerChangePasswordDTO.OldPassword, managerChangePasswordDTO.NewPassword);
            if (!result.Succeeded)
            {
                return new ErrorResult("Şifre değiştirme başarısız: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return new SuccessResult("Şifre başarıyla değiştirildi.");
        }
        public async Task<List<Manager>> GetManagersByCompanyIdAsync(Guid companyId)
        {
            return (await _managerRepository.GetAllAsync(m => m.CompanyId == companyId)).ToList();
        }
        public async Task<IDataResult<ManagerDTO>> CreateAsync(ManagerCreateDTO managerCreateDTO)
        {

            if (await _accountService.AnyAsync(x => x.Email == managerCreateDTO.Email))
            {
                return new ErrorDataResult<ManagerDTO>("Mail Adresi Kullanılmaktadır.");
            }

            IdentityUser identityUser = new IdentityUser
            {
                Email = managerCreateDTO.Email,
                NormalizedEmail = managerCreateDTO.Email.ToUpperInvariant(),
                UserName = managerCreateDTO.Email,
                NormalizedUserName = managerCreateDTO.Email.ToUpperInvariant(),

                EmailConfirmed = true
            };

            DataResult<ManagerDTO> result = new ErrorDataResult<ManagerDTO>();
            var strategy = await _managerRepository.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _managerRepository.BeginTransection().ConfigureAwait(false);
                try
                {

                    var identityResult = await _accountService.CreateUserAsync(identityUser, Domain.Enums.Roles.Manager);
                    if (!identityResult.Succeeded)
                    {
                        var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                        result = new ErrorDataResult<ManagerDTO>(errors);
                        transactionScope.Rollback();
                        return;
                    }

                    var manager = managerCreateDTO.Adapt<Manager>();
                    manager.IdentityId = identityUser.Id;

                    var randomPassword = PasswordHelper.GenerateRandomPassword();
                    var htmlMessage = $@"
                        <table width='100%' style='font-family: Arial, sans-serif;'>
                            <tr>
                                <td align='center'>
                                    <img src='~/images/HR-Photo.jpeg' alt='Your Company Logo' style='max-width: 200px; margin-bottom: 15px;' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h1 style='color: #8E24AA;'>HR Management System</h1>
                                    <p>Merhaba {manager.FirstName} {manager.LastName}</p>
                                    <p><p><a href='https://www.hrmanagementsystem.com.tr' style='color: #8E24AA;'>www.hrmanagementsystem.com.tr</a></p>
                                    </div> adresinden mail adresinizle aşağıda gönderilen şifre ile giriş sağlayabilirsiniz:</p>
                                    <p><strong>Şifre:</strong> {randomPassword}</p>
                                    <p>Teşekkürler,</p>
                                    <p>SEAO Ekibi</p>
                                </td>
                            </tr>
                        </table>";



                    // E-posta ile şifre gönderme işlemi
                    var mailDTO = new MailDTO
                    {
                        Email = identityUser.Email,
                        Subject = "Welcome to YourApp",
                        Message = htmlMessage,


                    };
                    await _mailService.SendMailAsync(mailDTO);

                    if (manager.Password is null)
                    {
                        // Şifreyi hashle
                        var hashedPassword = _passwordHasher.HashPassword(identityUser, randomPassword);
                        identityUser.PasswordHash = hashedPassword;

                        // Şifreyi hashlenmiş şekilde sakla
                        manager.Password = hashedPassword;
                    }
                    await _managerRepository.AddAsync(manager);
                    await _managerRepository.SaveChangesAsync();
                    result = new SuccessDataResult<ManagerDTO>("Kullanıcı Ekleme Başarılı");
                    transactionScope.Commit();


                }
                catch (Exception ex)
                {
                    result = new ErrorDataResult<ManagerDTO>("Ekleme Başarısız: " + ex.Message);
                    transactionScope.Rollback();
                    throw;
                }
                finally
                {
                    transactionScope.Dispose();
                }
            });

            return result;
        }

        public async Task<IDataResult<ManagerDTO>> CreateEmployeeAsync(ManagerCreateDTO managerCreateDTO)
        {
            if (await _accountService.AnyAsync(x => x.Email == managerCreateDTO.Email))
            {
                return new ErrorDataResult<ManagerDTO>("Mail Adresi Kullanılmaktadır.");
            }

            IdentityUser identityUser = new IdentityUser
            {
                Email = managerCreateDTO.Email,
                NormalizedEmail = managerCreateDTO.Email.ToUpperInvariant(),
                UserName = managerCreateDTO.Email,
                NormalizedUserName = managerCreateDTO.Email.ToUpperInvariant(),

                EmailConfirmed = true
            };

            DataResult<ManagerDTO> result = new ErrorDataResult<ManagerDTO>();
            var strategy = await _managerRepository.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _managerRepository.BeginTransection().ConfigureAwait(false);
                try
                {

                    var identityResult = await _accountService.CreateUserAsync(identityUser, Domain.Enums.Roles.Employee);
                    if (!identityResult.Succeeded)
                    {
                        var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                        result = new ErrorDataResult<ManagerDTO>(errors);
                        transactionScope.Rollback();
                        return;
                    }

                    var manager = managerCreateDTO.Adapt<Manager>();
                    manager.IdentityId = identityUser.Id;

                    var randomPassword = PasswordHelper.GenerateRandomPassword();
                    var htmlMessage = $@"
                        <table width='100%' style='font-family: Arial, sans-serif;'>
                            <tr>
                                <td align='center'>
                                    <img src='~/images/HR-Photo.jpeg' alt='Your Company Logo' style='max-width: 200px; margin-bottom: 15px;' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h1 style='color: #8E24AA;'>HR Management System</h1>
                                    <p>Merhaba {manager.FirstName} {manager.LastName}</p>
                                    <p><p><a href='https://www.hrmanagementsystem.com.tr' style='color: #8E24AA;'>www.hrmanagementsystem.com.tr</a></p>
                                    </div> adresinden mail adresinizle aşağıda gönderilen şifre ile giriş sağlayabilirsiniz:</p>
                                    <p><strong>Şifre:</strong> {randomPassword}</p>
                                    <p>Teşekkürler,</p>
                                    <p>SEAO Ekibi</p>
                                </td>
                            </tr>
                        </table>";



                    // E-posta ile şifre gönderme işlemi
                    var mailDTO = new MailDTO
                    {
                        Email = identityUser.Email,
                        Subject = "Welcome to YourApp",
                        Message = htmlMessage,


                    };
                    await _mailService.SendMailAsync(mailDTO);

                    if (manager.Password is null)
                    {
                        // Şifreyi hashle
                        var hashedPassword = _passwordHasher.HashPassword(identityUser, randomPassword);
                        identityUser.PasswordHash = hashedPassword;

                        // Şifreyi hashlenmiş şekilde sakla
                        manager.Password = hashedPassword;
                    }
                    await _managerRepository.AddAsync(manager);
                    await _managerRepository.SaveChangesAsync();
                    result = new SuccessDataResult<ManagerDTO>("Kullanıcı Ekleme Başarılı");
                    transactionScope.Commit();


                }
                catch (Exception ex)
                {
                    result = new ErrorDataResult<ManagerDTO>("Ekleme Başarısız: " + ex.Message);
                    transactionScope.Rollback();
                    throw;
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
            var strategy = await _managerRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _managerRepository.BeginTransection().ConfigureAwait(false);
                try
                {
                    var deletingUser = await _managerRepository.GetByIdAsync(id);

                    if (deletingUser == null)
                    {
                        result = new ErrorResult("Silinecek Kullanıcı Bulunamadı");
                        transactionScope.Rollback();
                        return;
                    }

                    // Silinecek kullanıcının rolünü kontrol et
                    if (deletingUser.Roles == Roles.Employee)
                    {
                        // Sadece ilgili Employee kaydını sil
                        await _managerRepository.DeleteAsync(deletingUser);

                        // IdentityUser'ı sil
                        var identityResult = await _accountService.DeleteUserAsync(deletingUser.IdentityId);
                        if (!identityResult.Succeeded)
                        {
                            result = new ErrorResult("Silme İşlemi Başarısız: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                            transactionScope.Rollback();
                            return;
                        }

                        await _managerRepository.SaveChangesAsync();
                        result = new SuccessResult("Silme İşlemi Başarılı");
                    }
                    else if (deletingUser.Roles == Roles.Manager)
                    {
                        // Manager için mevcut silme işlemleri
                        await _managerRepository.DeleteAsync(deletingUser);

                        var identityResult = await _accountService.DeleteUserAsync(deletingUser.IdentityId);
                        if (!identityResult.Succeeded)
                        {
                            result = new ErrorResult("Silme İşlemi Başarısız: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                            transactionScope.Rollback();
                            return;
                        }

                        // Silme işlemlerine ek olarak Manager ile ilişkili verileri de sil
                        var employees = await _managerRepository.GetAllAsync(x => x.CompanyId == deletingUser.CompanyId && x.Roles == Roles.Employee);
                        if (employees != null)
                        {
                            foreach (var employee in employees)
                            {
                                await _managerRepository.DeleteAsync(employee);
                                await _managerRepository.SaveChangesAsync();
                            }
                        }

                        var expenses = await _expenseRepository.GetAllAsync(x => x.ManagerId == deletingUser.Id);
                        if (expenses != null)
                        {
                            foreach (var expense in expenses)
                            {
                                await _expenseRepository.DeleteAsync(expense);
                                await _expenseRepository.SaveChangesAsync();
                            }
                        }

                        var leaves = await _leaveRepository.GetAllAsync(x => x.ManagerId == deletingUser.Id);
                        if (leaves != null)
                        {
                            foreach (var leave in leaves)
                            {
                                await _leaveRepository.DeleteAsync(leave);
                                await _leaveRepository.SaveChangesAsync();
                            }
                        }

                        var advances = await _advanceRepository.GetAllAsync(x => x.ManagerId == deletingUser.Id);
                        if (advances != null)
                        {
                            foreach (var advance in advances)
                            {
                                await _advanceRepository.DeleteAsync(advance);
                                await _advanceRepository.SaveChangesAsync();
                            }
                        }

                        await _managerRepository.SaveChangesAsync();
                        result = new SuccessResult("Silme İşlemi Başarılı");
                    }

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


        public async Task<IDataResult<List<ManagerListDTO>>> GetAllAsync()
        {
            var managers = await _managerRepository.GetAllAsync();
            var managerDTOs = managers.Adapt<List<ManagerListDTO>>();
            if (managers.Count() <= 0)
            {
                return new ErrorDataResult<List<ManagerListDTO>>(managerDTOs, "Kullanıcı Bulunamadı");
            }
            return new SuccessDataResult<List<ManagerListDTO>>(managerDTOs, "Listeleme Başarılı");

        }



        public async Task<IDataResult<ManagerDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var manager = await _managerRepository.GetByIdAsync(id);
                if (manager == null)
                {
                    return new ErrorDataResult<ManagerDTO>("Kullanıcı Bulunamadı");
                }

                var managerDTO = manager.Adapt<ManagerDTO>();

                return new SuccessDataResult<ManagerDTO>(managerDTO, "Kullanıcı Başarıyla Getirildi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ManagerDTO>("Kullanıcı Getirme İşlemi Başarısız: " + ex.Message);
            }
        }

        public async Task<IDataResult<ManagerDTO>> GetByIdentityUserIdAsync(string identityUserId)
        {
            var manager = await _managerRepository.GetByIdentityId(identityUserId);
            if (manager == null)
            {
                return new ErrorDataResult<ManagerDTO>("Manager bulunamadı");
            }

            var managerDTO = manager.Adapt<ManagerDTO>();
            return new SuccessDataResult<ManagerDTO>(managerDTO, "Manager başarıyla getirildi");
        }

        public async Task<IDataResult<ManagerDTO>> UpdateAsync(ManagerUpdateDTO managerUpdateDTO)
        {
            var updatingUser = await _managerRepository.GetByIdAsync(managerUpdateDTO.Id);
            if (updatingUser == null)
            {
                return new ErrorDataResult<ManagerDTO>("Manager bulunamadı");
            }
            if (updatingUser.Email != managerUpdateDTO.Email && await _accountService.AnyAsync(x => x.Email == managerUpdateDTO.Email))
            {
                return new ErrorDataResult<ManagerDTO>("Bu mail adresi kullanılmaktadır");
            }

            var identity = await _accountService.FindByIdAsync(updatingUser.IdentityId);
            identity.Email = managerUpdateDTO.Email;
            identity.NormalizedEmail = managerUpdateDTO.Email.ToUpperInvariant();
            identity.UserName = managerUpdateDTO.Email;
            identity.Email = managerUpdateDTO.Email.ToUpperInvariant();

            DataResult<ManagerDTO> result = new ErrorDataResult<ManagerDTO>();
            var strategy = await _managerRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _managerRepository.BeginTransection().ConfigureAwait(false);
                try
                {
                    var identityResult = await _accountService.UpdateUserAsync(identity);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorDataResult<ManagerDTO>();
                        transactionScope.Rollback();
                        return;
                    }
                    var updatedUser = managerUpdateDTO.Adapt(updatingUser);
                    await _managerRepository.UpdateAsync(updatedUser);
                    await _managerRepository.SaveChangesAsync();

                    result = new SuccessDataResult<ManagerDTO>(updatingUser.Adapt<ManagerDTO>(), "Kullanıcı Güncelleme");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {

                    result = new ErrorDataResult<ManagerDTO>("Başarıyla güncellendi");
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
}
