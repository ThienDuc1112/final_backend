using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Provider.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillCommand : IRequest
    {
        public int Id { get; set; }
    }
}
