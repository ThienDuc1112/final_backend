using Business.Application.Features.Areas.Commands.DeleteArea;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Business.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AreaController : Controller
    {
       private readonly IMediator _mediator;
       public AreaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteAreaCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
