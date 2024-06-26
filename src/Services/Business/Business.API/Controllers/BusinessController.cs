﻿using Business.Application.DTOs.BusinessInfor;
using Business.Application.Features.Areas.Commands.CreateArea;
using Business.Application.Features.BusinessInfors.Commands.CreateBusinessInfor;
using Business.Application.Features.BusinessInfors.Commands.ReviewBusinessInfor;
using Business.Application.Features.BusinessInfors.Commands.UpdateBusinessInfor;
using Business.Application.Features.BusinessInfors.Queries.GetBusinessById;
using Business.Application.Features.BusinessInfors.Queries.GetBusinessInfor;
using Business.Application.Features.BusinessInfors.Queries.GetIDBusiness;
using Business.Application.Features.Jobs.Queries.GetBusinessListAdmin;
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

        [HttpGet("BusinessList")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBusinessListAdminDTO>> GetBusinessList( [FromQuery(Name = "status")] string? status,[FromQuery(Name = "page")] int page = 1)
        {
            var query = new GetBusinessListAdminQuery
            {
                Page = page,
                Status = status
            };
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpGet("BusinessInforDetail/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BusinessInforDTO>> GetBusinessInfor(string id)
        {
            var query = new GetBusinessInforQuery { Id = id };
            var businessInfor = await _mediator.Send(query);

            return Ok(businessInfor);
        }

        [HttpGet("BusinessDetail/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BusinessInforDTO>> GetBusinessDetail(int id)
        {
            var query = new GetBusinessByIdQuery { Id = id };
            var businessInfor = await _mediator.Send(query);

            return Ok(businessInfor);
        }

        [HttpGet("BusinessId/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBusinessIdDTO>> GetBusinessID(string userId)
        {
            var query = new GetIDBusinessQuery { userId = userId };
            var business = await _mediator.Send(query);
            return Ok(business);
        }

        [HttpPost(Name = "AddBusinessInfor")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateBusinessInforDTO businessInforDTO)
        {
            var command = new CreateBusinessInforCommand { BusinessInforDTO = businessInforDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut(Name = "UpdateBusinessInfor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateBusinessInforDTO businessInforDTO)
        {
            var command = new UpdateBusinessInforCommand { BusinessInforDTO = businessInforDTO };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("ReviewBusiness")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ReviewBusiness([FromBody] ReviewBusinessInforDTO businessInforDTO)
        {
            var command = new ReviewBusinessInforCommand { BusinessInforDTO = businessInforDTO };
           var response = await _mediator.Send(command);
            return Ok(response);
        }


    }
}
