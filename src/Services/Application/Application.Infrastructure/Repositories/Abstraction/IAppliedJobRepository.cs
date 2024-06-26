﻿using Application.Domain.DTOs.AppliedJob;
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

        Task<List<AppliedJob>> GetAppliedJob(int jobId, string status);

        Task<QueryAppliedJobDTO> GetAppliedJobDetailDTO(int id);
        Task<List<GetApplicationList>> GetApplicationList(string candidateId);
        Task<GetRawAppliedJob> GetAppliedJobDashboard(int businessId);
        Task<List<GetInterviewCandidate>> GetInterviewCandidate(int? jobId);

    }
}
