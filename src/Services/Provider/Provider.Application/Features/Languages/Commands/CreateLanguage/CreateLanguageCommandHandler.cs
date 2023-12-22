using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Features.Careers.Commands.CreateCareer;
using Provider.Application.Responses;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, BaseCommandResponse>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLanguageValidator();
            var validationResult = await validator.ValidateAsync(request.CreateLanguageDTO);

            if (await _languageRepository.IsExisted(request.CreateLanguageDTO.LanguageName, request.CreateLanguageDTO.Level))
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors.Add("This language with this level was already existed");
            }
            else if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var language = _mapper.Map<Language>(request.CreateLanguageDTO);
                language = await _languageRepository.Add(language);
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = language.Id;
            }
            return response;
        }
    }
    
}
