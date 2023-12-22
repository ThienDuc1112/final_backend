using MediatR;
using Provider.Application.DTOs.Language;
using Provider.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<BaseCommandResponse>
    {
        public CreateLanguageDTO CreateLanguageDTO { get; set; }
    }
}
