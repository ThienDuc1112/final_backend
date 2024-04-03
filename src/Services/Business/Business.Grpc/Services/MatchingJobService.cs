using Business.Application.Contracts;
using Business.Domain.Entities;
using Business.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Business.Grpc.Services
{
    public class MatchingJobService : MatchingJobProtoService.MatchingJobProtoServiceBase
    {
        private readonly IJobRepository jobRepository;

        public MatchingJobService(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public override async Task<MatchingJobModel> GetMatchingJob(GetMatchingJobRequest request, ServerCallContext context)
        {
            var job = await jobRepository.GetJobById(request.JobId);
            if (job == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Job with Id = {request.JobId} is not found."));
            }

            MatchingJobModel model = new MatchingJobModel();
            model.Id = job.Id;
            model.CareerId = job.CareerId;
            model.EducationLevelMin = job.EducationLevelMin;
            model.RequiredSkills = job.RequiredSkills;
            model.YearExpMin = job.YearExpMin;
            model.GenderRequirement = job.GenderRequirement;
            model.LanguageRequirementId = job.LanguageRequirementId;
            model.Requirement = job.Requirement;
            model.BusinessId = job.BusinessId;
            model.FullName = job.Business.FullName;
            model.SalaryMin = (int)job.SalaryMin;
            model.SalaryMax = (int)job.SalaryMax;
            model.LogoUrl = job.Business.LogoUrl;
            model.Title = job.Title;
            model.JobType = job.JobType;
            model.ExpirationDate = job.ExpirationDate.ToString("yyyy-MM-dd HH:mm:ss");

            return model;
        }
    }
}
