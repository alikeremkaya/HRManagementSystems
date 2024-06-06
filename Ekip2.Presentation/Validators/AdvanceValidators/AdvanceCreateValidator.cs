using Ekip2.Presentation.Areas.Manager.Models.AdvanceVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ekip2.Presentation.Validators.AdvanceValidators
{
    public class AdvanceCreateValidator : AbstractValidator<AdvanceCreateVM>
    {
        private readonly IStringLocalizer _localizer;
        public AdvanceCreateValidator(IStringLocalizer<ModelResource> localizer)
        {
            _localizer = localizer;

            RuleFor(ad => ad.Amount)
                .NotEmpty().WithMessage(localizer["AmountIsNotEmpty"]);

            RuleFor(ad => ad.AdvanceDate)
                .NotEmpty().WithMessage(localizer["AdvanceDateIsNotEmpty"]);



           

        }
    }
}
