using Provider.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.GrpcServices
{
    public class CareerGrpcService
    {
        private readonly CareerProtoService.CareerProtoServiceClient _careerProtoService;

        public CareerGrpcService(CareerProtoService.CareerProtoServiceClient careerProtoService)
        {
            _careerProtoService = careerProtoService;
        }

        public async Task<CareerModel> GetCareer(int careerId)
        {
            var request = new GetCareerRequest { CareerId = careerId };
            return await _careerProtoService.GetCareerAsync(request);
        }


    }
}
