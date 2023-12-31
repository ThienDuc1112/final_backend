using Candidate.Application.DTOs.SkillOfResume;
using Candidate.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.SkillsOfResume.CreateSkillOfResume
{
    public class CreateSkillOfResumeCommand : IRequest<BaseCommandResponse>
    {
        public CreateSkillOfResumeDTO SkillOfResumeDTO { get; set; }
    }
}
