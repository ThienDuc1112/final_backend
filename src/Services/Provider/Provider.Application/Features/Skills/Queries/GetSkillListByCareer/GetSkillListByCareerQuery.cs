using MediatR;
using Provider.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Queries.GetSkillListByCareer
{
    public class GetSkillListByCareerQuery:IRequest<List<SkillDTO>>
    {
        public GetSkillListByCareerQuery(int careerId)
        {
            CareerId = careerId;
        }

        public int CareerId { get; set; }
    }
}
