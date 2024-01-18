using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthenticationSever.Entities;
using System.Reflection.Emit;

namespace AuthenticationSever.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ManageUser>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ManageUser>(
               users =>
               {
                   users.HasMany(x => x.Claims)
                       .WithOne()
                       .HasForeignKey(x => x.UserId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Cascade);

                   users.ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
               });

        }

        public DbSet<ManageUser> ManageUsers { get; set; }

    }
}
