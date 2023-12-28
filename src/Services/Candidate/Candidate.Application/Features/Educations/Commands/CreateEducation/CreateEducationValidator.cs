using Candidate.Application.DTOs.Education;
using FluentValidation;


namespace Candidate.Application.Features.Educations.Commands.CreateEducation
{
    public class CreateEducationValidator : AbstractValidator<CreateEducationDTO>
    {
        public CreateEducationValidator()
        {
            RuleFor(c => c.UniversityName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(c => c.Major)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(c => c.Degree)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(c => c.StartDate)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(c => c.EndDate)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
