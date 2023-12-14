using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using Provider.Application.Features.Careers.Commands.CreateCareer;


namespace Provider.Application.Features.Careers.Commands.UpdateCareer
{
    public class UpdateCareerCommandHandler : IRequestHandler<UpdateCareerCommand, Unit>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;

        public UpdateCareerCommandHandler(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCareerCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCareerValidator();
            var validationResult = await validator.ValidateAsync(request.CareerDTO);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var career = await _careerRepository.GetById(request.CareerDTO.Id);
            if(career == null)
            {
                throw new NotFoundException(nameof(career),request.CareerDTO.Id);
            }

            _mapper.Map(request.CareerDTO, career);

            await _careerRepository.Update(career);

            return Unit.Value;
        }
    }
}
