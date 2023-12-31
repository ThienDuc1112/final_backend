using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.LanguagesOfResume.Commands.DeleteLanguageOfResume
{
    public class DeleteLanguageOfResumeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
