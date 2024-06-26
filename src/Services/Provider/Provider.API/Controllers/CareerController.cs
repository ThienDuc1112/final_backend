﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Provider.Application.DTOs.Career;
using Provider.Application.Features.Careers.Commands.CreateCareer;
using Provider.Application.Features.Careers.Commands.DeleteCareer;
using Provider.Application.Features.Careers.Commands.TriggerCareer;
using Provider.Application.Features.Careers.Commands.UpdateCareer;
using Provider.Application.Features.Careers.Queries.GetCareerAdminList;
using Provider.Application.Features.Careers.Queries.GetCareerList;
using Provider.Application.Features.Careers.Queries.GetCareersWithSkills;
using Provider.Application.Responses;
using System.Net;

namespace Provider.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CareerController : Controller
    {
       private readonly IMediator _mediator;

       public CareerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllCareers")]
        [ProducesResponseType(typeof(IEnumerable<CareerDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CareerDTO>>> GetAllCareers()
        {
            var query = new GetCareerListQuery();
            var careers = await _mediator.Send(query);
            return Ok(careers);
        }
        [HttpGet("GetAdminCareers")]
        [ProducesResponseType(typeof(IEnumerable<CareerDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CareerDTO>>> GetAdminCareers()
        {
            var query = new GetCareerAdminListQuery();
            var careers = await _mediator.Send(query);
            return Ok(careers);
        }

        [HttpGet("GetCareersWithSkills")]
        [ProducesResponseType(typeof(IEnumerable<GetCareerDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<GetCareerDTO>>> GetCareersWithSkills()
        {
            var query = new GetCareersWithSkillsQuery();
            var careers = await _mediator.Send(query);
            return Ok(careers);
        }

        [HttpPost(Name = "AddCareer")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateCareerDTO careerDTO)
        {
            var command = new CreateCareerCommand { CareerDTO = careerDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut(Name = "UpdateCareer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateCareerDTO careerDTO)
        {
            var command = new UpdateCareerCommand { CareerDTO = careerDTO };
            var response =  await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("TriggerCareer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> TriggerCareer([FromBody] TriggerCareerDTO careerDTO)
        {
            var command = new TriggerCareerCommand { TriggerCareerDTO = careerDTO };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCareerCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
