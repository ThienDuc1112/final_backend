using Application.Domain.Entities;
using Application.Infrastructure.Persistance;
using Application.Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Repositories.Implementation
{
    public class FavoriteJobRepository : GenericRepository<FavoriteJob>, IFavoriteJobRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public FavoriteJobRepository(ApplicationDbContext DbContext) : base(DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<List<FavoriteJob>> GetFavoriteJobs(string candidateId)
        {
            return await _dbContext.FavoriteJobs.Where(j => j.CandidateId == candidateId)
                .AsNoTracking().ToListAsync();
        }

        public async Task<bool> IsExisted(string candidateId, int jobId)
        {
            return await _dbContext.FavoriteJobs.AnyAsync(j => j.CandidateId == candidateId && j.JobId == jobId);
        }
    }
}
