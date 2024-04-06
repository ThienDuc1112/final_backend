using Application.Domain.DTOs.AppliedJob;
using Application.Domain.Entities;
using Application.Infrastructure.Persistance;
using Application.Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Repositories.Implementation
{
    public class AppliedJobRepository : GenericRepository<AppliedJob>, IAppliedJobRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AppliedJobRepository(ApplicationDbContext DbContext) : base(DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<List<GetApplicationList>> GetApplicationList(string candidateId)
        {
            var jobs = await _dbContext.AppliedJobs.Where(j => j.CandidateId == candidateId)
                .Select(j => new GetApplicationList
                {
                    Id = j.Id,
                    CreatedDate = j.CreatedDate,
                    Status = j.Status,
                    JobId = j.JobId,
                }).OrderByDescending(j => j.CreatedDate)
                .ToListAsync();

            return jobs;
        }

        public async Task<List<AppliedJob>> GetAppliedJob(int jobId, string status)
        {
            IQueryable<AppliedJob> apps = _dbContext.AppliedJobs;
            if(status != "All")
            {
                apps = apps.Where(x => x.Status == status);
            }
            var applications = await apps.Where(a => a.JobId == jobId).ToListAsync();
            return applications;
        }

        public async Task<GetRawAppliedJob> GetAppliedJobDashboard(int businessId)
        {
            var appCount = await _dbContext.AppliedJobs
                .Where(a => a.BusinessId == businessId)
                .CountAsync();
               
           var interviewCount = await _dbContext.AppliedJobs
                 .Where(a => a.BusinessId == businessId && a.Status == "Interviewing")
                .CountAsync();

            var rawJobs = await _dbContext.AppliedJobs
                .Where(a => a.BusinessId == businessId).
                Select(a => new RawAppliedJob
                {
                    Id = a.Id,
                    ResumeId = a.ResumeId,
                    CreatedDate = a.CreatedDate,
                    Status = a.Status,
                })
                .OrderByDescending(j => j.Id)
                .Take(6)
                .ToListAsync();
            var data = new GetRawAppliedJob { ApplicationCount = appCount, InterviewCount = interviewCount, Jobs = rawJobs };
            return data;
        }

        public async Task<QueryAppliedJobDTO> GetAppliedJobDetailDTO(int id)
        {
            var application = await _dbContext.AppliedJobs.Where(a => a.Id == id)
                .Select(a => new QueryAppliedJobDTO
                {
                    Id = a.Id,
                    JobId = a.JobId,
                    ResumeId = a.ResumeId,
                    Status = a.Status,
                    Url = a.Url,
                    CreatedDate = a.CreatedDate,
                    AppliedNumber = _dbContext.AppliedJobs.Count(j => j.JobId == a.JobId),
                    AcceptedNumber = _dbContext.AppliedJobs.Count(j => j.JobId == a.JobId && j.Status == "Accepted")
                }).FirstOrDefaultAsync();

            return application;
        }

        public async Task<bool> IsExisted(int jobId, string candidateId)
        {
            var isExisted = await _dbContext.AppliedJobs.AnyAsync(a => a.JobId == jobId && a.CandidateId == candidateId);
            return isExisted;
        }
    }
}
