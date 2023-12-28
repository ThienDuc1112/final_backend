using Candidate.Application.DTOs.Experience;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Experiences.Commands.UpdateExperience
{
    public class UpdateExperienceCommand : IRequest<Unit>
    {
        public UpdateExperienceDTO ExperienceDTO { get; set; }
    }
}
