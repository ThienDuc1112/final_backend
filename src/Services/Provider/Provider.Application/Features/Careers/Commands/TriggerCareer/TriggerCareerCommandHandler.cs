using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using Provider.Application.Features.Careers.Commands.UpdateCareer;

namespace Provider.Application.Features.Careers.Commands.TriggerCareer
{
    public class TriggerCareerCommandHandler : IRequestHandler<TriggerCareerCommand, Unit>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;

        public TriggerCareerCommandHandler(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(TriggerCareerCommand request, CancellationToken cancellationToken)
        {
            var validator = new TriggerCareerValidator();
            var validationResult = await validator.ValidateAsync(request.TriggerCareerDTO);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var career = await _careerRepository.GetById(request.TriggerCareerDTO.Id);
            if (career == null)
            {
                throw new NotFoundException(nameof(career), request.TriggerCareerDTO.Id);
            }

            _mapper.Map(request.TriggerCareerDTO, career);

            await _careerRepository.Update(career);

            return Unit.Value;
        }
    }
}
