using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Responses;
using Candidate.Domain.Entities;
using MediatR;


namespace Candidate.Application.Features.SkillsOfResume.CreateSkillOfResume
{
    public class CreateSkillOfResumeCommandHandler : IRequestHandler<CreateSkillOfResumeCommand, BaseCommandResponse>
    {
        private readonly ISkillOfResumeRepository _skillOfResumeRepository;
        private readonly IMapper _mapper;

        public CreateSkillOfResumeCommandHandler(ISkillOfResumeRepository skillOfResumeRepository, IMapper mapper)
        {
            _skillOfResumeRepository = skillOfResumeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSkillOfResumeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSkillOfResumeValidator();
            var validationResult = await validator.ValidateAsync(request.SkillOfResumeDTO);

            if (await _skillOfResumeRepository.IsExistedSkill(request.SkillOfResumeDTO.SkillId, request.SkillOfResumeDTO.ResumeId))
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors.Add("This skill was already existed");
            }

            if (validationResult.IsValid == false)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var skillOfResume = _mapper.Map<SkillOfResume>(request.SkillOfResumeDTO);
                await _skillOfResumeRepository.Add(skillOfResume);
                response.Id = skillOfResume.Id;
                response.Success = true;
                response.Message = "Creation of skill of resume successful";
            }

            return response;
        }
    }
}
