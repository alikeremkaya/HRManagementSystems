namespace Ekip2.Presentation.Areas.Manager.Models.ChangePasswordVMs
{
    public class ManagerChangePasswordVM
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string RecaptchaResponse { get; set; }
    }
}
