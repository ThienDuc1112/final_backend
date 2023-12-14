using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Commands.DeleteCareer
{
    public class DeleteCareerCommandHandler : IRequestHandler<DeleteCareerCommand>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;
        
        public DeleteCareerCommandHandler(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCareerCommand request, CancellationToken cancellationToken)
        {
            var deletedCareer = await _careerRepository.GetById(request.Id);
            if (deletedCareer == null)
            {
                throw new NotFoundException(nameof(deletedCareer), request.Id);
            }

            await _careerRepository.Delete(deletedCareer);

            return Unit.Value;
        }
    }
}
