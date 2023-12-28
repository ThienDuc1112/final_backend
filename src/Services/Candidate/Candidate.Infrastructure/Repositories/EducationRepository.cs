using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using Candidate.Domain.Model;
using Candidate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Infrastructure.Repositories
{
    public class EducationRepository : GenericRepository<Education>, IEducationRepository
    {
        private readonly CandidateDbContext _dbContext;

        public EducationRepository(CandidateDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LevelEducation>> GetLevelEducationList(int idResume)
        {
            var educationList = await _dbContext.Educations.Where(e => e.ResumeId == idResume).ToListAsync();
            return educationList.Select(education => new LevelEducation
            {
                Major = education.Major,
                Degree = education.Degree,
            }).ToList();
            
        }
       
    }
}
