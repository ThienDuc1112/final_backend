
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Persistance
{
    public class ProviderContextSeed
    {
        public static async Task SeedAsync(ProviderDbContext providerDbContext)
        {
            if (!providerDbContext.Careers.Any())
            {
                providerDbContext.Careers.AddRange(GetCareer());
                await providerDbContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Career> GetCareer()
        {
            return new List<Career>
            {
                new Career() {Name = "Information technology", Description = "A major relating to store and handle information by applying technologies",
                IsAllowed = true}
            };
        }
     
    }
}
