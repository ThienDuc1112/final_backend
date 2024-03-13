using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Grpc.Protos;
using Grpc.Core;

namespace Candidate.Grpc.Services
{
    public class ResumeService : ResumeProtoService.ResumeProtoServiceBase
    {
        private readonly IResumeRepository resumeRepository;
        private readonly ILogger<ResumeService> logger;
        private readonly IMapper _mapper;

        public ResumeService(IResumeRepository resumeRepository, ILogger<ResumeService> logger, IMapper mapper)
        {
            this.resumeRepository = resumeRepository;
            this.logger = logger;
            _mapper = mapper;
        }

        public override async Task<ResumeModel> GetResume(GetResumeRequest request,ServerCallContext context)
        {
            var resume = await resumeRepository.GetById(request.ResumeId);
            if(resume == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Resume with id = {request.ResumeId} is not found."));
            }
            var resumeModel = _mapper.Map<ResumeModel>(resume);
            return resumeModel;
        }
    }
}
