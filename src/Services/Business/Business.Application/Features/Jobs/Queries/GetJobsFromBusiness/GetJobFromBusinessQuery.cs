using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetJobsFromBusiness
{
    public class GetJobFromBusinessQuery : IRequest<List<GetJobDTO>>
    {
        public int BusinessId { get; set; }
    }
}
