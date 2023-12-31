using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.DTOs.LanguageOfResume;
using Candidate.Application.DTOs.Resume;
using Candidate.Application.DTOs.SkillOfResume;
using Candidate.Application.Exceptions;
using Candidate.Application.GrpcServices;
using Candidate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Queries.GetResume
{
    public class GetResumeQueryHandler :IRequestHandler<GetResumeQuery, ResumeDTO>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;
        private readonly CareerGrpcService _careerGrpcService;
        private readonly SkillGrpcService _skillGrpcService;
        private readonly LanguageGrpcService _languageGrpcService;

        public GetResumeQueryHandler(IResumeRepository resumeRepository, IMapper mapper,
            CareerGrpcService careerGrpcService, SkillGrpcService skillGrpcService, LanguageGrpcService languageGrpcService)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
            _careerGrpcService = careerGrpcService;
            _skillGrpcService = skillGrpcService;
            _languageGrpcService = languageGrpcService;
        }

        public async Task<ResumeDTO> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            var resume = await _resumeRepository.FindResumeById(request.ResumeId);
            if (resume == null)
            {
                throw new NotFoundException(nameof(Resume), request.ResumeId);
            }
            var career = await _careerGrpcService.GetCareer(resume.CareerId);

            var resumeDTO = _mapper.Map<ResumeDTO>(resume);
            resumeDTO.NameCareer = career.Name;
            resumeDTO.SkillOfResumeDTOs = new List<SkillOfResumeDTO>();

            foreach (var skill in resume.Skills)
            {
                var skillDTO = _mapper.Map<SkillOfResumeDTO>(skill);
                var foundSkill = await _skillGrpcService.GetSkill(skill.SkillId);
                skillDTO.SkillName = foundSkill.NameSkill;
                resumeDTO.SkillOfResumeDTOs.Add(skillDTO);
            }
            resumeDTO.languageOfResumeDTOs = new List<LanguageOfResumeDTO>();

            foreach (var language in resume.Languages)
            {
                var languageDTO = _mapper.Map<LanguageOfResumeDTO>(language);
                var foundLanguage = await _languageGrpcService.GetLanguage(language.LanguageId);
                languageDTO.LanguageName = foundLanguage.LanguageName;
                languageDTO.Level = foundLanguage.Level;
                resumeDTO.languageOfResumeDTOs.Add(languageDTO);
            }
            return resumeDTO;
        }
    }
}
