using Ekip2.Application.DTOs.ChangePasswordDTOs;
using Ekip2.Application.Services.ManagerServices;
using Ekip2.Presentation.Areas.Employee.Models.ChangePasswordVMs;
using Ekip2.Presentation.Areas.Employee.Models.EmployeeProfileVMs;
using Ekip2.Presentation.Areas.Manager.Models.ManagerProfileVMs;
using Ekip2.Presentation.Validators.reCAPTCHAValidators;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace Ekip2.Presentation.Areas.Employee.Controllers
{
    public class EmployeeAccountController : EmployeeBaseController
 {
     private readonly IManagerService _managerService;
     private readonly UserManager<IdentityUser> _userManager;
     private readonly SignInManager<IdentityUser> _signInManager;
     private readonly IConfiguration _configuration;
     public EmployeeAccountController(IManagerService managerService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
     {
         _managerService = managerService;
         _userManager = userManager;
         _signInManager = signInManager;
         _configuration = configuration;
     }

        [HttpGet]
        public async Task<IActionResult> Profile(Guid id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bilgileri yüklenemedi.";
                return RedirectToAction("Index", "Home");
            }

            var result = await _managerService.GetByIdentityUserIdAsync(userId);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = "Yönetici bilgileri yüklenemedi.";
                return RedirectToAction("Index", "Home");
            }

            var managerProfileVM = result.Data.Adapt<ManagerProfileVM>();
            if (managerProfileVM.Image != null && managerProfileVM.Image.Length > 0)
            {
                managerProfileVM.ImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(managerProfileVM.Image)}";
                ViewBag.ProfileImageUrl = managerProfileVM.ImageUrl;
            }
            return PartialView("_EmployeeProfileModal", managerProfileVM);
        }


        [HttpGet]
     public IActionResult ChangePassword()
     {
         return View();
     }


     [HttpPost]
     public async Task<IActionResult> ChangePassword(EmployeeChangePasswordVM model)
     {
         if (!ModelState.IsValid)
         {
             TempData["ErrorMessage"] = "Form verileri geçersiz.";
             return RedirectToAction("Profile");
         }
         //var recaptchaResponse = model.RecaptchaResponse;
         //var secretKey = _configuration["RecaptchaSecretKey"];
         //var client = new HttpClient();
         //var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={recaptchaResponse}", null);
         //var resultRecaptcha = await response.Content.ReadAsStringAsync();
         //var captchaResult = JsonConvert.DeserializeObject<RecaptchaVerificationResult>(resultRecaptcha);

         //if (!captchaResult.Success)
         //{
         //    ModelState.AddModelError(string.Empty, "reCAPTCHA doğrulaması başarısız.");
         //    return View(model);
         //}

         if (model.NewPassword != model.ConfirmPassword)
         {
             TempData["ErrorMessage"] = "Yeni şifre ve doğrulama şifresi uyuşmuyor.";
                return RedirectToAction("Index", "Home", new { Area = "Employee" });
            }

         var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
         var user = await _userManager.FindByIdAsync(userId);
         if (user == null)
         {
             TempData["ErrorMessage"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index", "Home", new { Area = "Employee" });
            }

         var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
         if (!result.Succeeded)
         {
             TempData["ErrorMessage"] = string.Join(" ", result.Errors.Select(e => e.Description));
                return RedirectToAction("Index", "Home", new { Area = "Employee" });
            }

         await _signInManager.RefreshSignInAsync(user);
         TempData["SuccessMessage"] = "Şifre başarıyla değiştirildi.";
            return RedirectToAction("Index", "Home", new { Area = "Employee" });
        }


 }
}
