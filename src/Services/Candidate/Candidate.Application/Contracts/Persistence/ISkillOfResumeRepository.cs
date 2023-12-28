using Candidate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Contracts.Persistence
{
    public interface ISkillOfResumeRepository : IGenericRepository<SkillOfResume>
    {
        Task<bool> IsExistedSkill(int skillId, int resumeId);
    }
}
