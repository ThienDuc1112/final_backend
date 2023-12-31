using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using Candidate.Domain.Entities;
using MediatR;


namespace Candidate.Application.Features.SkillsOfResume.UpdateSkillOfResume
{
    public class UpdateSkillOfResumeCommandHandler : IRequestHandler<UpdateSkillOfResumeCommand, Unit>
    {
        private readonly ISkillOfResumeRepository _skillOfResumeRepository;
        private readonly IMapper _mapper;

        public UpdateSkillOfResumeCommandHandler(ISkillOfResumeRepository skillOfResumeRepository, IMapper mapper)
        {
            _skillOfResumeRepository = skillOfResumeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSkillOfResumeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSkillOfResumeValidator();
            var validationResult = await validator.ValidateAsync(request.SkillOfResumeDTO);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var skillOfResume = await _skillOfResumeRepository.GetById(request.SkillOfResumeDTO.Id);
            if (skillOfResume == null)
            {
                throw new NotFoundException(nameof(SkillOfResume), request.SkillOfResumeDTO.Id);
            }

            _mapper.Map(request.SkillOfResumeDTO, skillOfResume);

            await _skillOfResumeRepository.Update(skillOfResume);

            return Unit.Value;
        }
    }
}

