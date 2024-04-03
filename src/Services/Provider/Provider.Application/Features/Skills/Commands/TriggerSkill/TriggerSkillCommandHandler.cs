using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using Provider.Application.Features.Skills.Commands.UpdateSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.TriggerSkill
{
    public class TriggerSkillCommandHandler : IRequestHandler<TriggerSkillCommand, Unit>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public TriggerSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(TriggerSkillCommand request, CancellationToken cancellationToken)
        {
            var validator = new TriggerSkillValidator();
            var validationResult = await validator.ValidateAsync(request.TriggerSkillDTO);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var skill = await _skillRepository.GetById(request.TriggerSkillDTO.Id);

            _mapper.Map(request.TriggerSkillDTO, skill);

            await _skillRepository.Update(skill);

            return Unit.Value;
        }
    }
}
