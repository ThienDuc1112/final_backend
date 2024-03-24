using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Skill;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Queries.GetSkillList
{
    public class GetSkillListQueryHandler : IRequestHandler<GetSkillListQuery, List<GetSkillAdminDTO>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetSkillListQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        public async Task<List<GetSkillAdminDTO>> Handle(GetSkillListQuery request, CancellationToken cancellationToken)
        {
            var skillList = await _skillRepository.GetActiveSkills();
            return _mapper.Map<List<GetSkillAdminDTO>>(skillList);
        }
    }
}
