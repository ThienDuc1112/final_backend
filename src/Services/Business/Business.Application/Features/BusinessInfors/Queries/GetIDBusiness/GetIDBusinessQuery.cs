using Business.Application.DTOs.BusinessInfor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Queries.GetIDBusiness
{
    public class GetIDBusinessQuery : IRequest<GetBusinessIdDTO>
    {
        public string userId { get; set; }
    }
}
