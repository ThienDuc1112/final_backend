using MediatR;
using Provider.Application.DTOs.Skill;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Queries.GetSkillList
{
    public class GetSkillListQuery : IRequest<List<GetSkillAdminDTO>>
    {
    }
}
