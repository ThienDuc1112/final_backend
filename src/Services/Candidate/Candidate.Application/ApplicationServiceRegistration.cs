using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Provider.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Candidate.Application.GrpcServices;
using Candidate.Application.Mappings;

namespace Candidate.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddGrpcClient<CareerProtoService.CareerProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:GrpcUrl"]));
            services.AddGrpcClient<SkillProtoService.SkillProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:GrpcUrl"]));
            services.AddGrpcClient<LanguageProtoService.LanguageProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:GrpcUrl"]));
            services.AddScoped<CareerGrpcService>();
            services.AddScoped<SkillGrpcService>();
            services.AddScoped<LanguageGrpcService>();

            return services;
        }
    }
}
