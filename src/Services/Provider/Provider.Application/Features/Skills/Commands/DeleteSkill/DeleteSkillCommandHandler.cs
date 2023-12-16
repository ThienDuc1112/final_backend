using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using Provider.Application.Features.Careers.Commands.DeleteCareer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public DeleteSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var deletedSkill = await _skillRepository.GetById(request.Id);
            if (deletedSkill == null)
            {
                throw new NotFoundException(nameof(deletedSkill), request.Id);
            }

            await _skillRepository.Delete(deletedSkill);

            return Unit.Value;
        }
    }
}
