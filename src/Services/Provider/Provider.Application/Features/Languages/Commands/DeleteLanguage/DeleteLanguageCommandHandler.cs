using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetById(request.id);

            if (language == null)
            {
                throw new NotFoundException(nameof(language), request.id);
            }

            await _languageRepository.RemoveLanguage(language.Id);

            return Unit.Value;
        }
    }
}
