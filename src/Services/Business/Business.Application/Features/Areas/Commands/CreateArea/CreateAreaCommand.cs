using Business.Application.DTOs.Area;
using Business.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateAreaDTO> AreaDTO { get; set; }
    }
}
