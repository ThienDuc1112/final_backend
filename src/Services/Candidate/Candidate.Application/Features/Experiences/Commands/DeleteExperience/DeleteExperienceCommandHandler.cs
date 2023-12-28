using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Experiences.Commands.DeleteExperience
{
    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;

        public DeleteExperienceCommandHandler(IExperienceRepository experienceRepository, IMapper mapper)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var deletedExperience = await _experienceRepository.GetById(request.Id);
            if (deletedExperience == null)
            {
                throw new NotFoundException(nameof(deletedExperience), request.Id);
            }

            await _experienceRepository.Delete(deletedExperience);

            return Unit.Value;
        }
    }
}
