using Application.API.GrpcServices;
using Application.Domain.Common;
using Application.Domain.DTOs.AppliedJob;
using Application.Domain.Entities;
using Application.Infrastructure.Repositories.Abstraction;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppliedJobController : ControllerBase
    {
        private readonly IAppliedJobRepository _appliedJobRepository;
        private readonly IMapper _mapper;
        private readonly ResumeGrpcService _resumeGrpcService;
        private readonly JobGrpcService _jobGrpcService;

      public AppliedJobController(IAppliedJobRepository appliedJobRepository, IMapper mapper, ResumeGrpcService resumeGrpcService, JobGrpcService jobGrpcService)
        {
            _appliedJobRepository = appliedJobRepository;
            _mapper = mapper;
            _resumeGrpcService = resumeGrpcService;
            _jobGrpcService = jobGrpcService;
        }

        [HttpGet("GetApplication/{id}")]
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

            var resume = await _resumeGrpcService.GetResume(app.JobId);
            appDTO.FullName = resume.FullName;
            appDTO.AvatarUrl = resume.AvatarUrl;
            appDTO.Title = resume.Title;
            appDTO.Email = resume.Email;

            var job = await _jobGrpcService.GetJob(app.JobId);
            appDTO.JobTitle = job.Title;
            appDTO.NumberRecruitment = job.NumberRecruitment;

            return appDTO;
        }

        [HttpGet("{jobId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<GetAppliedJobDTO>>> Get(int jobId)
        {
            var applications = await _appliedJobRepository.GetAppliedJob(jobId);
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
                if (await _appliedJobRepository.IsExisted(appliedJobDTO.JobId, appliedJobDTO.CandidateId))
                {
                    response.Id = 0;
                    response.Success = false;
                    response.Message = "You have already applied to this job";
                    
                }
                else
                {
                    response.Id = 1;
                    response.Success = true;
                    response.Message = "Applied successfully";
                    var appliedJob = _mapper.Map<AppliedJob>(appliedJobDTO);
                    appliedJob.Status = "Pending";
                    await _appliedJobRepository.Add(appliedJob);
                }
                return Ok(response);
            }
        }
    }
}
