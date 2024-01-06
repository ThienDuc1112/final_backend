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
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        private readonly BusinessDbContext _dbContext;
        public JobRepository(BusinessDbContext businessDbContext) : base(businessDbContext)
        {
            _dbContext = businessDbContext;
        }
        public async Task<List<Job>> GetAllJobs()
        {
            return await _dbContext.Jobs.ToListAsync();
        }
    }
}
