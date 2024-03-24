using MediatR;
using Provider.Application.DTOs.Career;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Commands.TriggerCareer
{
    public class TriggerCareerCommand : IRequest<Unit>
    {
        public TriggerCareerDTO TriggerCareerDTO { get; set; }
    }
}
