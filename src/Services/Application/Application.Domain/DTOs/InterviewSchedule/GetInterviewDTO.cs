using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.InterviewSchedule
{
    public class GetInterviewDTO
    {
        public int Id { get; set; }
        public DateTime InterviewTime { get; set; }
        public bool IsSelected { get; set; }
    }
}
