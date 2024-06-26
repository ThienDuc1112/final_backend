﻿using Candidate.Domain.Entities;
using Candidate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Contracts.Persistence
{
    public interface IEducationRepository : IGenericRepository<Education>
    {
        Task<List<LevelEducation>> GetLevelEducationList(int idResume);
    }
}
