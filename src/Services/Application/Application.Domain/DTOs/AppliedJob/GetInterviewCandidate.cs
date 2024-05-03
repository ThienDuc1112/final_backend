using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class GetInterviewCandidate
    {
        public int Id { get; set; }
        public string CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string CandidateAvatar { get; set; }
        public string CandidateTitle { get; set; }
        public DateTime AppliedTime { get; set; }
        public string MeetingLink { get; set; }
        public DateTime MeetingTime { get; set; }
        public int ResumeId { get; set; }
    }
}
