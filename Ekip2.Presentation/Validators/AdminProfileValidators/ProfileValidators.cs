using Ekip2.Presentation.Areas.Admin.Models.ChangePasswordVMs;
using FluentValidation;

namespace Ekip2.Presentation.Validators.AdminProfileValidators
{
    public class ProfileValidators : AbstractValidator<AdminChangePasswordVM>
    {
        public ProfileValidators()
        {
            RuleFor(x => x.OldPassword)
              .NotEmpty().WithMessage("Password Alanı Boş Bırakılamaz");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password Confirm Alanı Boş Bırakılamaz");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Password Confirm Alanı Boş Bırakılamaz")
                .Equal(x => x.NewPassword).WithMessage("Şifreler eşleşmiyor.");
            RuleFor(x => x.RecaptchaResponse)
               .NotEmpty().WithMessage("reCAPTCHA Alanı Boş Bırakılamaz");
        }
    }
}
