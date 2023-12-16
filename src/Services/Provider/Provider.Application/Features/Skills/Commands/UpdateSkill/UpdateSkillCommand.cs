using MediatR;
using Provider.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommand : IRequest<Unit>
    {
        public UpdateSkillDTO skillDTO { get; set; }
    }
}
