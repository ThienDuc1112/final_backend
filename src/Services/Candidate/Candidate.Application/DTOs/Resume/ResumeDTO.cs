using Candidate.Application.DTOs.Education;
using Candidate.Application.DTOs.Experience;
using Candidate.Application.DTOs.LanguageOfResume;
using Candidate.Application.DTOs.SkillOfResume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.DTOs.Resume
{
    public class ResumeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Linkedln { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StatusOfEmployment { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string NameCareer { get; set; }

        public List<EducationDTO> EducationsDTO { get; set; }
        public List<ExperienceDTO> ExperiencesDTO { get; set; }
        public List<SkillOfResumeDTO> SkillOfResumeDTOs { get; set; }
        public List<LanguageOfResumeDTO> languageOfResumeDTOs { get; set; }
    }
}
