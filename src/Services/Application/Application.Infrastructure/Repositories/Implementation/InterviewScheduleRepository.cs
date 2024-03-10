﻿using Application.Domain.Entities;
using Application.Infrastructure.Persistance;
using Application.Infrastructure.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Repositories.Implementation
{
    public class InterviewScheduleRepository : GenericRepository<InterviewSchedule>, IInterviewScheduleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public InterviewScheduleRepository(ApplicationDbContext DbContext) : base(DbContext)
        {
            _dbContext = DbContext;
        }
    }
}
