using MediatR;
using Provider.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Queries.GetSkillByAdmin
{
    public class GetSkillByAdminQuery : IRequest<GetSkillAdminListDTO>
    {
        public int Page { get; set; }
        public int CareerId { get; set; }
    }
}
