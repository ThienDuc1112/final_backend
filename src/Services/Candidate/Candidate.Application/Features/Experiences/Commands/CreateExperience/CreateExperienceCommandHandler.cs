using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Responses;
using Candidate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Experiences.Commands.CreateExperience
{
    public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, BaseCommandResponse>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;

        public CreateExperienceCommandHandler(IExperienceRepository experienceRepository, IMapper mapper)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateExperienceValidator();
            var validationResult = await validator.ValidateAsync(request.ExperienceDTO);

            if (validationResult.IsValid == false)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var experience = _mapper.Map<Experience>(request.ExperienceDTO);
                await _experienceRepository.Add(experience);
                response.Id = experience.Id;
                response.Success = true;
                response.Message = "Experience creation successful";
            }

            return response;
        }
    }
}
