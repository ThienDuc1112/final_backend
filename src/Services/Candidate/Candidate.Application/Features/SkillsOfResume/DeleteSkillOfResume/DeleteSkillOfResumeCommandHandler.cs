using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using Candidate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.SkillsOfResume.DeleteSkillOfResume
{
    public class DeleteSkillOfResumeCommandHandler : IRequestHandler<DeleteSkillOfResumeCommand, Unit>
    {
        private readonly ISkillOfResumeRepository _skillOfResumeRepository;

        public DeleteSkillOfResumeCommandHandler(ISkillOfResumeRepository skillOfResumeRepository)
        {
            _skillOfResumeRepository = skillOfResumeRepository;
        }

        public async Task<Unit> Handle(DeleteSkillOfResumeCommand request, CancellationToken cancellationToken)
        {
            var deletedSkillOfResume = await _skillOfResumeRepository.GetById(request.Id);
            if (deletedSkillOfResume == null)
            {
                throw new NotFoundException(nameof(SkillOfResume), request.Id);
            }

            await _skillOfResumeRepository.Delete(deletedSkillOfResume);

            return Unit.Value;
        }
    }
}
