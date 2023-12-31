using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using Candidate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.LanguagesOfResume.Commands.UpdateLanguageOfResume
{
    public class UpdateLanguageOfResumeCommandHandler : IRequestHandler<UpdateLanguageOfResumeCommand, Unit>
    {
        private readonly ILanguageOfResumeRepository _languageOfResumeRepository;
        private readonly IMapper _mapper;

        public UpdateLanguageOfResumeCommandHandler(ILanguageOfResumeRepository languageOfResumeRepository, IMapper mapper)
        {
            _languageOfResumeRepository = languageOfResumeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLanguageOfResumeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLanguageOfResumeValidator();
            var validationResult = await validator.ValidateAsync(request.LanguageOfResumeDTO);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var languageOfResume = await _languageOfResumeRepository.GetById(request.LanguageOfResumeDTO.Id);
            if (languageOfResume == null)
            {
                throw new NotFoundException(nameof(LanguageOfResume), request.LanguageOfResumeDTO.Id);
            }

            _mapper.Map(request.LanguageOfResumeDTO, languageOfResume);

            await _languageOfResumeRepository.Update(languageOfResume);

            return Unit.Value;
        }
    }
}
