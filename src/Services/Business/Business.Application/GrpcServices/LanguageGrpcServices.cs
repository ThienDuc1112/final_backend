using Provider.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Provider.Grpc.Protos.LanguageProtoService;

namespace Business.Application.GrpcServices
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
