using Business.Application.Contracts;
using Business.Infrastructure.Persistance;
using Business.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BusinessDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("BusinessConnectionString"));
                });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IJobRepository, JobRepository>();

            return services;
        }
    }
}
