using Business.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Infrastructure.Persistance
{
    public class BusinessDbContext : AuditableDbContext
    {
        public BusinessDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusinessDbContext).Assembly);
        }

        public DbSet<Media> Medias { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<BusinessInfor> Businesses { get; set; }
        public DbSet<Area> Areas { get; set; }

    }
}
