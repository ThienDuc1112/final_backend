using Candidate.Application.DTOs.Resume;
using Candidate.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Commands.CreateResume
{
    public class CreateResumeCommand : IRequest<BaseCommandResponse>
    {
        public CreateResumeDTO ResumeDTO { get; set; }
    }
}
