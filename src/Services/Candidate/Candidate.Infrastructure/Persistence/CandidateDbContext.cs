using Microsoft.EntityFrameworkCore;
using Candidate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Infrastructure.Persistence
{
    public class CandidateDbContext : AuditableDbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CandidateDbContext).Assembly);
        }

        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<LanguageOfResume> LanguageOfResumes { get; set; }
        public DbSet<SkillOfResume> SkillOfResumes { get; set; }
    }
}
