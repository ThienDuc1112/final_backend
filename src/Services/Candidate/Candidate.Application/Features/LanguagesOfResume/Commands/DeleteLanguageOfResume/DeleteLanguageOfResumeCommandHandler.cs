using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using Candidate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.LanguagesOfResume.Commands.DeleteLanguageOfResume
{
    public class DeleteLanguageOfResumeCommandHandler : IRequestHandler<DeleteLanguageOfResumeCommand>
    {
        private readonly ILanguageOfResumeRepository _languageOfResumeRepository;

        public DeleteLanguageOfResumeCommandHandler(ILanguageOfResumeRepository languageOfResumeRepository)
        {
            _languageOfResumeRepository = languageOfResumeRepository;
        }

        public async Task<Unit> Handle(DeleteLanguageOfResumeCommand request, CancellationToken cancellationToken)
        {
            var languageOfResume = await _languageOfResumeRepository.GetById(request.Id);
            if (languageOfResume == null)
            {
                throw new NotFoundException(nameof(LanguageOfResume), request.Id);
            }

            await _languageOfResumeRepository.Delete(languageOfResume);

            return Unit.Value;
        }
    }
}
