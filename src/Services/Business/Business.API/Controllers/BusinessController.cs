using Business.Application.DTOs.BusinessInfor;
using Business.Application.Features.Areas.Commands.CreateArea;
using Business.Application.Features.BusinessInfors.Commands.CreateBusinessInfor;
using Business.Application.Features.BusinessInfors.Commands.UpdateBusinessInfor;
using Business.Application.Features.BusinessInfors.Queries.GetBusinessInfor;
using Business.Application.Responses;
using Business.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Business.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BusinessController : Controller
    {
        private readonly IMediator _mediator;
        private readonly BusinessDbContext _dbContext;

        public BusinessController(IMediator mediator, BusinessDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [HttpGet("BusinessInforDetail/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BusinessInforDTO>> GetBusinessInfor(string id)
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

       
    }
}
