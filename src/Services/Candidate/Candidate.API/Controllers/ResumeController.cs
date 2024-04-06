using Candidate.Application.Contracts.Persistence;
using Candidate.Application.DTOs.Education;
using Candidate.Application.DTOs.Experience;
using Candidate.Application.DTOs.LanguageOfResume;
using Candidate.Application.DTOs.Resume;
using Candidate.Application.DTOs.SkillOfResume;
using Candidate.Application.Features.Educations.Commands.CreateEducation;
using Candidate.Application.Features.Educations.Commands.UpdateEducation;
using Candidate.Application.Features.Experiences.Commands.CreateExperience;
using Candidate.Application.Features.Experiences.Commands.UpdateExperience;
using Candidate.Application.Features.LanguagesOfResume.Commands.CreateLanguageOfResume;
using Candidate.Application.Features.LanguagesOfResume.Commands.UpdateLanguageOfResume;
using Candidate.Application.Features.Resumes.Commands.CreateResume;
using Candidate.Application.Features.Resumes.Commands.HideResume;
using Candidate.Application.Features.Resumes.Commands.UpdateResume;
using Candidate.Application.Features.Resumes.Queries.GetResume;
using Candidate.Application.Features.Resumes.Queries.GetResumeByUser;
using Candidate.Application.Features.SkillsOfResume.CreateSkillOfResume;
using Candidate.Application.Features.SkillsOfResume.UpdateSkillOfResume;
using Candidate.Application.Responses;
using Candidate.Domain.Entities;
using Candidate.Infrastructure.Persistence;
using Candidate.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Candidate.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ResumeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly CandidateDbContext _dbcontext;
        private readonly IEducationRepository _educationRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly ISkillOfResumeRepository _skillOfResumeRepository;
        private readonly ILanguageOfResumeRepository _languageOfResumeRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ResumeController(IMediator mediator, CandidateDbContext dbcontext, IServiceScopeFactory serviceScopeFactory,
        IExperienceRepository experienceRepository, ISkillOfResumeRepository skillOfResumeRepository,
            ILanguageOfResumeRepository languageOfResumeRepository, IEducationRepository educationRepository)
        {
            _mediator = mediator;
            _dbcontext = dbcontext;
            _experienceRepository = experienceRepository;
            _skillOfResumeRepository = skillOfResumeRepository;
            _languageOfResumeRepository = languageOfResumeRepository;
            _educationRepository = educationRepository;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet("ViewResume/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResumeDTO>> GetResume(int id)
        {
            var query = new GetResumeQuery { ResumeId = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetResume/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResumeDTO>> GetResumeByUser(string userId)
        {
            var query = new GetResumeByUserQuery { UserId = userId };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("DesignYourResume", Name = "DesignResume")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseCommandResponse>> Design([FromBody] ResumeWithRelatedDTO dto)
        {
            List<BaseCommandResponse> reponseList = new List<BaseCommandResponse>();
                await using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Create Resume
                        var resumeCommand = new CreateResumeCommand { ResumeDTO = dto.Resume };
                        var resumeResponse = await _mediator.Send(resumeCommand);
                        var resumeId = resumeResponse.Id;
                        reponseList.Add(resumeResponse);

                        if (resumeId == 0)
                        {
                            return resumeResponse;
                        }
                        foreach (var experience in dto.Experiences)
                        {
                            experience.ResumeId = resumeId;
                            var experienceCmd = new CreateExperienceCommand { ExperienceDTO = experience };
                            var res = await _mediator.Send(experienceCmd);
                            reponseList.Add(res);
                        }

                        foreach (var education in dto.Educations)
                        {
                            education.ResumeId = resumeId;
                            var educationCmd = new CreateEducationCommand { EducationDTO = education };
                            var res = await _mediator.Send(educationCmd);
                            reponseList.Add(res);
                        }

                        foreach (var skill in dto.SkillOfResumes)
                        {
                          skill.ResumeId = resumeId;
                          var skillCmd = new CreateSkillOfResumeCommand { SkillOfResumeDTO = skill };
                          var res = await _mediator.Send(skillCmd);
                          reponseList.Add(res);
                        }

                        foreach (var language in dto.LanguageOfResumes)
                        {
                            language.ResumeId = resumeId;
                            var languageCmd = new CreateLanguageOfResumeCommand { LanguageOfResumeDTO = language };
                            var res = await _mediator.Send(languageCmd);
                            reponseList.Add(res);
                        }

                        await transaction.CommitAsync(); // Commit the transaction

                    return Ok(reponseList);

                }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); // Rollback the transaction
                        throw; // Re-throw the exception
                    }
                }               
        }

        [HttpPut("UpdateResume/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] UpdateResumeWithRelatedDTO dto)
        {
            using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var updateResumeCommand = new UpdateResumeCommand { ResumeDTO = dto.Resume };
                    var res = await _mediator.Send(updateResumeCommand);
                    if (!res.Success)
                    {
                        return BadRequest(res);
                    }
                    await UpdateExperiences(dto.Experiences, res.Id);
                    await UpdateEducations(dto.Educations, res.Id);
                    await UpdateSkillsOfResume(dto.SkillOfResumes, res.Id);
                    await UpdateLanguagesOfResume(dto.LanguageOfResumes, res.Id);
                    await transaction.CommitAsync();
                    
                    return Ok(res);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An error occurred while updating the resume.", ex);
                }
            }
         }

        private async Task UpdateLanguagesOfResume(List<UpdateLanguageOfResumeDTO> languages, int id)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CandidateDbContext>();
                foreach (var language in languages)
                {
                    if (language.Id != 0)
                    {
                        var updateLanguageCommand = new UpdateLanguageOfResumeCommand { LanguageOfResumeDTO = language };
                        await _mediator.Send(updateLanguageCommand);
                    }
                    else
                    {
                        var lang = new CreateLanguageOfResumeDTO
                        {
                            ResumeId = id,
                            LanguageId = language.LanguageId
                        };
                        var createLanguageCommand = new CreateLanguageOfResumeCommand { LanguageOfResumeDTO = lang };
                        await _mediator.Send(createLanguageCommand);
                    }
                }
            }
        }

        private async Task UpdateSkillsOfResume(List<UpdateSkillOfResumeDTO> skills, int id)
        {
                foreach (var skill in skills)
                {
                    if (skill.Id != 0)
                    {
                        var updateSkillCommand = new UpdateSkillOfResumeCommand { SkillOfResumeDTO = skill };
                        await _mediator.Send(updateSkillCommand);
                    }
                    else
                    {
                        var sk = new CreateSkillOfResumeDTO
                        {
                            ResumeId = id,
                            SkillId = skill.SkillId,
                        };
                        var cmd = new CreateSkillOfResumeCommand { SkillOfResumeDTO = sk };
                        await _mediator.Send(cmd);
                    }
                }
            
        }

        private async Task UpdateEducations(List<UpdateEducationDTO> educations, int id)
        {
            foreach(var education in educations)
            {
                if (await _educationRepository.Exists(education.Id))
                {
                    var updateEducationCommand = new UpdateEducationCommand { EducationDTO = education };
                    await _mediator.Send(updateEducationCommand);
                }
                else
                {
                    var edu = new CreateEducationDTO
                    {
                        ResumeId = id,
                        UniversityName = education.UniversityName,
                        StartDate = education.StartDate,
                        EndDate = education.EndDate,
                        Degree = education.Degree,
                        Major = education.Major
                    };
                    var cmd = new CreateEducationCommand { EducationDTO = edu };
                    await _mediator.Send(cmd);
                }
            }
        }

        private async Task UpdateExperiences(List<UpdateExperienceDTO> experiences, int resumeId)
        {
            foreach(var experience in experiences)
            {
                if (await _experienceRepository.Exists(experience.Id))
                {
                    var updateExperienceCommand = new UpdateExperienceCommand { ExperienceDTO = experience };
                    await _mediator.Send(updateExperienceCommand);
                }
                else
                {
                    var Ex = new CreateExperienceDTO
                    {
                        ResumeId = resumeId,
                        EndDate = experience.EndDate,
                        StartDate = experience.StartDate,
                        Company = experience.Company,
                        Title = experience.Title,
                        Responsibility = experience.Responsibility
                    };
                    var cmd = new CreateExperienceCommand { ExperienceDTO = Ex };
                    await _mediator.Send(cmd);
                }
            }
        }

        [HttpPut("HideResume")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseCommandResponse>> Hide([FromBody] HideResumeDTO dto)
        {
            var command = new HideResumeCommand { HideResumeDTO = dto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
