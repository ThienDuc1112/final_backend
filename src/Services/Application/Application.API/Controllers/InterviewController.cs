using Application.Domain.Common;
using Application.Domain.DTOs.AppliedJob;
using Application.Domain.DTOs.InterviewSchedule;
using Application.Domain.Entities;
using Application.Infrastructure.Repositories.Abstraction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InterviewController : Controller
    {
        private readonly IInterviewScheduleRepository _interviewScheduleRepository;
        private readonly IMapper _mapper;

        public InterviewController(IInterviewScheduleRepository interviewScheduleRepository, IMapper mapper)
        {
            _interviewScheduleRepository = interviewScheduleRepository;
            _mapper = mapper;
        }

        [HttpGet("{applicationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<GetInterviewDTO>>> Get(int applicationId)
        {
            var interviews = await _interviewScheduleRepository.GetAll();
            if (interviews == null)
            {
                return NotFound();
            }
            var interviewDTOs = _mapper.Map<List<GetInterviewDTO>>(interviews);
            return View(interviewDTOs);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromBody] CreateInterviewDTO createInterviewDTO)
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
                response.Id = 1;
                response.Success = true;
                response.Message = "Created interview schedule successfully";

                var interview = _mapper.Map<InterviewSchedule>(createInterviewDTO);
                interview.IsSelected = false;
                await _interviewScheduleRepository.Add(interview);
            }
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] UpdateInterviewDTO updateInterviewDTO)
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
                var interview = await _interviewScheduleRepository.GetById(updateInterviewDTO.Id);

                if (interview == null)
                {
                    response.Id = 0;
                    response.Success = false;
                    response.Message = "Can not find this interview";
                }
                else
                {
                    _mapper.Map(updateInterviewDTO, interview);
                    await _interviewScheduleRepository.Update(interview);
                    response.Id = 1;
                    response.Success = true;
                    response.Message = "Updating interview succesfully";
                }
            }
            return Ok(response);
        }
    }
}
