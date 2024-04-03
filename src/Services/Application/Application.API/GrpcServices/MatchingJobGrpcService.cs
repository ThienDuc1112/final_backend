using Business.Grpc.Protos;

namespace Application.API.GrpcServices
{
    public class MatchingJobGrpcService
    {
        private readonly MatchingJobProtoService.MatchingJobProtoServiceClient _jobProtoService;

        public MatchingJobGrpcService(MatchingJobProtoService.MatchingJobProtoServiceClient jobProtoService)
        {
            _jobProtoService = jobProtoService;
        }

        public async Task<MatchingJobModel> GetMatchingJob(int jobId)
        {
            var request = new GetMatchingJobRequest { JobId = jobId };

            return await _jobProtoService.GetMatchingJobAsync(request);
        }
    }
}
