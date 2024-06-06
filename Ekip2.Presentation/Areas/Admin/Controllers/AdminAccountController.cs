

using Ekip2.Application.DTOs.AdminDTOs;
using Ekip2.Presentation.Areas.Manager.Models.ManagerProfileVMs;

namespace Ekip2.Presentation.Areas.Admin.Controllers
{
    public class AdminAccountController : AdminBaseController
    {
        private readonly IAdminService _adminService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AdminAccountController(IAdminService adminService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _adminService = adminService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _adminService.GetByIdentityUserIdAsync(user.Id);
            if (!result.IsSuccess)
            {
                // Hata durumunu ele alın (örneğin, hata mesajı gösterin)
                return RedirectToAction("Index", "Home");
            }

            var adminDto = result.Data;
            return PartialView("_AdminProfileModal", adminDto);
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AdminChangePasswordVM model)
        {
            

            //// reCAPTCHA doğrulama
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

            // Yeni şifre ve doğrulama şifresinin eşleşme kontrolü
            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Yeni şifre ve doğrulama şifresi uyuşmuyor.";
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }

            // Kullanıcıyı bul
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }

            // Şifre değiştirme işlemi
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    TempData["ErrorMessage"] += error.Description + " ";
                }
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }

            // Oturum yenileme
            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = "Şifre başarıyla değiştirildi.";
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }



    }
}
