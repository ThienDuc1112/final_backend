using MediatR;
using Provider.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.TriggerSkill
{
    public class TriggerSkillCommand : IRequest<Unit>
    {
        public TriggerSkillDTO TriggerSkillDTO { get; set; }
    }
}
