using FluentValidation;
using Provider.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.TriggerSkill
{
    public class TriggerSkillValidator :AbstractValidator<TriggerSkillDTO>
    {
        public TriggerSkillValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage("This skill must be present");
            RuleFor(p => p.CreatedBy).NotNull().NotEmpty().WithMessage("CreatedBy can't be null or empty");
        }
    }
}
