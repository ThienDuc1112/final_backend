using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class GetAppliedJobDashboard
    {
        public List<GetAppliedJobDTO> Jobs { get; set; } = new List<GetAppliedJobDTO>();
        public int ApplicationCount { get; set; }
        public int InterviewCount { get; set; }
    }
}
