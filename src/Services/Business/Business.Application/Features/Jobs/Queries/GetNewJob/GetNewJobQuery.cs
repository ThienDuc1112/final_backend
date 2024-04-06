using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetNewJob
{
    public class GetNewJobQuery : IRequest<List<GetJobDTO>>
    {

    }
}
