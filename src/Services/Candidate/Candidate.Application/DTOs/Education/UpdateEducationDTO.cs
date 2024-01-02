using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.DTOs.Education
{
    public class UpdateEducationDTO
    {
        public int Id { get; set; }
        public string UniversityName { get; set; }
        public string Degree { get; set; }
        public string Major { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
