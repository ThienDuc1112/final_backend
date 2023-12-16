using Microsoft.EntityFrameworkCore;
using Provider.Infrastructure.Persistance;
using System;

namespace Provider.API.Extensions
{
   public static class HostExtensions
    {
        //public static void MigrateDatabase(this IServiceCollection services)
        //{
        //    using (var scope = services.BuildServiceProvider().CreateScope())
        //    {
        //        var dbContext = scope.ServiceProvider.GetRequiredService<ProviderDbContext>();
        //        dbContext.Database.Migrate();
        //        ProviderContextSeed.SeedAsync(dbContext).Wait();
        //    }
        //}

        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            try
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProviderDbContext>();

                if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    dbContext.Database.EnsureCreated();
                }
                ProviderContextSeed.SeedAsync(dbContext).Wait();
            }
            catch (Exception exception)
            {

                throw;
            }

            return host;
        }
    }
}
