using Microsoft.EntityFrameworkCore;
using Provider.Application.Contracts.Persistence;
using Provider.Domain.Entities;
using Provider.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Repositories
{
    public class CareerRepository : GenericRepository<Career>, ICareerRepository
    {
        private readonly ProviderDbContext _dbcontext;
        public CareerRepository(ProviderDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<bool> IsExisted(string name)
        {
            return await _dbcontext.Careers.AllAsync(e => e.Name == name);
        }
    }
}
