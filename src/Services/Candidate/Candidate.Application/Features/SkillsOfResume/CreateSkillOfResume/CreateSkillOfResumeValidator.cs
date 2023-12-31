using Candidate.Application.DTOs.SkillOfResume;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.SkillsOfResume.CreateSkillOfResume
{
    public class CreateSkillOfResumeValidator : AbstractValidator<CreateSkillOfResumeDTO>
    {
        public CreateSkillOfResumeValidator()
        {
            RuleFor(c => c.SkillId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();

            RuleFor(c => c.ResumeId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        }
    }
}
