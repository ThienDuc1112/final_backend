using Business.Application.Features.Medias.Commands.DeleteMedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
