using Provider.Domain.Entities;
using Provider.Infrastructure.Repositories;
using Provider.Grpc.Protos;
using Provider.Application.Contracts.Persistence;
using Grpc.Core;
using AutoMapper;

namespace Provider.Grpc.Services
{
    public class CareerService : CareerProtoService.CareerProtoServiceBase
    {
        private readonly ICareerRepository careerRepository;
        private readonly ILogger<CareerService> logger;
        private readonly IMapper _mapper;

        public CareerService(ICareerRepository careerRepository, ILogger<CareerService> logger, IMapper mapper)
        {
            this.careerRepository = careerRepository;
            this.logger = logger;
            _mapper = mapper;
        }

        public override async Task<CareerModel> GetCareer(GetCareerRequest request, ServerCallContext context)
        {
            var career = await careerRepository.GetById(request.CareerId);
            if(career == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Career with id = {request.CareerId} is not found."));
            }
            
            var careerModel = new CareerModel { Name = career.Name, Id = career.Id };
            logger.LogInformation($"Career is retrieved: {career.Name} - {career.Id}");
            return careerModel;
        }
    }
}
