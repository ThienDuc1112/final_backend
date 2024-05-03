using Application.API.GrpcServices;
using Application.Domain.Common;
using Application.Domain.DTOs.AppliedJob;
using Application.Domain.Entities;
using Application.Infrastructure.Repositories.Abstraction;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppliedJobController : Controller
    {
        private readonly IAppliedJobRepository _appliedJobRepository;
        private readonly IMapper _mapper;
        private readonly ResumeGrpcService _resumeGrpcService;
        private readonly JobGrpcService _jobGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;

        public AppliedJobController(IAppliedJobRepository appliedJobRepository, IMapper mapper, ResumeGrpcService resumeGrpcService,
            JobGrpcService jobGrpcService, IPublishEndpoint publishEndpoint)
        {
            _appliedJobRepository = appliedJobRepository;
            _mapper = mapper;
            _resumeGrpcService = resumeGrpcService;
            _jobGrpcService = jobGrpcService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetAppDetailDTO>> GetDetail(int id)
        {
            var app = await _appliedJobRepository.GetAppliedJobDetailDTO(id);
            if (app == null)
            {
                return NotFound();
            }
            var appDTO = _mapper.Map<GetAppDetailDTO>(app);
            appDTO.JobId = app.JobId;

            var resume = await _resumeGrpcService.GetResume(app.ResumeId);
            appDTO.FullName = resume.FullName;
            appDTO.AvatarUrl = resume.AvatarUrl;
            appDTO.Title = resume.Title;
            appDTO.Email = resume.Email;

            var job = await _jobGrpcService.GetJob(app.JobId);
            appDTO.JobTitle = job.Title;
            appDTO.NumberRecruitment = job.NumberRecruitment;
            appDTO.BusinessName = job.BusinessName;
            appDTO.LogoUrl = job.AvatarUrl;

            return appDTO;
        }

        [HttpGet("GetAppsUser/{candidateId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<GetApplicationList>>> GetApplicationList(string candidateId)
        {
            var apps = await _appliedJobRepository.GetApplicationList(candidateId);
            if (apps == null)
            {
                return NotFound();
            }

            var jobTasks = apps.Select(job => _jobGrpcService.GetJob(job.JobId)).ToList();
            var jobs = await Task.WhenAll(jobTasks);
            for (int i = 0; i < jobs.Length; i++)
            {
                var job = jobs[i];
                apps[i].BusinessName = job.BusinessName;
                apps[i].LogoUrl = job.AvatarUrl;
                apps[i].Title = job.Title;
            }

            return Ok(apps);
        }

        [HttpGet("GetApplications")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<GetAppliedJobDTO>>> Get([FromQuery(Name = "jobId")] int jobId, [FromQuery(Name = "status")] string? status)
        {
            var applications = await _appliedJobRepository.GetAppliedJob(jobId, status);
            if (applications == null)
            {
                return NotFound();
            }

            var appDTOs = _mapper.Map<List<GetAppliedJobDTO>>(applications);

            var resumeTasks = applications.Select(app => _resumeGrpcService.GetResume(app.ResumeId)).ToList();
            var resumes = await Task.WhenAll(resumeTasks);

            for (int i = 0; i < applications.Count; i++)
            {
                var resume = resumes[i];
                appDTOs[i].FullName = resume.FullName;
                appDTOs[i].AvatarUrl = resume.AvatarUrl;
                appDTOs[i].Title = resume.Title;
            }

            return Ok(appDTOs);
        }

        [HttpGet("GetInterviewCandidate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<GetInterviewCandidate>>> GetInterviewCandidate([FromQuery(Name = "jobId")] int? jobId)
        {
            var interview = await _appliedJobRepository.GetInterviewCandidate(jobId);
            if (interview == null)
            {
                return NotFound();
            }

            var resumeTasks = interview.Select(app => _resumeGrpcService.GetResume(app.ResumeId)).ToList();
            var resumes = await Task.WhenAll(resumeTasks);
            for (int i = 0; i < interview.Count; i++)
            {
                var resume = resumes[i];
                interview[i].CandidateName = resume.FullName;
                interview[i].CandidateTitle = resume.Title;
                interview[i].CandidateAvatar = resume.AvatarUrl;
            }

            return interview;
        }

        [HttpGet("GetDashboardApplications/{businessId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetAppliedJobDashboard>> GetDashboardApplications(int businessId)
        {
            var rawJobs = await _appliedJobRepository.GetAppliedJobDashboard(businessId);
            if (rawJobs == null)
            {
                return NotFound();
            }
            var apps = new GetAppliedJobDashboard();
            apps.ApplicationCount = rawJobs.ApplicationCount;
            apps.InterviewCount = rawJobs.InterviewCount;

            var resumeTasks = rawJobs.Jobs.Select(app => _resumeGrpcService.GetResume(app.ResumeId)).ToList();
            var resumes = await Task.WhenAll(resumeTasks);


            for (int i = 0; i < rawJobs.Jobs.Count; i++)
            {
                var app = new GetAppliedJobDTO();
                var resume = resumes[i];
                app.FullName = resume.FullName;
                app.AvatarUrl = resume.AvatarUrl;
                app.Title = resume.Title;
                app.CreatedDate = rawJobs.Jobs[i].CreatedDate;
                app.Id = rawJobs.Jobs[i].Id;
                app.CreatedDate = rawJobs.Jobs[i].CreatedDate;
                app.Status = rawJobs.Jobs[i].Status;
                app.ResumeId = rawJobs.Jobs[i].ResumeId;
                apps.Jobs.Add(app);
            }

            return apps;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateAppliedJobDTO appliedJobDTO)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            if (!ModelState.IsValid)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Created failed";
                response.Errors = ModelState.Values
                    .SelectMany(e => e.Errors.Select(er => er.ErrorMessage)).ToList();
                return BadRequest(response);
            }
            else
            {
                if (!await _appliedJobRepository.IsExisted(appliedJobDTO.JobId, appliedJobDTO.CandidateId))
                {
                    response.Id = 1;
                    response.Success = true;
                    response.Message = "Applied successfully";
                    var appliedJob = _mapper.Map<AppliedJob>(appliedJobDTO);
                    appliedJob.Status = "Pending";
                    await _appliedJobRepository.Add(appliedJob);
                    var job = await _jobGrpcService.GetJob(appliedJob.JobId);
                    var resume = await _resumeGrpcService.GetResume(appliedJob.ResumeId);
                    var message = new SendingMessageEvent
                    {
                        ApplicationId = appliedJob.Id,
                        BusinessName = job.BusinessName,
                        Title = job.Title,
                        FullName = resume.FullName,
                        Type = "Applying",
                        UserId = appliedJobDTO.BusinessUserId,
                    };
                    await _publishEndpoint.Publish(message);
                    return Ok(response);
                }
                else
                {

                    response.Id = 0;
                    response.Success = false;
                    response.Message = "You have already applied to this job";
                    return Ok(response);

                }

            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] UpdateAppliedJobDTO appliedJobDTO)
        {
            var response = new BaseCommandResponse();
            if (!ModelState.IsValid)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Created failed";
                response.Errors = ModelState.Values
                    .SelectMany(e => e.Errors.Select(er => er.ErrorMessage)).ToList();
                return BadRequest(response);
            }
            else
            {
                var app = await _appliedJobRepository.GetById(appliedJobDTO.Id);
                if (app == null)
                {
                    response.Id = 0;
                    response.Success = false;
                    response.Message = "Can not find this application";
                }
                else
                {
                    _mapper.Map(appliedJobDTO, app);
                    await _appliedJobRepository.Update(app);
                    response.Id = 1;
                    response.Success = true;
                    response.Message = "Updating job application succcesfully";

                    var job = await _jobGrpcService.GetJob(app.JobId);
                    var message = new SendingMessageEvent
                    {
                        ApplicationId = app.Id,
                        BusinessName = job.BusinessName,
                        Title = job.Title,
                        UserId = app.CandidateId,
                        Type = appliedJobDTO.Status,
                        FullName = "user"
                    };

                    await _publishEndpoint.Publish(message);
                }
            }
            return Ok(response);
        }

        [HttpPut("UpdatingMeeting")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseCommandResponse>> UpdateMeeting([FromBody] UpdateMeetingUrlApp appliedJobDTO)
        {
            var response = new BaseCommandResponse();
            if (!ModelState.IsValid)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Created failed";
                response.Errors = ModelState.Values
                    .SelectMany(e => e.Errors.Select(er => er.ErrorMessage)).ToList();
                return BadRequest(response);
            }
            else
            {
                var app = await _appliedJobRepository.GetById(appliedJobDTO.Id);
                if (app == null)
                {
                    response.Id = 0;
                    response.Success = false;
                    response.Message = "Can not find this application";
                }
                else
                {
                    _mapper.Map(appliedJobDTO, app);
                    await _appliedJobRepository.Update(app);
                    response.Id = 1;
                    response.Success = true;
                    response.Message = "Adding meeting url succcesfully";
                }
            }
            return Ok(response);
        }
    }
}
