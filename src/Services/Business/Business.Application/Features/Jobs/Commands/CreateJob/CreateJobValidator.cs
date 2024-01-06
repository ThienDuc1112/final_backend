using Business.Application.DTOs.Job;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobValidator : AbstractValidator<CreateJobDTO>
    {
        public CreateJobValidator()
        {
            RuleFor(dto => dto.CareerId).NotEmpty().NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(dto => dto.BusinessId).NotEmpty().NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(dto => dto.LanguageRequirementId).NotEmpty()
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(dto => dto.Title).NotEmpty().WithMessage("{PropertyName} is required").
                MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.NumberRecruitment).GreaterThan(0);

            RuleFor(dto => dto.ExpirationDate).NotEmpty().
                GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("{PropertyName} must start at the following days");

            RuleFor(dto => dto.EducationLevelMin).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.YearExpMin).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.GenderRequirement).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.Address).MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.JobType)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.CareerLevel)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.SalaryMin).GreaterThanOrEqualTo(0);

            RuleFor(dto => dto.SalaryMax).GreaterThanOrEqualTo(dto => dto.SalaryMin);

            RuleFor(dto => dto.Description).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(3000).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.Welfare)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(3000).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.Requirement).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(3000)
                .WithMessage("{PropertyName} must not exceed {MaxLength} characters"); 

            RuleFor(dto => dto.Responsibilities).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(3000)
                .WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(dto => dto.Status).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
