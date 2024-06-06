using Ekip2.Application.DTOs.MailDTOs;
using Ekip2.Application.DTOs.ManagerDTOs;
using Ekip2.Application.Services.MailServices;
using Ekip2.Application.Services.ManagerServices;
using Ekip2.Domain.Entities;
using Ekip2.Domain.Enums;
using Ekip2.Presentation.Models.LoginVMs;
using Ekip2.Presentation.Validators.reCAPTCHAValidators;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Newtonsoft.Json;
using MimeKit;
using Ekip2.Presentation.Models.ForgotPasswordVMs;
using MailKit.Net.Smtp;
using Ekip2.Presentation.Models.ChangePasswordVMs;
using Ekip2.Infrastructure.Repositories.ManagerRepositories;
using Ekip2.Application.Services.AccountServices;
using Ekip2.Application.DTOs.AdminDTOs;
using Ekip2.Domain.Utilities.Concretes;
using Ekip2.Application.Services.PackageLogServices;
using Hangfire;

namespace Ekip2.Presentation.Controllers;

public class AccountController : BaseController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IMailService _mailService;
    private readonly IManagerRepository _managerRepository;
    private readonly IConfiguration _configuration;
    private readonly IAccountService _accountService;
    private readonly IPackageLogService _packageLogService;




    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMailService mailService, IManagerRepository managerRepository, IConfiguration configuration, IAccountService accountService, IPackageLogService packageLogService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mailService = mailService;
        _managerRepository = managerRepository;
        _configuration = configuration;
        _accountService = accountService;
        _packageLogService = packageLogService;
    }

    public async Task<IActionResult> Login()
    {
       
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Email veya şifre hatalı!!");
            return View(model);
        }

        var checkPass = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (!checkPass.Succeeded)
        {
            // Şifre politikasına uyumluluğunu kontrol et
            var passwordValidationResult = await _userManager.PasswordValidators[0].ValidateAsync(_userManager, user, model.Password);
            if (!passwordValidationResult.Succeeded)
            {
                foreach (var error in passwordValidationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre hatalı!!");
            }
            return View(model);
        }

        // Kullanıcı rolünü al
        var userRole = await _userManager.GetRolesAsync(user);
        if (userRole == null || userRole.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Email veya şifre hatalı!!!");
            return View(model);
        }

        // Abonelik durumu kontrolü
        var manager = await _managerRepository.GetByIdentityId(user.Id); // Kullanıcının şirket bilgisini al
        if (manager != null)
        {
            var allSubscriptions = await _packageLogService.GetAllAsync();
            var companySubscription = allSubscriptions.Data
                .Where(pl => pl.CompanyId == manager.CompanyId)
                .OrderByDescending(pl => pl.EndDate)
                .FirstOrDefault();

            // Eğer aktif bir abonelik varsa ve süresi geçmemişse girişe izin ver
            if (companySubscription == null || !companySubscription.IsActive || companySubscription.EndDate < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Şirketinizin aboneliği sona ermiştir. Lütfen yöneticinizle iletişime geçin.");
                return View(model);
            }
        }

        // Hangfire'da arka plan görevi olarak abonelik kontrolü
        var hangfireCheck = BackgroundJob.Enqueue(() => _accountService.CheckSubscriptionStatus());

        // Sadece Manager ve Employee rolü için IsFirstLogin kontrolü yap
        if (userRole.Contains("Manager") || userRole.Contains("Employee"))
        {
            if (manager == null)
            {
                ModelState.AddModelError(string.Empty, $"{userRole[0]} bilgisi alınamadı!!");
                TempData["ErrorMessage"] = "Böyle bir kullanıcı bulunmamaktadır.";
                return View(model);
            }

            if (manager.IsFirstLogin)
            {
                return RedirectToAction("ChangePassword", "Account");
            }
        }

        // Admin için IsFirstLogin kontrolü yapılmadan giriş yapılır
        return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }


    [HttpGet]
    public IActionResult ForgetPassword()
    {

        return View();
    }



    [HttpPost]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)

    {
        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "Invalid form data.";
            return View(forgetPasswordVM);
        }

        var user = await _userManager.FindByEmailAsync(forgetPasswordVM.Mail);
        if (user == null)
        {
            ViewBag.ErrorMessage = "The email address does not exist in our system.";
            return View(forgetPasswordVM);
        }

        string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var passwordResetTokenLink = Url.Action("ResetPassword", "Account", new
        {
            userId = user.Id,
            token = passwordResetToken
        }, HttpContext.Request.Scheme);

        // HTML formatında e-posta içeriği oluşturma
        var htmlMessage = $@"
        <h1>Şifre Yenileme Talebi</h1>
        <p>Merhaba {user.UserName},</p>
        <p>Şifrenizi yenilemek için aşağıdaki bağlantıya tıklayın:</p>
        <a href='{passwordResetTokenLink}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>Şifrenizi Yenileyin</a>
        <p>Bu bağlantı 24 saat geçerlidir.</p>
        <p>Teşekkürler,</p>
        <p>SEAO Ekibi</p>";

        var mailDTO = new MailDTO
        {
            Email = forgetPasswordVM.Mail,
            Subject = "Şifre Yenileme Talebi",
            Message = htmlMessage
        };

        await _mailService.SendMailAsync(mailDTO);
        ViewBag.IsSuccess = true;

        return View();
    }


    [HttpGet]
    public async Task<IActionResult> ResetPassword(string userId, string token)
    {
        TempData["userId"] = userId;
        TempData["token"] = token;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
    {
        if (!ModelState.IsValid)
        {
            return View(resetPasswordVM);
        }

        var userId = TempData["userId"]?.ToString();
        var token = TempData["token"]?.ToString();

        if (userId == null || token == null)
        {
            // TempData hatalı ise
            TempData["ErrorMessage"] = "Password reset link is invalid or expired.";
            return RedirectToAction("ForgetPassword", "Account");
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            // Kullanıcı bulunamadıysa
            TempData["ErrorMessage"] = "User not found.";
            return RedirectToAction("ForgetPassword", "Account");
        }

        var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Login", "Account");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(resetPasswordVM);
        }
    }
    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ChangePassword(IsFirstLoginChangePasswordVM model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Form verileri geçersiz.";
            return View(model);
        }

        // reCAPTCHA doğrulama
        var recaptchaResponse = model.RecaptchaResponse;
        var secretKey = _configuration["RecaptchaSecretKey"];
        var client = new HttpClient();
        var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={recaptchaResponse}", null);
        var resultRecaptcha = await response.Content.ReadAsStringAsync();
        var captchaResult = JsonConvert.DeserializeObject<RecaptchaVerificationResult>(resultRecaptcha);

        if (!captchaResult.Success)
        {
            ModelState.AddModelError(string.Empty, "reCAPTCHA doğrulaması başarısız.");
            return View(model);
        }

        // Yeni şifre ve doğrulama şifresinin eşleşme kontrolü
        if (model.NewPassword != model.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Yeni şifre ve doğrulama şifresi uyuşmuyor.");
            return View(model);
        }

        // Kullanıcıyı bulma
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
            return View(model);
        }

        // Yeni şifreyi Identity kurallarına göre doğrulama
        foreach (var validator in _userManager.PasswordValidators)
        {
            var result = await validator.ValidateAsync(_userManager, user, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        // Şifre değiştirme işlemi
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        // IsFirstLogin durumunu güncelleme
        var manager = await _managerRepository.GetByIdentityId(user.Id);
        if (manager != null)
        {
            manager.IsFirstLogin = false;
            await _managerRepository.UpdateAsync(manager);
            await _managerRepository.SaveChangesAsync();
        }

        // Oturum yenileme
        await _signInManager.RefreshSignInAsync(user);
        TempData["SuccessMessage"] = "Şifre başarıyla değiştirildi.";

        // Login ekranına yönlendirme
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }


}
