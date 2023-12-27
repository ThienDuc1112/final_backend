using Candidate.Infrastructure.Persistence;

namespace Candidate.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            try
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CandidateDbContext>();

                if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    dbContext.Database.EnsureCreated();
                }
            }
            catch (Exception exception)
            {

                throw;
            }

            return host;
        }
    }
}
