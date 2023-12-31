using Candidate.Application.DTOs.LanguageOfResume;
using Candidate.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.LanguagesOfResume.Commands.CreateLanguageOfResume
{
    public class CreateLanguageOfResumeCommand : IRequest<BaseCommandResponse>
    {
        public CreateLanguageOfResumeDTO LanguageOfResumeDTO { get; set; }
    }
}
