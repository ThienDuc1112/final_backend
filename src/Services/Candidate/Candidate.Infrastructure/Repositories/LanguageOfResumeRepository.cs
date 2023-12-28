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
    public class LanguageOfResumeRepository : GenericRepository<LanguageOfResume>, ILanguageOfResumeRepository
    {
        private readonly CandidateDbContext _dbContext;

        public LanguageOfResumeRepository(CandidateDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> IsExistedLanguage(int languageId, int resumeId)
        {
         return await _dbContext.LanguageOfResumes.AnyAsync(l => l.LanguageId == languageId && l.ResumeId == resumeId);   
        }
    }
}
