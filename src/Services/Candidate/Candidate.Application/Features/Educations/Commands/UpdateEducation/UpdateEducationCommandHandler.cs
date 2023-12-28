using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Educations.Commands.UpdateEducation
{
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, Unit>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;

        public UpdateEducationCommandHandler(IEducationRepository educationRepository, IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEducationValidator();
            var validationResult = await validator.ValidateAsync(request.EducationDTO);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var education = await _educationRepository.GetById(request.EducationDTO.Id);
            if (education == null)
            {
                throw new NotFoundException(nameof(education), request.EducationDTO.Id);
            }

            _mapper.Map(request.EducationDTO, education);

            await _educationRepository.Update(education);

            return Unit.Value;
        }
    }
}
