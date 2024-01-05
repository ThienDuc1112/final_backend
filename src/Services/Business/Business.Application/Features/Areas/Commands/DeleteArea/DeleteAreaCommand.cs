using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Areas.Commands.DeleteArea
{
    public class DeleteAreaCommand : IRequest
    {
        public int Id { get; set; }
    }
}
