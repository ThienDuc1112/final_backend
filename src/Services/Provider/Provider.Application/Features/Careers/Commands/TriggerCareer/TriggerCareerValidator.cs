using FluentValidation;
using Provider.Application.DTOs.Career;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Commands.TriggerCareer
{
    public class TriggerCareerValidator : AbstractValidator<TriggerCareerDTO>
    {
        public TriggerCareerValidator()
        {
            RuleFor(c => c.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(c => c.IsAllowed)
      .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
