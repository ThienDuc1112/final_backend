using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Career;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Queries.GetCareerList
{
    public class GetCareerListQueryHandler : IRequestHandler<GetCareerListQuery, List<CareerDTO>>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;

        public GetCareerListQueryHandler(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }

        public async Task<List<CareerDTO>> Handle(GetCareerListQuery request, CancellationToken cancellationToken)
        {
            var careerList = await _careerRepository.GetAll();
            return _mapper.Map<List<CareerDTO>>(careerList);
        }
    }
}
