using AuthenticationSever.ExtensionGrant;
using AuthenticationSever.Interface;
using AuthenticationSever.Interface.Processors;
using AuthenticationSever.Processor;
using AuthenticationSever.Providers;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationSever.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices<TUser>(this IServiceCollection services) where TUser : IdentityUser, new()
        {
            services.AddScoped<INonEmailUserProcessor, NonEmailUserProcessor<TUser>>();
            services.AddScoped<IEmailUserProcessor, EmailUserProcessor<TUser>>();
            services.AddScoped<IExtensionGrantValidator, ExternalAuthenticationGrant<TUser>>();
            services.AddSingleton<HttpClient>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IProviderRepository, ProviderRepository>();
            return services;
        }

        public static IServiceCollection AddProviders<TUser>(this IServiceCollection services) where TUser : IdentityUser, new()
        {
            services.AddTransient<IGoogleAuthProvider, GoogleAuthProvider<TUser>>();
            return services;
        }
    }
}
