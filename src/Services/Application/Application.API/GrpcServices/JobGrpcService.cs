using Business.Grpc.Protos;
namespace Application.API.GrpcServices
{
    public class JobGrpcService
    {
        private readonly JobProtoService.JobProtoServiceClient _jobProtoService;

        public JobGrpcService(JobProtoService.JobProtoServiceClient jobProtoService)
        {
            _jobProtoService = jobProtoService;
        }

        public async Task<JobModel> GetJob(int jobId)
        {
            var request = new GetJobRequest { JobId = jobId };
            return await _jobProtoService.GetJobAsync(request);
        }
    }
}
