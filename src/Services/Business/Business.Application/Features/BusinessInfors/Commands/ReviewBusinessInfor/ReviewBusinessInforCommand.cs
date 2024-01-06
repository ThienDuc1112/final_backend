using Business.Application.DTOs.BusinessInfor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Commands.ReviewBusinessInfor
{
    public class ReviewBusinessInforCommand : IRequest<Unit>
    {
        public ReviewBusinessInforDTO BusinessInforDTO { get; set; }
    }
}
