using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Career;
using Provider.Application.Features.Careers.Queries.GetCareerList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Queries.GetCareerAdminList
{
    public class GetCareerAdminListQueryHandler : IRequestHandler<GetCareerAdminListQuery, List<CareerDTO>>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;

        public GetCareerAdminListQueryHandler(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }

        public async Task<List<CareerDTO>> Handle(GetCareerAdminListQuery request, CancellationToken cancellationToken)
        {
            var careerList = await _careerRepository.GetAll();
            return _mapper.Map<List<CareerDTO>>(careerList);
        }
    }

}
