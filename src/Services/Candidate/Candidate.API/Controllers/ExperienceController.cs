using Candidate.Application.DTOs.Experience;
using Candidate.Application.Features.Experiences.Commands.CreateExperience;
using Candidate.Application.Features.Experiences.Commands.DeleteExperience;
using Candidate.Application.Features.Experiences.Commands.UpdateExperience;
using Candidate.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Candidate.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ExperienceController : Controller
    {
        private readonly IMediator _mediator;

        public ExperienceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddExperience")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateExperienceDTO experienceDTO)
        {
            var command = new CreateExperienceCommand { ExperienceDTO = experienceDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}", Name = "UpdateExperience")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateExperienceDTO experienceDTO)
        {
            experienceDTO.Id = id;
            var command = new UpdateExperienceCommand { ExperienceDTO = experienceDTO };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteExperienceCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
