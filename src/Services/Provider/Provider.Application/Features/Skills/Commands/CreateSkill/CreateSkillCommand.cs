using MediatR;
using Provider.Application.DTOs.Skill;
using Provider.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.CreateSkill
{
    public class CreateSkillCommand : IRequest<BaseCommandResponse>
    {
        public CreateSkillDTO SkillDTO { get; set; }
    }
}
