using Business.Application.DTOs.Job;
using Business.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobCommand : IRequest<BaseCommandResponse>
    {
        public CreateJobDTO JobDTO { get; set; }
    }
}
