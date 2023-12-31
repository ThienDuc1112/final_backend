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
    public class ResumeWithRelatedDTO
    {
        public CreateResumeDTO Resume { get; set; }
        public List<CreateEducationDTO> Educations { get; set; }
        public List<CreateExperienceDTO> Experiences { get; set; }
        public List<CreateSkillOfResumeDTO> SkillOfResumes { get; set; }
        public List<CreateLanguageOfResumeDTO> LanguageOfResumes { get; set; }  
    }
}
