namespace Ekip2.Presentation.Models.ChangePasswordVMs
{
    public class IsFirstLoginChangePasswordVM
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string RecaptchaResponse { get; set; }

    }
}

