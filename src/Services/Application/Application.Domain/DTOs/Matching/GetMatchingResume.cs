using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.Matching
{
    public class GetMatchingResume
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string AdditionalSkill { get; set; }
        public int CareerId { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public List<string> EducationDegree { get; set; } = new List<string>();
        public List<int> Languages { get; set; } = new List<int>();
        public double ExperienceYear { get; set; }
    }
}
