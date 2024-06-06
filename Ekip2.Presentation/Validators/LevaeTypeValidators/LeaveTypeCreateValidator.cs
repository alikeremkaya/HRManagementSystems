using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ekip2.Presentation.Validators.LevaeTypeValidators
{
    public class LeaveTypeCreateValidator : AbstractValidator<LeaveTypeCreateVM>
    {
        private readonly IStringLocalizer _localizer;
        public LeaveTypeCreateValidator(IStringLocalizer<ModelResource> localizer)
        {
            _localizer = localizer;
            RuleFor(m => m.Type)
                .NotEmpty().WithMessage(localizer["TypeIsNotEmpty"]);
            RuleFor(m => m.Description)
                .MaximumLength(250).WithMessage(localizer["DescriptionCannotExceed"]);
        }
    }
}
