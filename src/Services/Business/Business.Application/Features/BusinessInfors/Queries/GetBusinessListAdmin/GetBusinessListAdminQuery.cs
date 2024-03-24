using Business.Application.DTOs.BusinessInfor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetBusinessListAdmin
{
    public class GetBusinessListAdminQuery : IRequest<GetBusinessListAdminDTO>
    {
        public int Page { get; set; }
        public string Status { get; set; }
    }
}
