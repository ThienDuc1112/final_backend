using Microsoft.EntityFrameworkCore;
using Provider.Application.Contracts.Persistence;
using Provider.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ProviderDbContext _providerDbContext;

        public GenericRepository(ProviderDbContext providerDbContext)
        {
            _providerDbContext = providerDbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _providerDbContext.Set<T>().AddAsync(entity);
            await _providerDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _providerDbContext.Set<T>().Remove(entity);
            await _providerDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _providerDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _providerDbContext.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _providerDbContext.Entry(entity).State = EntityState.Modified;
            _providerDbContext.SaveChangesAsync();
        }
    }
}
