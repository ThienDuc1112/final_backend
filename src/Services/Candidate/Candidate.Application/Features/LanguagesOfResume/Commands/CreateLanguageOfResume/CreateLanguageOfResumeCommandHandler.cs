using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Responses;
using Candidate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.LanguagesOfResume.Commands.CreateLanguageOfResume
{
    public class CreateLanguageOfResumeCommandHandler : IRequestHandler<CreateLanguageOfResumeCommand, BaseCommandResponse>
    {
        private readonly ILanguageOfResumeRepository _languageOfResumeRepository;
        private readonly IMapper _mapper;

        public CreateLanguageOfResumeCommandHandler(ILanguageOfResumeRepository languageOfResumeRepository, IMapper mapper)
        {
            _languageOfResumeRepository = languageOfResumeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLanguageOfResumeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLanguageOfResumeValidator();
            var validationResult = await validator.ValidateAsync(request.LanguageOfResumeDTO);

            if (await _languageOfResumeRepository.IsExistedLanguage(request.LanguageOfResumeDTO.LanguageId, request.LanguageOfResumeDTO.ResumeId))
            {
                throw new ArgumentException("This language existed");
            }

            if (validationResult.IsValid == false)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var languageOfResume = _mapper.Map<LanguageOfResume>(request.LanguageOfResumeDTO);
                await _languageOfResumeRepository.Add(languageOfResume);
                response.Id = languageOfResume.Id;
                response.Success = true;
                response.Message = "Creation of language of resume successful";
            }

            return response;
        }
    }
}
