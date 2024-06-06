namespace Ekip2.Presentation.Areas.Admin.Models.ChangePasswordVMs
{
    public class AdminChangePasswordVM
    {
        
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        public string RecaptchaResponse { get; set; }
    }
}
