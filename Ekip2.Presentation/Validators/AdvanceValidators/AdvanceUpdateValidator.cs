using Ekip2.Presentation.Areas.Manager.Models.AdvanceVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace Ekip2.Presentation.Validators.AdvanceValidators
{
    public class AdvanceUpdateValidator : AbstractValidator<AdvanceUpdateVM>
    {
        private readonly IStringLocalizer _localizer;

        public AdvanceUpdateValidator(IStringLocalizer<ModelResource> localizer)
        {
            _localizer = localizer;

            RuleFor(ad => ad.Amount)
                .NotEmpty().WithMessage(_localizer["AmountIsNotEmpty"]);

            RuleFor(ad => ad.AdvanceDate)
                .NotEmpty().WithMessage(_localizer["AdvanceDateIsNotEmpty"]);

        }
    }
}
