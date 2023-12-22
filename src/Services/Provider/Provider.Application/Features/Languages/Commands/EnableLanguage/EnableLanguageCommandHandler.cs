using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using Provider.Application.Features.Languages.Commands.DeleteLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Commands.EnableLanguage
{
    public class EnableLanguageCommandHandler : IRequestHandler<EnableLanguageCommand>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public EnableLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EnableLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetById(request.Id);

            if (language == null)
            {
                throw new NotFoundException(nameof(language), request.Id);
            }

            await _languageRepository.AllowLanguage(language.Id);

            return Unit.Value;
        }
    }
}
