using Ekip2.Application.DTOs.MailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.MailServices
{
    public interface IMailService
    {
        /// <summary>
        /// Belirtilen e-posta bilgileri ile bir e-posta gönderir.
        /// </summary>
        /// <param name="mailDTO">Gönderilecek e-postanın bilgilerini içeren DTO.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev.</returns>
        Task SendMailAsync(MailDTO mailDTO);

    }
}
