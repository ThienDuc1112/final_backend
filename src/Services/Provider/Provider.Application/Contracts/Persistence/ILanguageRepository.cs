using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Contracts.Persistence
{
    public interface ILanguageRepository : IGenericRepository<Language>
    {
        Task<bool> IsExisted(string name, string level);
        Task RemoveLanguage(int id);
        Task AllowLanguage(int id);
        
    }
}
