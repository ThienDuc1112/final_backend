using Candidate.Application.DTOs.Resume;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Commands.CreateResume
{
    public class CreateResumeValidator : AbstractValidator<CreateResumeDTO>
    {
        public CreateResumeValidator()
        {
            RuleFor(c => c.AvatarUrl)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(60).WithMessage("{PropertyName} must not exceed 60 characters.")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");

            RuleFor(c => c.UserId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(c => c.CareerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(c => c.FullName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(c => c.Linkedln)
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(c => c.Gender)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(c => c.Country)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(BeValidDateOfBirth).WithMessage("{PropertyName} must be a valid date of birth.");

            RuleFor(c => c.StatusOfEmployment)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(c => c.PhoneNumber)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .MaximumLength(15).WithMessage("{PropertyName} must not exceed 15 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }

        private bool BeValidDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now)
            {
                return false;
            }
            return true;
        }
        private bool IsPhoneNumberNumeric(string phoneNumber)
        {
            long numericValue;
            return long.TryParse(phoneNumber, out numericValue);
        }
    }
}
