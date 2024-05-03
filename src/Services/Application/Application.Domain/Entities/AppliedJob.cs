using Application.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Entities
{
    public class AppliedJob : EntityBase
    {
        public string CandidateId { get; set; }
        public int JobId { get; set; }
        public int ResumeId { get; set; }
        public int BusinessId { get; set; }
        public string Status { get; set; }
        public string? Url { get; set; }
        public List<InterviewSchedule> Interviews { get; set; }
    }
}
