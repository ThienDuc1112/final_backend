using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetDashBoardJob
{
    public class GetDashBoardJobQuery : IRequest<GetJobDashBoard>
    {
        public int BusinessId { get; set; }
    }
}
