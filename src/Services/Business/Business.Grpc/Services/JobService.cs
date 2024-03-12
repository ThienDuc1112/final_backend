using AutoMapper;
using Business.Application.Contracts;
using Business.Grpc.Protos;
using Grpc.Core;

namespace Business.Grpc.Services
{
    public class JobService : JobProtoService.JobProtoServiceBase
    {
        private readonly IJobRepository jobRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public JobService(IJobRepository jobRepository, ILogger logger, IMapper mapper)
        {
            this.jobRepository = jobRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public override async Task<JobModel> GetJob(GetJobRequest request, ServerCallContext context)
        {
            var job = await jobRepository.GetById(request.JobId);
            if(job == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Job with id = {request.JobId} is not found."));
            }

            var jobModel = mapper.Map<JobModel>(job);
            return jobModel;

        }
    }
}
