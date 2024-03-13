using Business.Application.DTOs.Job;
using Business.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Contracts
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<List<Job>> GetAllJobs();

        Task<List<GetJobDTO>> GetJobs(int? page, string? query, string? jobType,
            decimal? minSalary, decimal? maxSalary, int? career, List<string> experience,
            string date, List<string> position, List<string> education);

        Task<Job> GetJobById(int id);

        Task<List<GetJobManagementDTO>> GetJobManagements(int? page, int businessId);
        Task<List<Job>> GetJobApp(int businessId);
    }
}
