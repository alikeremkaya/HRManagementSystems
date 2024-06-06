namespace Ekip2.Presentation.Areas.Manager.Models.ManagerProfileVMs
{
    public class ManagerProfileVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string RecaptchaResponse { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
