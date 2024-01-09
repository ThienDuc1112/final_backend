using Business.Application.DTOs.BusinessInfor;
using Business.Application.Features.BusinessInfors.Commands.CreateBusinessInfor;
using Business.Application.Features.BusinessInfors.Commands.UpdateBusinessInfor;
using Business.Application.Features.BusinessInfors.Queries.GetBusinessInfor;
using Business.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Business.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BusinessController : Controller
    {
        private readonly IMediator _mediator;

        public BusinessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("BusinessInforDetail/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BusinessInforDTO>> GetBusinessInfor(int id)
        {
            var query = new GetBusinessInforQuery { Id = id };
            var businessInfor = await _mediator.Send(query);

            return Ok(businessInfor);
        }

        [HttpPost(Name = "AddBusinessInfor")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateBusinessInforDTO businessInforDTO)
        {
            var command = new CreateBusinessInforCommand { BusinessInforDTO = businessInforDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}", Name = "UpdateBusinessInfor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateBusinessInforDTO businessInforDTO)
        {
            businessInforDTO.Id = id;
            var command = new UpdateBusinessInforCommand { BusinessInforDTO = businessInforDTO };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBusinessInforCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
