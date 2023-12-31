using Provider.Domain.Entities;
using Provider.Infrastructure.Repositories;
using Provider.Grpc.Protos;
using Provider.Application.Contracts.Persistence;
using Grpc.Core;
using AutoMapper;

namespace Provider.Grpc.Services
{
    public class LanguageService : LanguageProtoService.LanguageProtoServiceBase
    {
        private readonly ILanguageRepository languageRepository;
        private readonly ILogger<LanguageService> logger;
        private readonly IMapper _mapper;

        public LanguageService(ILanguageRepository languageRepository, ILogger<LanguageService> logger, IMapper mapper)
        {
            this.languageRepository = languageRepository;
            this.logger = logger;
            _mapper = mapper;
        }

        public override async Task<LanguageModel> GetLanguage(GetLanguageRequest request, ServerCallContext context)
        {
            var language = await languageRepository.GetById(request.LanguageId);
            if (language == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Language with id = {request.LanguageId} is not found."));
            }

            var languageModel = _mapper.Map<LanguageModel>(language);
            logger.LogInformation($"Language is retrieved: {language.LanguageName} - {language.Id}");
            return languageModel;
        }
    }
}
