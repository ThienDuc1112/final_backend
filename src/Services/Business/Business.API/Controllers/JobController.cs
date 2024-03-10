using Business.Application.DTOs.Job;
using Business.Application.Features.Jobs.Commands.CreateJob;
using Business.Application.Features.Jobs.Commands.DeleteJob;
using Business.Application.Features.Jobs.Commands.UpdateJob;
using Business.Application.Features.Jobs.Queries.GetJobDetail;
using Business.Application.Features.Jobs.Queries.GetJobManagement;
using Business.Application.Features.Jobs.Queries.GetListJob;
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
        private readonly ILogger<JobController> _logger;

        public JobController(IMediator mediator, ILogger<JobController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetListJob")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetJobDTO>>> GetListJob([FromQuery(Name = "page")] int? page, [FromQuery(Name = "Query")] string? query,
            [FromQuery(Name = "JobType")] string? jobType, [FromQuery(Name = "MinSalary")] int? minSalary, [FromQuery(Name = "MaxSalary")] int? maxSalary,
            [FromQuery(Name = "Career")] int? career, [FromQuery(Name = "Experience")] List<string>? experience, [FromQuery(Name = "Date")] string? date,
            [FromQuery(Name = "Position")] List<string>? position, [FromQuery(Name = "Education")] List<string>? education)
        {
            _logger.LogWarning($"Received query: Query={query}, page={page}");

            var getJobsQuery = new GetListJobQuery
            {
                Page = page,
                Query = query,
                JobType = jobType,
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Career = career,
                Experience = experience,
                Date = date,
                Position = position,
                Education = education
            };

            var jobs = await _mediator.Send(getJobsQuery);
            return Ok(jobs);
        }

        [HttpGet("GetJobManagement")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetJobManagementDTO>>> GetJobManagement([FromQuery(Name = "page")] int? page, [FromQuery(Name = "businessId")] int businessId)
        {
            var getJobsQuery = new GetJobManagementQuery
            {
                BusinessId = businessId,
                Page = page
            };

            var jobs = await _mediator.Send(getJobsQuery);
            return Ok(jobs);
        }

        [HttpGet("{id}")]
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

        [HttpPut(Name = "UpdateJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] UpdateJobDTO jobDTO)
        {
            var command = new UpdateJobCommand { JobDTO = jobDTO };
            var response = await _mediator.Send(command);
            return Ok(response);
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
