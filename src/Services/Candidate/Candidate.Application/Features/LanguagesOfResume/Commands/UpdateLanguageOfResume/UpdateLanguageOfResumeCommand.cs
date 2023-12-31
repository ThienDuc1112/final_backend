using Candidate.Application.DTOs.LanguageOfResume;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.LanguagesOfResume.Commands.UpdateLanguageOfResume
{
    public class UpdateLanguageOfResumeCommand : IRequest<Unit>
    {
        public UpdateLanguageOfResumeDTO LanguageOfResumeDTO { get; set; }
    }
}
