using Business.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Contracts
{
    public interface IAreaRepository : IGenericRepository<Area>
    {
        Task<List<Area>> GetAreasByBusinessId(int id);
    }
}
