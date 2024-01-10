using Business.Application.GrpcServices;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Business.Application
{
    public static class ApplicationServicesRegistration
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
