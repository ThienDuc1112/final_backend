using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Educations.Commands.DeleteEducation
{
    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;

        public DeleteEducationCommandHandler(IEducationRepository educationRepository, IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var deletedEducation = await _educationRepository.GetById(request.Id);
            if (deletedEducation == null)
            {
                throw new NotFoundException(nameof(deletedEducation), request.Id);
            }

            await _educationRepository.Delete(deletedEducation);

            return Unit.Value;
        }
    }
}
