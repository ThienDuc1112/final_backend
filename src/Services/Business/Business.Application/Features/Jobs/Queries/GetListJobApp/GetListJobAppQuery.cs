using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetListJobApp
{
    public class GetListJobAppQuery : IRequest<List<GetJobAppDTO>>
    {
        public int BusinessId { get; set; }
    }
}
