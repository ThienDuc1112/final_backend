using Candidate.Application.DTOs.Resume;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Commands.HideResume
{
    public class HideResumeValidator : AbstractValidator<HideResumeDTO>
    {
        public HideResumeValidator()
        {
            RuleFor(c => c.Id).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.IsPublic).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
