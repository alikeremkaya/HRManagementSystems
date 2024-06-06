using Ekip2.Presentation.Areas.Admin.Models.CompanyVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ekip2.Presentation.Validators.CompanyValidators
{
    public class CompanyCreateValidator : AbstractValidator<CompanyCreateVM>
    {
        private readonly IStringLocalizer _localizer;
        public CompanyCreateValidator(IStringLocalizer<ModelResource> localizer)
        {
            _localizer = localizer;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(localizer["CompanyNameIsNotEmpty"]);
            RuleFor(a => a.Address).NotEmpty().WithMessage(localizer["Address cannot exceed 128 characters"]);

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(localizer["Phone number is required"])
            .Length(11).WithMessage(localizer["Phone number must be exactly 11 digits"]);
            
        }
    }
}
