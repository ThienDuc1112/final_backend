using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Career;
using Provider.Application.DTOs.Skill;
using Provider.Application.Features.Careers.Queries.GetCareerList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Queries.GetSkillListByCareer
{
    public class GetSkillListByCareerQueryHandler : IRequestHandler<GetSkillListByCareerQuery, List<SkillDTO>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetSkillListByCareerQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<List<SkillDTO>> Handle(GetSkillListByCareerQuery request, CancellationToken cancellationToken)
        {
            var skillList = await _skillRepository.GetSkillsByCarrerId(request.CareerId);
            return _mapper.Map<List<SkillDTO>>(skillList);
        }
    }
}
