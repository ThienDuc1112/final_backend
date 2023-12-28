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
    public class SkillOfResumeRepository : GenericRepository<SkillOfResume>, ISkillOfResumeRepository
    {
        private readonly CandidateDbContext _dbcontext;
        public SkillOfResumeRepository(CandidateDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<bool> IsExistedSkill(int skillId, int resumeId)
        {
            return await _dbcontext.SkillOfResumes.AnyAsync(s => s.SkillId == skillId && s.ResumeId == resumeId);
        }
    }
}
