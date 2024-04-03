using Microsoft.EntityFrameworkCore;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Skill;
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

        public async Task<List<Skill>> GetActiveSkills()
        {
            return await _dbcontext.Skills.Where(s => s.CreatedBy == "admin")
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }

        public async Task<GetSkillAdminListDTO> GetSkillsByAdmin(int page, int carrerId)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;

            IQueryable<Skill> query =  _dbcontext.Skills;
            if(carrerId != 0)
            {
                query = query.Where(s => s.CareerId == carrerId);
            }

            var skillList = await query
                .Select(s => new GetSkillAdminDTO
                {
                    Id = s.Id,
                    CareerName = s.Career.Name,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    Name = s.NameSkill
                })
                .Skip(skip)
                .Take(pageSize)
                .OrderByDescending(s => s.Id)
                .AsNoTracking()
                .ToListAsync();
            var total = await _dbcontext.Skills.CountAsync();

            GetSkillAdminListDTO skills = new GetSkillAdminListDTO
            {
                GetSkillAdminDTOs = skillList,
                TotalNumber = total,
            };

            return skills;
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
