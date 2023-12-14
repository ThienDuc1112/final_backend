using FluentValidation;
using Provider.Application.DTOs.Career;

namespace Provider.Application.Features.Careers.Commands.CreateCareer
{
    public class CreateCareerValidator : AbstractValidator<CreateCareerDTO>
    {
        public CreateCareerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(60).WithMessage("{PropertyName} must not exceed 60 characters.");

            RuleFor(c => c.Description)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
        }
    }
}
