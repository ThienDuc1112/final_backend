using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Experiences.Commands.UpdateExperience
{
    public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, Unit>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;

        public UpdateExperienceCommandHandler(IExperienceRepository experienceRepository, IMapper mapper)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateExperienceValidator();
            var validationResult = await validator.ValidateAsync(request.ExperienceDTO);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var experience = await _experienceRepository.GetById(request.ExperienceDTO.Id);
            if (experience == null)
            {
                throw new NotFoundException(nameof(experience), request.ExperienceDTO.Id);
            }

            _mapper.Map(request.ExperienceDTO, experience);

            await _experienceRepository.Update(experience);

            return Unit.Value;
        }
    }
}
