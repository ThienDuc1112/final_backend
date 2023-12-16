using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using Provider.Application.Features.Careers.Commands.UpdateCareer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, Unit>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public UpdateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSkillValidator();
            var validationResult = await validator.ValidateAsync(request.skillDTO);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var skill = await _skillRepository.GetById(request.skillDTO.Id);

            _mapper.Map(request.skillDTO, skill);

            await _skillRepository.Update(skill);

            return Unit.Value;
        }
    }
    
}
