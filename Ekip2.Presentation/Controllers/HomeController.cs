using Ekip2.Application.DTOs.MailDTOs;
using Ekip2.Application.Services.PackageServices;
using Ekip2.Presentation.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using System.Text;

namespace Ekip2.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly IPackageService _packageService;

        public HomeController(ILogger<HomeController> logger, IMailService mailService, IConfiguration configuration, IPackageService packageService)
        {
            _logger = logger;
            _mailService = mailService;
            _configuration = configuration;
            _packageService = packageService;
        }

        public async Task<IActionResult>Index()
        {
            
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> SendRequest(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var emailSettings = _configuration.GetSection("EmailSettings");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("SEAO HR Management", "no-reply@hrmanagement.com"));
                message.To.Add(new MailboxAddress("HR Management", "info.hrmanagementsystem@gmail.com"));
                message.Subject = model.Subject;

                // E-posta gövdesini oluştur
                message.Body = new TextPart("plain")
                {
                    Text = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}\nPackage:{model.Package}"
                };

                try
                {
                    using (var client = new SmtpClient())
                    {
                        // Bağlantıyı kur ve güvenli bağlantı seçeneği (TLS) kullan
                        await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), SecureSocketOptions.StartTls);

                        // Kimlik doğrulaması yap
                        await client.AuthenticateAsync(emailSettings["SmtpUser"], emailSettings["SmtpPass"]);

                        // E-postayı gönder
                        await client.SendAsync(message);

                        // Bağlantıyı kapat
                        await client.DisconnectAsync(true);
                    }

                    return RedirectToAction("Index", new { success = true });
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bilgi ver
                    ModelState.AddModelError("", $"Email gönderilirken hata oluştu: {ex.Message}");
                }
            }

            return View("Index", model);
        }


        private async Task<SelectList> GetPackages(Guid? packageId = null)
        {
            var packages = (await _packageService.GetAllAsync()).Data;
            return new SelectList(packages.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (packageId != null ? packageId.Value : packageId)

            }).OrderBy(x => x.Text), "Value", "Text");




        }

    }

}
