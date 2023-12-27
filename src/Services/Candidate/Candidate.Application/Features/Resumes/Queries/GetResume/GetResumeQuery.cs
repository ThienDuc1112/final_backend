using Candidate.Application.DTOs.Resume;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Queries.GetResume
{
    public class GetResumeQuery : IRequest<ResumeDTO>
    {
        public int ResumeId { get; set; }
    }
}
