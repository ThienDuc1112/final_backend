using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Commands.EnableLanguage
{
    public class EnableLanguageCommand : IRequest
    {
        public int Id { get; set; }
    }
}
