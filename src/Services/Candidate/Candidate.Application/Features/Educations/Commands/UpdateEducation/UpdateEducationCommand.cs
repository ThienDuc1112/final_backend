using Candidate.Application.DTOs.Education;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Educations.Commands.UpdateEducation
{
    public class UpdateEducationCommand : IRequest<Unit>
    {
        public UpdateEducationDTO EducationDTO { get; set; }
    }
}
