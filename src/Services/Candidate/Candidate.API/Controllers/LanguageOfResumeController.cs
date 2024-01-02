
using Candidate.Application.Features.LanguagesOfResume.Commands.DeleteLanguageOfResume;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LanguageOfResumeController : Controller
    {
        private readonly IMediator _mediator;

        public LanguageOfResumeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLanguageOfResumeCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
