﻿using Provider.Application.DTOs.Skill;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Contracts.Persistence
{
    public interface ISkillRepository : IGenericRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetSkillsByCarrerId(int carrerId);
        Task<bool> IsExisted(string nameSkill);
        Task<List<Skill>> GetActiveSkills();
        Task<GetSkillAdminListDTO> GetSkillsByAdmin(int page, int carrerId);
    }
}
