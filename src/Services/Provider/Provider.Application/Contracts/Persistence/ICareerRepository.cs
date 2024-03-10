using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Contracts.Persistence
{
    public interface ICareerRepository : IGenericRepository<Career>
    {
        Task<bool> IsExisted(string name);
        Task<List<Career>> GetCareersWithSkills();
    }
}
