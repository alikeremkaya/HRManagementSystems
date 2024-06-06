using Ekip2.Presentation.Areas.Manager.Models.LeaveVMs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ekip2.Presentation.Validators.LeaveValidators
{
    public class LeaveUpdateValidator : AbstractValidator<LeaveUpdateVM>
    {
        private readonly IStringLocalizer _localizer;
        public LeaveUpdateValidator(IStringLocalizer<ModelResource> localizer)
        {
            
            _localizer = localizer;

            RuleFor(m => m.StartDate)
              .NotEmpty().WithMessage(localizer["StartDateIsNotEmpty"]);
            RuleFor(m => m.EndDate)
              .NotEmpty().WithMessage(localizer["EndDateIsNotEmpty"]);
            
           
            //RuleFor(m => m.Employees)
            // .NotEmpty().WithMessage(localizer["EmployeeTypeIsNotEmpty"]);
        }

       
    }
}
