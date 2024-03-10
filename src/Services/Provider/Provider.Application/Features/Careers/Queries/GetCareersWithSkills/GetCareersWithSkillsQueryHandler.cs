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

namespace Provider.Application.Features.Careers.Queries.GetCareersWithSkills
{
    public class GetCareersWithSkillsQueryHandler : IRequestHandler<GetCareersWithSkillsQuery, List<GetCareerDTO>>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;

        public GetCareersWithSkillsQueryHandler(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }

        public async Task<List<GetCareerDTO>> Handle(GetCareersWithSkillsQuery request, CancellationToken cancellationToken)
        {
            var careerList = await _careerRepository.GetCareersWithSkills();
            return _mapper.Map<List<GetCareerDTO>>(careerList);
        }
    }
}
