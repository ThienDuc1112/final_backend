using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class QueryAppliedJobDTO
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public int JobId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int AppliedNumber { get; set; }
        public int AcceptedNumber { get; set; }
    }
}
