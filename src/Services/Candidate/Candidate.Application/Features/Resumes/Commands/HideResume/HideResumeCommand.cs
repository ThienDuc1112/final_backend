using Candidate.Application.DTOs.Resume;
using Candidate.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Commands.HideResume
{
    public class HideResumeCommand : IRequest<BaseCommandResponse>
    {
        public HideResumeDTO HideResumeDTO { get; set; }
    }
}
