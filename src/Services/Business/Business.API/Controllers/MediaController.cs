using Business.Application.DTOs.Job;
using Business.Application.DTOs.Media;
using Business.Application.Features.Jobs.Commands.CreateJob;
using Business.Application.Features.Medias.Commands.DeleteMedia;
using Business.Application.Features.Medias.Commands.UploadMedia;
using Business.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Business.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MediaController : Controller
    {
        private readonly IMediator _mediator;
        public MediaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteMediaCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("UploadMedia")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BaseCommandResponse>>> Add([FromBody] UploadMediaDTO mediaDTO)
        {
            var command = new UploadMediaCommand { MediaDTO = mediaDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
