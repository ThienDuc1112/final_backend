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
            services.AddScoped<CareerGrpcService>();

            return services;
        }
    }
}
