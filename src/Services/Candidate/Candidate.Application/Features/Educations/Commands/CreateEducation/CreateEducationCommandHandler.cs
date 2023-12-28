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

namespace Candidate.Application.Features.Educations.Commands.CreateEducation
{
    public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, BaseCommandResponse>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;

        public CreateEducationCommandHandler(IEducationRepository educationRepository, IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEducationValidator();
            var validationResult = await validator.ValidateAsync(request.EducationDTO);

            if (validationResult.IsValid == false)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var education = _mapper.Map<Education>(request.EducationDTO);
                await _educationRepository.Add(education);
                response.Id = education.Id;
                response.Success = true;
                response.Message = "Creation education successful";
            }

            return response;
        }
    }
}
