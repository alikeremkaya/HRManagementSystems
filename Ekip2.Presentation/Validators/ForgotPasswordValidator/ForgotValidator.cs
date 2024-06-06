using Ekip2.Presentation.Models.ForgotPasswordVMs;
using FluentValidation;

namespace Ekip2.Presentation.Validators.ForgotPasswordValidator
{
    public class ForgotValidator : AbstractValidator<ForgetPasswordVM>
    {
        public ForgotValidator()
        {
            RuleFor(x => x.Mail)
             .NotEmpty().WithMessage("Email Alanı Boş Bırakılamaz")
             .EmailAddress().WithMessage("Email alanı uygun formatta girilmelidir");
        }
    }
}
