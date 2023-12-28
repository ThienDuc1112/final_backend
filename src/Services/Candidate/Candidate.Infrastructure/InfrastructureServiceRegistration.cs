using Candidate.Application.Contracts.Persistence;
using Candidate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Candidate.Infrastructure.Repositories;

namespace Candidate.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CandidateDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("CandidateConnectionString"));
                    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IResumeRepository, ResumeRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IExperienceRepository, ExperienceRepository>();

            return services;
        }

    }
}
