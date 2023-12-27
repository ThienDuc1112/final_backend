using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using Candidate.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Infrastructure.Repositories
{
    public class ResumeRepository : GenericRepository<Resume>, IResumeRepository
    {
        private readonly CandidateDbContext _dbcontext;
        public ResumeRepository(CandidateDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
        public Task<string> ProvideData()
        {
            throw new NotImplementedException();
        }

        public Task<string> ProvideGender()
        {
            throw new NotImplementedException();
        }

        public Task<string> ProvideStatusOfEmployment()
        {
            throw new NotImplementedException();
        }
    }
}
