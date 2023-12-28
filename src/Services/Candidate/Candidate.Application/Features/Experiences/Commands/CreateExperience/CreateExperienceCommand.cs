using Candidate.Application.DTOs.Experience;
using Candidate.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Experiences.Commands.CreateExperience
{
    public class CreateExperienceCommand : IRequest<BaseCommandResponse>
    {
        public CreateExperienceDTO ExperienceDTO { get; set; }
    }
}
