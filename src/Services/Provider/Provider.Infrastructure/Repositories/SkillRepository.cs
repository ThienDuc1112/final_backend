using Microsoft.EntityFrameworkCore;
using Provider.Application.Contracts.Persistence;
using Provider.Domain.Entities;
using Provider.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Repositories
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        private readonly ProviderDbContext _dbcontext;
        public SkillRepository(ProviderDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IEnumerable<Skill>> GetSkillsByCarrerId(int carrerId)
        {
            return await _dbcontext.Skills.Where(s => s.Career.Id == carrerId)
                .Select(s => new Skill
                {
                    NameSkill = s.NameSkill,
                    Career = s.Career,
                    Id = s.Id
                }).ToListAsync();

        }

        public async Task<bool> IsExisted(string nameSkill)
        {
            var skill = await _dbcontext.Skills.AnyAsync(s => s.NameSkill == nameSkill);
            return skill;
        }
    }
}
