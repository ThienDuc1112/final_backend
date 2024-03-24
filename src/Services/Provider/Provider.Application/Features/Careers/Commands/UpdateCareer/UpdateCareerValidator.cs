using FluentValidation;
using Provider.Application.DTOs.Career;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Commands.UpdateCareer
{
    public class UpdateCareerValidator : AbstractValidator<UpdateCareerDTO>
    {
        public UpdateCareerValidator()
        {
            RuleFor(c => c.Id)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
        }
    }
}
