using Ekip2.Presentation.Areas.Manager.Models.LeaveVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ekip2.Presentation.Validators.LeaveValidators
{
    public class LeaveCreateValidator: AbstractValidator<LeaveCreateVM>
    {
        private readonly IStringLocalizer _localizer;

        public LeaveCreateValidator(IStringLocalizer<ModelResource> localizer) 
        {
            _localizer = localizer;
            RuleFor(m => m.StartDate)
              .NotEmpty().WithMessage(localizer["StartDateIsNotEmpty"]);
            RuleFor(m => m.EndDate)
              .NotEmpty().WithMessage(localizer["EndDateIsNotEmpty"]);
            

        }
    }
}
