using Candidate.Application.DTOs.Resume;
using Candidate.Application.Responses;
using MediatR;


namespace Candidate.Application.Features.Resumes.Commands.UpdateResume
{
    public class UpdateResumeCommand : IRequest<BaseCommandResponse>
    {
        public UpdateResumeDTO ResumeDTO { get; set; }
    }
}
