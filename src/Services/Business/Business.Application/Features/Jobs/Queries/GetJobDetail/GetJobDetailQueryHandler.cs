using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.Job;
using Business.Application.Exceptions;
using Business.Application.GrpcServices;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetJobDetail
{
    public class GetJobDetailQueryHandler : IRequestHandler<GetJobDetailQuery, GetJobDetailDTO>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly LanguageGrpcService _languageGrpcService;
        private readonly SkillGrpcService _skillGrpcService;
        private readonly CareerGrpcService _careerGrpcService;

        public GetJobDetailQueryHandler(IJobRepository jobRepository, IMapper mapper,
            SkillGrpcService skillGrpcService, CareerGrpcService careerGrpcService, LanguageGrpcService languageGrpcService)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _skillGrpcService = skillGrpcService;
            _careerGrpcService = careerGrpcService;
            _languageGrpcService = languageGrpcService;
        }

        public async Task<GetJobDetailDTO> Handle(GetJobDetailQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetById(request.Id);
            if (job == null)
            {
                throw new NotFoundException(nameof(Job), request.Id);
            }

            var jobDTO = _mapper.Map<GetJobDetailDTO>(job);
            var carrer = await _careerGrpcService.GetCareer(job.CareerId);
            var language = await _languageGrpcService.GetLanguage(job.LanguageRequirementId);
            jobDTO.CareerName = carrer.Name;
            jobDTO.LanguageRequirementName = language.LanguageName;
            jobDTO.LanguageRequirementLevel = language.Level;
            jobDTO.AllRequiredSkills = new List<string>();
            string[] requiredSkills = job.RequiredSkills.Split(";", StringSplitOptions.RemoveEmptyEntries);
            foreach (string skillIdString in requiredSkills)
            {
                if (int.TryParse(skillIdString, out int idSkill))
                {
                    var skill = await _skillGrpcService.GetSkill(idSkill);
                    jobDTO.AllRequiredSkills.Add(skill.NameSkill);
                }
                else
                {
                    throw new ArgumentException("An error during adding skill name arised");
                }
            }

            return jobDTO;
        }
    }
}
