

namespace Ekip2.Application.Services.MailServices
{
    public class MailService : IMailService
    {
        public async Task SendMailAsync(MailDTO mailDTO)
        {//xhqwydkdlastoobo

            try
            {
                // E-posta mesajını oluştur
                var newMail = new MimeMessage
                {
                    From = { MailboxAddress.Parse("info.hrmanagementsystem@gmail.com") },
                    To = { MailboxAddress.Parse(mailDTO.Email) },
                    Subject = mailDTO.Subject
                };

                // E-posta gövdesini oluştur
                var builder = new BodyBuilder
                {
                    HtmlBody = mailDTO.Message
                };
                newMail.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    // Gmail sunucusuna bağlan
                    await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // Kimlik doğrulaması yap
                    await smtp.AuthenticateAsync("info.hrmanagementsystem@gmail.com", "xhqwydkdlastoobo");

                    // E-postayı gönder
                    await smtp.SendAsync(newMail);

                    // Bağlantıyı kapat
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new InvalidOperationException($"Email gönderilirken hata oluştu: {ex.Message}", ex);
            }

        }

    }
}
