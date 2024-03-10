using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetJobManagement
{
    public class GetJobManagementQuery : IRequest<List<GetJobManagementDTO>>
    {
        public int? Page { get; set; }
        public int BusinessId { get; set; }
    }
}
