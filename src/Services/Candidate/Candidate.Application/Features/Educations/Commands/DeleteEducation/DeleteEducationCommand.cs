using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Educations.Commands.DeleteEducation
{
    public class DeleteEducationCommand : IRequest
    {
        public int Id { get; set; }
    }
}
