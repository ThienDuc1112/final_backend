using Microsoft.EntityFrameworkCore;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Persistance
{
    public class ProviderDbContext : AuditableDbContext
    {
        public ProviderDbContext(DbContextOptions<ProviderDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProviderDbContext).Assembly);
        }

        public DbSet<Career> Careers { get; set; }
        public DbSet<Skill> Skills { get; set; }

    }
}
