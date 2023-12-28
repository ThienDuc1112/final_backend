using Candidate.Application.DTOs.Experience;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Experiences.Commands.UpdateExperience
{
    public class UpdateExperienceValidator : AbstractValidator<UpdateExperienceDTO>
    {
        public UpdateExperienceValidator()
        {
            RuleFor(c => c.Company)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(60).WithMessage("{PropertyName} must not exceed 60 characters.");

            RuleFor(c => c.Title)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(c => c.StartDate)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();

            RuleFor(c => c.EndDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(c => c.Responsibility)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
        }

    }
}
