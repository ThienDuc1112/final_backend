using Candidate.Application.DTOs.SkillOfResume;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.SkillsOfResume.UpdateSkillOfResume
{
    public class UpdateSkillOfResumeValidator : AbstractValidator<UpdateSkillOfResumeDTO>
    {
        public UpdateSkillOfResumeValidator()
        {
            RuleFor(c => c.SkillId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();

            RuleFor(c => c.Id)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();

        }
    }
}
