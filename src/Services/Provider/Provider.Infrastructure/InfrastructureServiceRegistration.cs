using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
                {
                    options.UseSqlServer(configuration.GetConnectionString("ProviderConnectionString"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();

            return services;
        }
    }
}
