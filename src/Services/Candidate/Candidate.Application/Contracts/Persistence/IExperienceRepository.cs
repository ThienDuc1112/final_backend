using Candidate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Contracts.Persistence
{
    public interface IExperienceRepository : IGenericRepository<Experience>
    {
        Task<int> GetExperienceYear(int idResume);

    }
}
