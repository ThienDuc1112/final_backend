using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Commands.DeleteJob
{
    public class DeleteJobCommand : IRequest
    {
        public int Id { get; set; }
    }
}
