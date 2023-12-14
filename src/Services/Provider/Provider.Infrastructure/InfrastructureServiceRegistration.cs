using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Application.Contracts.Persistence;
using Provider.Infrastructure.Persistance;
using Provider.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProviderDbContext>(
                options =>
                options.UseSqlServer(configuration.GetConnectionString("ProviderConnectionString")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();

            return services;
        }
    }
}
