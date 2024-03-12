using Application.Domain.DTOs.AppliedJob;
using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Repositories.Abstraction
{
    public interface IAppliedJobRepository : IGenericRepository<AppliedJob>
    {
        Task<bool> IsExisted(int jobId, string candidateId);

        Task<List<AppliedJob>> GetAppliedJob(int jobId);

        Task<QueryAppliedJobDTO> GetAppliedJobDetailDTO(int id);
    }
}
