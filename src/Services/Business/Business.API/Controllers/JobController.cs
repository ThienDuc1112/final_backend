using Business.Application.DTOs.Job;
using Business.Application.Features.Jobs.Commands.CreateJob;
using Business.Application.Features.Jobs.Commands.DeleteJob;
using Business.Application.Features.Jobs.Commands.UpdateJob;
using Business.Application.Features.Jobs.Queries.GetJobDetail;
using Business.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Business.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobController : Controller
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("JobDetail/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetJobDetailDTO>> GetJob(int id)
        {
            var query = new GetJobDetailQuery{ Id = id };
            var jobDetail = await _mediator.Send(query);

            return Ok(jobDetail);
        }

        [HttpPost(Name = "AddJob")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateJobDTO jobDTO)
        {
            var command = new CreateJobCommand { JobDTO = jobDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}", Name = "UpdateJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateJobDTO jobDTO)
        {
            jobDTO.Id = id;
            var command = new UpdateJobCommand { JobDTO = jobDTO };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteJobCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
