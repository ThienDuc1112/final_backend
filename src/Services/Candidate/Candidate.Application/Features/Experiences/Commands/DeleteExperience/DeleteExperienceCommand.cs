using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Experiences.Commands.DeleteExperience
{
    public class DeleteExperienceCommand : IRequest
    {
        public int Id { get; set; }
    }
}
