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

        Task<GetListJobDTO> GetJobs(int? page, string? query, string? jobType,
            decimal? minSalary, decimal? maxSalary, int? career, List<string> experience,
            string date, List<string> position, List<string> education);

        Task<List<GetJobDTO>> GetJobsFromBusiness(int businessId);

        Task<Job> GetJobById(int id);
        Task<List<Job>> GetJobByCareerId(int careerId);

        Task<GetJobManagementListDTO> GetJobManagements(int? page, int businessId);
        Task<List<Job>> GetJobApp(int businessId);
        Task<GetJobWithBusinessDTO> GetJobWithBusiness(int id);
        Task<GetJobDashBoard> GetJobDashBoard(int businessId);
        Task<List<GetJobDTO>> GetNewJobs();
    }
}
