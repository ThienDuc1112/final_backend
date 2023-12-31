using Candidate.Application.DTOs.SkillOfResume;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.SkillsOfResume.UpdateSkillOfResume
{
    public class UpdateSkillOfResumeCommand : IRequest<Unit>
    {
        public UpdateSkillOfResumeDTO SkillOfResumeDTO { get; set; }
    }
}
