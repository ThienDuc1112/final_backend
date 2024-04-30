using Microsoft.EntityFrameworkCore;
using Provider.Application.Contracts.Persistence;
using Provider.Application.Exceptions;
using Provider.Domain.Entities;
using Provider.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Repositories
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        private readonly ProviderDbContext _dbcontext;
        public LanguageRepository(ProviderDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task AllowLanguage(int id)
        {
            Language language = await _dbcontext.Languages.FirstOrDefaultAsync(x => x.Id == id);
            if (language != null)
            {
                if(language.IsAvailable == true)
                {
                    language.IsAvailable = false;
                }
                else
                {
                    language.IsAvailable = true;
                }
                _dbcontext.Entry(language).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("This language doesn't exist");
            }
        }

        public async Task<IEnumerable<Language>> GetActiveLanguages()
        {
            return await _dbcontext.Languages.GroupBy(l => l.LanguageName)
                    .Select(g => g.First()).ToListAsync();
        }

        public async Task<bool> IsExisted(string name, string level)
        {
            var existedLanguage = _dbcontext.Languages.AnyAsync(l => l.LanguageName == name && l.Level == level);
            return await existedLanguage;
        }

        public async Task RemoveLanguage(int id)
        {
            Language language = await _dbcontext.Languages.FirstOrDefaultAsync(x => x.Id == id);
            if (language != null)
            {
                language.IsAvailable = false;
                _dbcontext.Entry(language).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("This language doesn't exist");
            }
        }
    }
}
