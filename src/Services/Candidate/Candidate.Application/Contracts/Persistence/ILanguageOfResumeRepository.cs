using Candidate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Contracts.Persistence
{
    public interface ILanguageOfResumeRepository : IGenericRepository<LanguageOfResume>
    {
        Task<bool> IsExistedLanguage(int languageId, int resumeId);
    }
}
