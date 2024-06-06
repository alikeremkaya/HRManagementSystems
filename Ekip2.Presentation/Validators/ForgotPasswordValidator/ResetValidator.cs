using Ekip2.Presentation.Models.ForgotPasswordVMs;
using FluentValidation;

namespace Ekip2.Presentation.Validators.ForgotPasswordValidator
{
    public class ResetValidator : AbstractValidator<ResetPasswordVM>
    {
        public ResetValidator()
        {
            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password Alanı Boş Bırakılamaz");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Password Confirm Alanı Boş Bırakılamaz")
                .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
        }
    }
}
