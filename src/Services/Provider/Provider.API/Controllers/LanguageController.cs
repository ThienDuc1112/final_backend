using MediatR;
using Microsoft.AspNetCore.Mvc;
using Provider.Application.DTOs.Language;
using Provider.Application.DTOs.Skill;
using Provider.Application.Features.Languages.Commands.CreateLanguage;
using Provider.Application.Features.Languages.Commands.DeleteLanguage;
using Provider.Application.Features.Languages.Commands.EnableLanguage;
using Provider.Application.Features.Languages.Queries.GetAdminLanguageList;
using Provider.Application.Features.Languages.Queries.GetLanguageList;
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
    public class LanguageController : Controller
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetLanguages")]
        [ProducesResponseType(typeof(IEnumerable<LanguageDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LanguageDTO>>> GetLangugages()
        {
            var query = new GetLanguageListQuery();
            var languages = await _mediator.Send(query);
            return Ok(languages);
        }
        [HttpGet("GetAdminLanguages")]
        [ProducesResponseType(typeof(IEnumerable<LanguageDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LanguageDTO>>> GetAdminLangugages()
        {
            var query = new GetAdminLanguageListQuery();
            var languages = await _mediator.Send(query);
            return Ok(languages);
        }

        [HttpPost(Name = "AddLanguage")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateLanguageDTO languageDTO)
        {
            var command = new CreateLanguageCommand { CreateLanguageDTO = languageDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("EnableLanguage/{id}", Name = "EnableLanguage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> EnableLanguage(int id)
        {
            var command = new EnableLanguageCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPut("DisableLanguage/{id}", Name = "DisableLanguage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DisableLanguage(int id)
        {
            var command = new DeleteLanguageCommand { id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
