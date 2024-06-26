﻿using Business.Application.DTOs.Job;
using Business.Application.Features.Jobs.Commands.CreateJob;
using Business.Application.Features.Jobs.Commands.DeleteJob;
using Business.Application.Features.Jobs.Commands.UpdateJob;
using Business.Application.Features.Jobs.Queries.GetJobDetail;
using Business.Application.Features.Jobs.Queries.GetJobManagement;
using Business.Application.Features.Jobs.Queries.GetListJob;
using Business.Application.Features.Jobs.Queries.GetListJobApp;
using Business.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Business.Application.Features.Jobs.Queries.GetJobsFromBusiness;
using Business.Application.Features.Jobs.Queries.GetDashBoardJob;
using Business.Application.Features.Jobs.Queries.GetNewJob;

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
        public async Task<ActionResult<GetListJobDTO>> GetListJob([FromQuery(Name = "page")] int? page, [FromQuery(Name = "query")] string? query,
            [FromQuery(Name = "jobType")] string? jobType, [FromQuery(Name = "minSalary")] int? minSalary, [FromQuery(Name = "maxSalary")] int? maxSalary,
            [FromQuery(Name = "career")] int career, [FromQuery(Name = "experience")] string? experience, [FromQuery(Name = "date")] string? date,
            [FromQuery(Name = "position")] string? position, [FromQuery(Name = "education")] string? education)
        {

            var getJobsQuery = new GetListJobQuery
            {
                Page = page,
                Query = query,
                JobType = jobType,
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Career = career,
                Experience = JsonConvert.DeserializeObject<List<string>>(experience),
                Date = date,
                Position = JsonConvert.DeserializeObject<List<string>>(position),
                Education = JsonConvert.DeserializeObject<List<string>>(education),
            };

            var jobs = await _mediator.Send(getJobsQuery);
            return Ok(jobs);
        }

        [HttpGet("GetNewJobs")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetJobDTO>>> GetNewJobs()
        {
            var query = new GetNewJobQuery();
            var jobs = await _mediator.Send(query);
            if(jobs == null)
            {
                return NotFound();
            }
            return jobs;
        }

        [HttpGet("GetJobsByBusiness/{businessId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetJobDTO>>> GetJobByBusiness( int businessId)
        {
            var query = new GetJobFromBusinessQuery
            {
                BusinessId = businessId
            };
            var jobs = await _mediator.Send(query);

            return Ok(jobs);
        }

        [HttpGet("GetJobManagement")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetJobManagementListDTO>> GetJobManagement([FromQuery(Name = "page")] int? page, [FromQuery(Name = "businessId")] int businessId)
        {
            var getJobsQuery = new GetJobManagementQuery
            {
                BusinessId = businessId,
                Page = page
            };

            var jobs = await _mediator.Send(getJobsQuery);
            return Ok(jobs);
        }
        [HttpGet("GetJobApp")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetJobAppDTO>>> GetJobApp([FromQuery(Name = "businessId")] int businessId)
        {
            var getJobsQuery = new GetListJobAppQuery
            {
                BusinessId = businessId,
            };

            var jobs = await _mediator.Send(getJobsQuery);
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetJobDetailDTO>> GetJob(int id)
        {
            var query = new GetJobDetailQuery { Id = id };
            var jobDetail = await _mediator.Send(query);

            return Ok(jobDetail);
        }
        [HttpGet("GetDashboardJob/{businessId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetJobDetailDTO>> GetDashboardJob(int businessId)
        {
            var query = new GetDashBoardJobQuery { BusinessId = businessId };
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
