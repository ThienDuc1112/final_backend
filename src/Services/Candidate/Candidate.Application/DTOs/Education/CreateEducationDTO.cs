using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.DTOs.Education
{
    public class CreateEducationDTO
    {
        public string UniversityName { get; set; }
        public string Degree { get; set; }
        public string Major { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ResumeId { get; set; }
    }
}
