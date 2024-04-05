using Application.API.GrpcServices;
using Application.Domain.DTOs.FavoriteJob;
using Application.Domain.Entities;
using Application.Infrastructure.Repositories.Abstraction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FavoriteJobController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteJobRepository _favoriteJobRepository;
        private readonly JobGrpcService _jobGrpcService;

        public FavoriteJobController(IMapper mapper, IFavoriteJobRepository favoriteJobRepository, JobGrpcService jobGrpcService)
        {
            _mapper = mapper;
            _favoriteJobRepository = favoriteJobRepository;
            _jobGrpcService = jobGrpcService;
        }

        [HttpGet("{candidateId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<GetFavoriteJobDTO>>> GetJobs(string candidateId)
        {
            var jobs = await _favoriteJobRepository.GetFavoriteJobs(candidateId);
            if(jobs == null)
            {
                return NotFound();
            }

            var jobTasks = jobs.Select(task => _jobGrpcService.GetJob(task.JobId)).ToList();
            var result = await Task.WhenAll(jobTasks);

            var favoriteJobs = _mapper.Map<List<GetFavoriteJobDTO>>(jobs);
            for(int i = 0; i < favoriteJobs.Count; i++)
            {
                var job = favoriteJobs[i];
                favoriteJobs[i].BusinessId = result[i].BusinessId;
                favoriteJobs[i].BusinessName = result[i].BusinessName;
                favoriteJobs[i].LogoUrl = result[i].AvatarUrl;
                favoriteJobs[i].JobName = result[i].Title;
                favoriteJobs[i].ExpiredDate = DateTime.Parse(result[i].ExpirateDate);
            }

            return Ok(favoriteJobs);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> SavedJob([FromBody] CreateFavoriteJobDTO jobDTO)
        {
            if (ModelState.IsValid)
            {
                if (await _favoriteJobRepository.IsExisted(jobDTO.CandidateId, jobDTO.JobId))
                {
                    return Ok(new { message = "failed" });
                }
                else
                {
                    var job = _mapper.Map<FavoriteJob>(jobDTO);
                    await _favoriteJobRepository.Add(job);
                    return Ok(new { message = "success" });
                }
            }
            else
            {
                return Ok(new { message = "invalid model" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteJob(int id)
        {
            var job = await _favoriteJobRepository.GetById(id);
            if(job == null)
            {
                return Ok(new { message = "failed" });
            }
            else
            {
                await _favoriteJobRepository.Delete(job);
                return Ok(new { message = "success" });
            }
        }
    }
}
