using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.ChangePasswordDTOs
{
    public class IsFirstLoginChangePasswordDTO
    {
        public Guid Id { get; set; }


        public string OldPassword { get; set; }


        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string RecaptchaResponse { get; set; }
    }



}
