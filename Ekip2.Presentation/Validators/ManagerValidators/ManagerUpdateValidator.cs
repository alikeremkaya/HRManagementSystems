using Ekip2.Presentation.Areas.Admin.Models.ManagerVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ekip2.Presentation.Validators.ManagerValidators
{
    public class ManagerUpdateValidator : AbstractValidator<ManagerUpdateVM>
    {
        private readonly IStringLocalizer _localizer;
        public ManagerUpdateValidator(IStringLocalizer<ModelResource> localizer)
        {
            _localizer = localizer;

            RuleFor(m => m.FirstName)
                .NotEmpty().WithMessage(localizer["FirstNameIsNotEmpty"])
                .MaximumLength(128).WithMessage(localizer["MaxLength128"]);

            RuleFor(m => m.LastName)
                .NotEmpty().WithMessage(localizer["LastNameIsNotEmpty"])
                .MaximumLength(128).WithMessage(localizer["MaxLength128"]);

            RuleFor(m => m.Email)
             .NotEmpty().WithMessage(localizer["EmailIsNotEmpty"])
             .EmailAddress().WithMessage(localizer["PleaseEnterAValidEmailAddress"]);

            RuleFor(m => m.CompanyId)
                 .NotEmpty().WithMessage(localizer["CompanyIsNotEmpty"])
                 .Must(id => id != Guid.Empty).WithMessage(localizer["CompanyIsNotEmpty"]);

            RuleFor(m => m.Address)
               .NotEmpty().WithMessage(localizer["AddressIsNotEmpty"])
               .MaximumLength(128).WithMessage(localizer["MaxLength128"]);

            RuleFor(m => m.PhoneNumber)
                .NotEmpty().WithMessage(localizer["PhoneNumberIsNotEmpty"])
                .Length(11).WithMessage(localizer["Phone number must be exactly 11 digits"]);

        }
    }
}
