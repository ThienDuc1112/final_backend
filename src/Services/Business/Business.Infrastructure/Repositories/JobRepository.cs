using Business.Application.Contracts;
using Business.Application.DTOs.Job;
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

        public async Task<List<Job>> GetJobApp(int businessId)
        {
            var jobs = await _dbContext.Jobs
                .Where(j => j.BusinessId == businessId)
                .ToListAsync();

            return jobs;
        }

        public async Task<List<Job>> GetJobByCareerId(int careerId)
        {
            return await _dbContext.Jobs
                .Where(j => j.CareerId == careerId)
                .Include(j => j.Business)
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<Job> GetJobById(int id)
        {
            return await _dbContext.Jobs
                .AsNoTracking()
                .Include(j => j.Business)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<GetJobDashBoard> GetJobDashBoard(int businessId)
        {
            var listJobs = await _dbContext.Jobs
                .Where(j => j.BusinessId == businessId)
                .Select(j => new GetJobManagementDTO
                {
                    Id = j.Id,
                    CreatedDate = j.CreatedDate,
                    Status = j.Status,
                    ExpirationDate = j.ExpirationDate,
                    NumberRecruitment = j.NumberRecruitment,
                    Title = j.Title,
                }).OrderByDescending(j => j.Id).Take(5).ToListAsync();

            var total = await _dbContext.Jobs.CountAsync(j => j.BusinessId == businessId);

            var job = new GetJobDashBoard
            {
                Jobs = listJobs,
                TotalJob = total
            };

            return job;
        }

        public async Task<GetJobManagementListDTO> GetJobManagements(int? page, int businessId)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int itemsToSkip = (int)(page - 1) * pageSize;
            IQueryable<Job> jobQuery = _dbContext.Jobs;

            jobQuery = jobQuery.Where(j => j.BusinessId == businessId);

            var jobs = await jobQuery.Select(j => new GetJobManagementDTO
            {
                Id = j.Id,
                CreatedDate = j.CreatedDate,
                Title = j.Title,
                Status = j.Status
            }).ToListAsync();

            int total = await _dbContext.Jobs.CountAsync();


            return new GetJobManagementListDTO { Jobs = jobs, TotalJob = total };
        }

        public async Task<GetListJobDTO> GetJobs(int? page, string? query, string? jobType,
            decimal? minSalary, decimal? maxSalary, int? career, List<string> experience,
            string date, List<string> position, List<string> education)
        {
            if (page == null) page = 1;
            int pageSize = 6;
            int itemsToSkip = (int)(page - 1) * pageSize;
            IQueryable<Job> jobQuery = _dbContext.Jobs;

            if (!string.IsNullOrEmpty(query))
            {
                jobQuery = jobQuery.Where(x => x.Title.Contains(query));
            }

            if (jobType != "Job Type")
            {
                jobQuery = jobQuery.Where(x => x.JobType == jobType);
            }

            if (minSalary.HasValue)
            {
                jobQuery = jobQuery.Where(x => x.SalaryMin >= minSalary.Value);
            }

            if (maxSalary.HasValue)
            {
                jobQuery = jobQuery.Where(x => x.SalaryMax <= maxSalary.Value);
            }

            if (career != 0)
            {
                jobQuery = jobQuery.Where(x => x.CareerId == career.Value);
            }

            if (experience != null && experience.Any())
            {
                jobQuery = jobQuery.Where(x => experience.Contains(x.YearExpMin));
            }
            if (position != null && position.Any())
            {
                jobQuery = jobQuery.Where(x => position.Contains(x.CareerLevel));
            }

            if (education != null && education.Any())
            {
                jobQuery = jobQuery.Where(x => education.Contains(x.EducationLevelMin));
            }
            if (!string.IsNullOrEmpty(date) && date != "All")
            {
                DateTime currentDate = DateTime.Now.Date;

                switch (date)
                {
                    case "1 day":
                        DateTime oneDayAgo = currentDate.AddDays(-1);
                        jobQuery = jobQuery.Where(x => x.CreatedDate >= oneDayAgo);
                        break;
                    case "3 days":
                        DateTime threeDaysAgo = currentDate.AddDays(-3);
                        jobQuery = jobQuery.Where(x => x.CreatedDate >= threeDaysAgo);
                        break;
                    case "7 days":
                        DateTime sevenDaysAgo = currentDate.AddDays(-7);
                        jobQuery = jobQuery.Where(x => x.CreatedDate >= sevenDaysAgo);
                        break;
                    case "30 days":
                        DateTime thirtyDaysAgo = currentDate.AddDays(-30);
                        jobQuery = jobQuery.Where(x => x.CreatedDate >= thirtyDaysAgo);
                        break;
                    default:
                        break;
                }
            }

            var jobs = await jobQuery
                .Select(j => new GetJobDTO
                {
                    BusinessId = j.BusinessId,
                    FullName = j.Business.FullName,
                    LogoUrl = j.Business.LogoUrl,
                    Address = j.Business.Address,
                    Id = j.Id,
                    JobType = j.JobType,
                    Title = j.Title,
                    SalaryMax = j.SalaryMax,
                    SalaryMin = j.SalaryMin,
                    RequiredSkills = j.RequiredSkills,
                    ExpirationDate = j.ExpirationDate,
                    Description = j.Description
                })
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();
            
            int total = await _dbContext.Jobs.CountAsync();
            GetListJobDTO listJob = new GetListJobDTO { GetJobDTOs = jobs, Total = total };

            return listJob;
        }

        public async Task<List<GetJobDTO>> GetJobsFromBusiness(int businessId)
        {
            var jobs = await _dbContext.Jobs
                 .Select(j => new GetJobDTO
                 {
                     BusinessId = j.BusinessId,
                     FullName = j.Business.FullName,
                     LogoUrl = j.Business.LogoUrl,
                     Address = j.Business.Address,
                     Id = j.Id,
                     JobType = j.JobType,
                     Title = j.Title,
                     SalaryMax = j.SalaryMax,
                     SalaryMin = j.SalaryMin,
                     RequiredSkills = j.RequiredSkills,
                     ExpirationDate = j.ExpirationDate,
                     Description = j.Description
                 })
                 .Where(j => j.BusinessId == businessId)
                 .OrderByDescending(j => j.Id)
                 .ToListAsync();

            return jobs;
        }

        public async Task<GetJobWithBusinessDTO> GetJobWithBusiness(int id)
        {
            var job = await _dbContext.Jobs.Where(x => x.Id == id)
                .Select(x => new GetJobWithBusinessDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    BusinessId = x.BusinessId,
                    BusinessName = x.Business.FullName,
                    NumberRecruitment = x.NumberRecruitment,
                    AvatarUrl = x.Business.LogoUrl,
                    ExpiratedDate = x.ExpirationDate
                }).FirstOrDefaultAsync();

            return job;
        }

        public async Task<List<GetJobDTO>> GetNewJobs()
        {
            return await _dbContext.Jobs.Select(j => new GetJobDTO
            {
                BusinessId = j.BusinessId,
                FullName = j.Business.FullName,
                LogoUrl = j.Business.LogoUrl,
                Address = j.Business.Address,
                Id = j.Id,
                JobType = j.JobType,
                Title = j.Title,
                SalaryMax = j.SalaryMax,
                SalaryMin = j.SalaryMin,
                RequiredSkills = j.RequiredSkills,
                ExpirationDate = j.ExpirationDate,
                Description = j.Description
            }).OrderByDescending(j => j.Id)
            .Take(8).ToListAsync();
        }
    }
}
