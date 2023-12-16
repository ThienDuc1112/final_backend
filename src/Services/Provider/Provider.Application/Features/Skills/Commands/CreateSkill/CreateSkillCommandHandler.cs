using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Features.Careers.Commands.CreateCareer;
using Provider.Application.Responses;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.CreateSkill
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, BaseCommandResponse>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;

        public CreateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper, ICareerRepository careerRepository)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
            _careerRepository = careerRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSkillValidator(_careerRepository);
            var validationResult = await validator.ValidateAsync(request.SkillDTO);

            if (await _skillRepository.IsExisted(request.SkillDTO.NameSkill))
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors.Add("This skill was already existed");
            }
            else if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var skill = _mapper.Map<Skill>(request.SkillDTO);
                skill.CareerId = request.SkillDTO.CareerId;
                skill = await _skillRepository.Add(skill);
                skill.Career = await _careerRepository.GetById(request.SkillDTO.CareerId);
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = skill.Id;
            }
            return response;
        }
    }
    
}
