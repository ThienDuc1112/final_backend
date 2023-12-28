using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using Candidate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Infrastructure.Repositories
{
    public class ExperienceRepository : GenericRepository<Experience>, IExperienceRepository
    {
        private readonly CandidateDbContext _dbContext;

        public ExperienceRepository(CandidateDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetExperienceYear(int idResume)
        {
            var experiences = await _dbContext.Experiences
               .Where(e => e.ResumeId == idResume)
               .ToListAsync();

            int totalExperienceYears = 0;

            foreach (var experience in experiences)
            {
                TimeSpan experienceDuration = experience.EndDate - experience.StartDate;
                totalExperienceYears += (int)experienceDuration.TotalDays;
            }

            return (int)Math.Floor(totalExperienceYears / 365.25);
        }
    }
}
