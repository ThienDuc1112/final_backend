using MediatR;
using Provider.Application.DTOs.Career;
using Provider.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Commands.CreateCareer
{
    public class CreateCareerCommand : IRequest<BaseCommandResponse>
    {
        public CreateCareerDTO CareerDTO { get; set; }
    }
}
