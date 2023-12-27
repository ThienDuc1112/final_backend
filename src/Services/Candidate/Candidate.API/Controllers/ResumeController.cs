using Candidate.Application.DTOs.Resume;
using Candidate.Application.Features.Resumes.Commands.CreateResume;
using Candidate.Application.Features.Resumes.Queries.GetResume;
using Candidate.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Candidate.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ResumeController : Controller
    {
        private readonly IMediator _mediator;

        public ResumeController(IMediator mediator)
        {
           _mediator = mediator;
        }

        [HttpGet(Name ="GetResume/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResumeDTO>> GetResume(int resumeId)
        {
            var query = new GetResumeQuery { ResumeId = resumeId };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost(Name ="AddResume")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateResumeDTO resumeDTO)
        {
            var command = new CreateResumeCommand { ResumeDTO = resumeDTO };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
