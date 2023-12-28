using Candidate.Application.DTOs.Education;
using Candidate.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Educations.Commands.CreateEducation
{
    public class CreateEducationCommand : IRequest<BaseCommandResponse>
    {
        public CreateEducationDTO EducationDTO { get; set; }
    }
}
