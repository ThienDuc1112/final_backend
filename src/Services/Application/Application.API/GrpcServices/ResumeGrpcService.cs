using Candidate.Grpc.Protos;

namespace Application.API.GrpcServices
{
    public class ResumeGrpcService
    {
        private readonly ResumeProtoService.ResumeProtoServiceClient _service;

        public ResumeGrpcService(ResumeProtoService.ResumeProtoServiceClient service)
        {
            _service = service;
        }

        public async Task<ResumeModel> GetResume(int resumeId)
        {
            var request = new GetResumeRequest { ResumeId = resumeId };
            return await _service.GetResumeAsync(request);
        }
    }
}
