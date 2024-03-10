using Application.Infrastructure.Persistance;
using Application.Infrastructure.Repositories.Abstraction;
using Application.Infrastructure.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("ApplicationConnectionString"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAppliedJobRepository, AppliedJobRepository>();
            services.AddScoped<IFavoriteJobRepository, FavoriteJobRepository>();
            services.AddScoped<IInterviewScheduleRepository, InterviewScheduleRepository>();

            return services;
        }
    }
}
