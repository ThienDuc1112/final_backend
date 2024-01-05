using Business.Application.DTOs.BusinessInfor;
using Business.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Commands.CreateBusinessInfor
{
    public class CreateBusinessInforCommand : IRequest<BaseCommandResponse>
    {
        public CreateBusinessInforDTO BusinessInforDTO { get; set; }
    }
}
