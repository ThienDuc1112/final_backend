using Candidate.Application.Features.LanguagesOfResume.Commands.DeleteLanguageOfResume;
using Candidate.Application.Features.SkillsOfResume.DeleteSkillOfResume;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkillOfResumeController : Controller
    {
        private readonly IMediator _mediator;

        public SkillOfResumeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSkillOfResumeCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
