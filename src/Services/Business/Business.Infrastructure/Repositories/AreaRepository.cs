using Business.Application.Contracts;
using Business.Domain.Entities;
using Business.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Infrastructure.Repositories
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        private readonly BusinessDbContext _dbContext;
        public AreaRepository(BusinessDbContext businessDbContext) : base(businessDbContext)
        {
            _dbContext = businessDbContext;
        }
        public async Task<List<Area>> GetAreasByBusinessId(int id)
        {
            var areaList = _dbContext.Areas.Where(a => a.BusinessId == id);
            return await areaList.ToListAsync();
        }

        public async Task<bool> IsExisted(int businessId, int careerId)
        {
            var existedArea = _dbContext.Areas.AnyAsync(a => a.BusinessId == businessId && a.CareerId == careerId);
            return await existedArea;
        }
    }
}
