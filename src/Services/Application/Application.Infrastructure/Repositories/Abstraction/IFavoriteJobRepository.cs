using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Repositories.Abstraction
{
    public interface IFavoriteJobRepository : IGenericRepository<FavoriteJob>
    {
        Task<bool> IsExisted(string candidateId, int jobId);
        Task<List<FavoriteJob>> GetFavoriteJobs(string candidateId);
    }
}
