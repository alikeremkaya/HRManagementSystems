namespace Ekip2.Presentation.Areas.Employee.Models.ChangePasswordVMs
{
    public class EmployeeChangePasswordVM
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string RecaptchaResponse { get; set; }
    }
}
