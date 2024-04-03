using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.GrpcServices;
using Candidate.Domain.Entities;
using Candidate.Grpc.Protos;
using Grpc.Core;

namespace Candidate.Grpc.Services
{
    public class MatchingResumeService : MatchingResumeProtoService.MatchingResumeProtoServiceBase
    {

        private readonly IResumeRepository resumeRepository;
        private readonly SkillGrpcService _skillGrpcService;

        public MatchingResumeService(IResumeRepository resumeRepository, SkillGrpcService skillGrpcService)
        {
            this.resumeRepository = resumeRepository;
            _skillGrpcService = skillGrpcService;
        }

        public override async Task<MatchingResumeModel> GetMatchingResume(GetMatchingResumeRequest request, ServerCallContext context)
        {
            var resume = await resumeRepository.FindResumeById(request.ResumeId);
            if (resume == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Resume with id = {request.ResumeId} is not found."));
            }

            MatchingResumeModel model = new MatchingResumeModel();
            model.Id = resume.Id;
            model.Gender = resume.Gender;
            model.AdditionalSkill = resume.AdditionalSkills ?? "";
            model.CareerId = resume.CareerId;
            if (resume.Languages != null)
            {
                foreach (var language in resume.Languages)
                {
                    if (language != null)
                    {
                        model.Languages.Add(language.LanguageId);
                    }
                }
            }
            if (resume.Skills != null)
            {
                foreach (var skill in resume.Skills)
                {
                    var foundSkill = await _skillGrpcService.GetSkill(skill.SkillId);
                    model.Skills.Add(foundSkill.NameSkill);
                }
            }
            if (resume.Educations != null)
            {
                foreach (var education in resume.Educations)
                {
                    model.EducationDegree.Add(education.Degree);
                }
            }
            model.ExperienceYear = resume.Experiences != null ? CalculateTotalYearsOfExperience(resume.Experiences) : 0;

            return model;
        }

        private double CalculateTotalYearsOfExperience(List<Experience> experiences)
        {
            double totalDays = 0;

            foreach (var experience in experiences)
            {

                double experienceDays = (experience.EndDate - experience.StartDate).TotalDays;

                totalDays += experienceDays;
            }

            double totalYears = totalDays / 365.25;

            return totalYears;
        }
    }

}
