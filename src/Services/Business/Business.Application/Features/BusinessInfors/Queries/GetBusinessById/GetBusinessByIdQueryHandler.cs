using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.Area;
using Business.Application.DTOs.BusinessInfor;
using Business.Application.Exceptions;
using Business.Application.GrpcServices;
using MediatR;

namespace Business.Application.Features.BusinessInfors.Queries.GetBusinessById
{
    public class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, BusinessInforDTO>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;
        private readonly CareerGrpcService _careerGrpcService;


        public GetBusinessByIdQueryHandler(IBusinessRepository businessRepository, IMapper mapper,
            CareerGrpcService careerGrpcService)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
            _careerGrpcService = careerGrpcService;
            _careerGrpcService = careerGrpcService;
        }

        public async Task<BusinessInforDTO> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
        {
            var businessInfor = await _businessRepository.GetBusinessDetail(request.Id);
            if (businessInfor == null)
            {
                throw new NotFoundException(nameof(businessInfor), request.Id);
            }
            var businessInforDTO = _mapper.Map<BusinessInforDTO>(businessInfor);
            businessInforDTO.AreaDTOs = new List<AreaDTO>();
            foreach (var area in businessInfor.Areas)
            {
                var areaDTO = _mapper.Map<AreaDTO>(area);
                var career = await _careerGrpcService.GetCareer(area.CareerId);
                areaDTO.CareerName = career.Name;
                businessInforDTO.AreaDTOs.Add(areaDTO);
            }

            return businessInforDTO;
        }
    }
}
