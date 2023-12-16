using FluentValidation;
using Provider.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillValidator :AbstractValidator<UpdateSkillDTO>
    {
        public UpdateSkillValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage("This skill must be present");

            RuleFor(s => s.NameSkill)
               .NotEmpty().WithMessage("Name of Skill is required.")
               .MaximumLength(60).WithMessage("Name of Skill must not exceed 60 characters.");
        }
    }
}
