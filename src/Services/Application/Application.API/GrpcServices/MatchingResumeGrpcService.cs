using Candidate.Grpc.Protos;

namespace Application.API.GrpcServices
{
    public class MatchingResumeGrpcService
    {
        private readonly MatchingResumeProtoService.MatchingResumeProtoServiceClient _service;

        public MatchingResumeGrpcService(MatchingResumeProtoService.MatchingResumeProtoServiceClient service)
        {
            _service = service;
        }

        public async Task<MatchingResumeModel> GetMatchingResume(int resumeId)
        {
            var request = new GetMatchingResumeRequest { ResumeId = resumeId };
            return await _service.GetMatchingResumeAsync(request);
        }
    }
}
