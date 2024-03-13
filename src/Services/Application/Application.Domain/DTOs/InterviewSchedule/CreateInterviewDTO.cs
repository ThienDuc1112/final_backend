using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.InterviewSchedule
{
    public class CreateInterviewDTO
    {
        public int AppliedJobId { get; set; }
        public DateTime InterviewTime { get; set; }

    }
}
