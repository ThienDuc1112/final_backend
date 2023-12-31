
using Provider.Grpc.Protos;
using static Provider.Grpc.Protos.LanguageProtoService;

namespace Candidate.Application.GrpcServices
{
    public class LanguageGrpcService
    {
        private readonly LanguageProtoServiceClient _languageProtoService;

        public LanguageGrpcService(LanguageProtoServiceClient languageProtoService)
        {
            _languageProtoService = languageProtoService;
        }

        public async Task<LanguageModel> GetLanguage(int languageId)
        {
            var request = new GetLanguageRequest { LanguageId = languageId };
            return await _languageProtoService.GetLanguageAsync(request);
        }
    }
}
