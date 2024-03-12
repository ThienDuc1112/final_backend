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

        public async Task<List<AppliedJob>> GetAppliedJob(int jobId)
        {
            var applications = await _dbContext.AppliedJobs.Where(a => a.JobId == jobId).ToListAsync();
            return applications;
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
