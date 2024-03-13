using Application.Domain.DTOs.InterviewSchedule;
using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Repositories.Abstraction
{
    public interface IInterviewScheduleRepository : IGenericRepository<InterviewSchedule>
    {

        Task<List<InterviewSchedule>> GetInterviewsByApp(int appId);
    }
}
