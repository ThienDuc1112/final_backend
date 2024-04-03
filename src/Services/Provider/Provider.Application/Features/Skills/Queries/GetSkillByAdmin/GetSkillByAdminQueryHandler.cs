using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Skill;


namespace Provider.Application.Features.Skills.Queries.GetSkillByAdmin
{
    public class GetSkillByAdminQueryHandler : IRequestHandler<GetSkillByAdminQuery, GetSkillAdminListDTO>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetSkillByAdminQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        public async Task<GetSkillAdminListDTO> Handle(GetSkillByAdminQuery request, CancellationToken cancellationToken)
        {
            var skillList = await _skillRepository.GetSkillsByAdmin(request.Page, request.CareerId);
            return skillList;
        }
    }
}
