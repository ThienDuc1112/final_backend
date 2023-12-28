using Candidate.Application.DTOs.Education;
using Candidate.Application.Features.Educations.Commands.CreateEducation;
using Candidate.Application.Features.Educations.Commands.DeleteEducation;
using Candidate.Application.Features.Educations.Commands.UpdateEducation;
using Candidate.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Candidate.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EducationController : Controller
    {
        private readonly IMediator _mediator;

        public EducationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddEducation")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateEducationDTO educationDTO)
        {
            var command = new CreateEducationCommand { EducationDTO = educationDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}", Name = "UpdateEducation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateEducationDTO educationDTO)
        {
            educationDTO.Id = id;
            var command = new UpdateEducationCommand { EducationDTO = educationDTO };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEducationCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
