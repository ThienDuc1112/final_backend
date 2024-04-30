using Candidate.Application.Contracts.Persistence;
using Candidate.Application.DTOs.Education;
using Candidate.Application.DTOs.Experience;
using Candidate.Application.DTOs.Resume;
using Candidate.Domain.Entities;
using Candidate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Resume> FindResumeById(int id)
        {
            var resume = await _dbcontext.Resumes
                .Where(r => r.Id == id)
                .Select(r => new Resume
                {
                    Id = r.Id,
                    CareerId = r.CareerId,
                    FullName = r.FullName,
                    PhoneNumber = r.PhoneNumber,
                    Email = r.Email,
                    Linkedln = r.Linkedln,
                    Gender = r.Gender,
                    Country = r.Country,
                    DateOfBirth = r.DateOfBirth,
                    StatusOfEmployment = r.StatusOfEmployment,
                    AvatarUrl = r.AvatarUrl,
                    Description = r.Description,
                    Title = r.Title,
                    AdditionalSkills = r.AdditionalSkills,
                    Educations = r.Educations.Select(e => new Education
                    {
                        Id = e.Id,
                        UniversityName = e.UniversityName,
                        Degree = e.Degree,
                        Major = e.Major,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Description = e.Description
                    }).ToList(),
                    Experiences = r.Experiences.Select(e => new Experience
                    {
                        Id = e.Id,
                        Company = e.Company,
                        Title = e.Title,
                        Responsibility = e.Responsibility,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate
                    }).ToList(),
                    Skills = r.Skills,
                    Languages = r.Languages
                }).FirstOrDefaultAsync();

            return resume;
        }

        public async Task<List<Resume>> FindResumeByUserId(string userId)
        {
            var resume = await _dbcontext.Resumes
                .Where(s => s.UserId == userId && s.IsPublic == true).ToListAsync();
            return resume;
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
