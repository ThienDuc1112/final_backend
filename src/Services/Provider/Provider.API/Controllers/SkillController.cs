using MediatR;
using Microsoft.AspNetCore.Mvc;
using Provider.Application.DTOs.Career;
using Provider.Application.DTOs.Skill;
using Provider.Application.Features.Careers.Commands.CreateCareer;
using Provider.Application.Features.Careers.Commands.DeleteCareer;
using Provider.Application.Features.Careers.Commands.UpdateCareer;
using Provider.Application.Features.Careers.Queries.GetCareerList;
using Provider.Application.Features.Skills.Commands.CreateSkill;
using Provider.Application.Features.Skills.Commands.DeleteSkill;
using Provider.Application.Features.Skills.Commands.UpdateSkill;
using Provider.Application.Features.Skills.Queries.GetSkillListByCareer;
using Provider.Application.Responses;
using System.Net;

namespace Provider.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkillController : Controller
    {
        private readonly IMediator _mediator;

        public SkillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetSkillsByCareerId")]
        [ProducesResponseType(typeof(IEnumerable<SkillDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<SkillDTO>>> GetSkillsByCareerId(int careerId)
        {
            var query = new GetSkillListByCareerQuery(careerId);
            var skills = await _mediator.Send(query);
            return Ok(skills);
        }

        [HttpPost(Name = "AddSkill")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateSkillDTO skillDTO)
        {
            var command = new CreateSkillCommand { SkillDTO = skillDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}", Name = "UpdateSkill")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateSkillDTO skillDTO)
        {
            var command = new UpdateSkillCommand { skillDTO = skillDTO };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSkillCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
