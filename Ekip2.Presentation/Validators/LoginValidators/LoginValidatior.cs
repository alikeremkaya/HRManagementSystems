using Ekip2.Presentation.Models.LoginVMs;
using FluentValidation;

namespace Ekip2.Presentation.Validators.LoginValidators
{
    public class LoginValidatior : AbstractValidator<LoginVM>
    {
        public LoginValidatior()
        {
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email Alanı Boş Bırakılamaz")
              .EmailAddress().WithMessage("Email alanı uygun formatta girilmelidir");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password Alanı Boş Bırakılamaz");

           
        }
    }
}
