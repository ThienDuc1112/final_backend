using Business.Application.DTOs.BusinessInfor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Queries.GetBusinessInfor
{
    public class GetBusinessInforQuery : IRequest<BusinessInforDTO>
    {
        public int Id { get; set; }
    }
}
