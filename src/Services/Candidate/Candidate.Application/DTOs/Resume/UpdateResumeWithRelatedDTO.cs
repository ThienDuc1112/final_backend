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
    public class UpdateResumeWithRelatedDTO
    {
        public UpdateResumeDTO Resume { get; set; }
        public List<UpdateEducationDTO> Educations { get; set; }
        public List<UpdateExperienceDTO> Experiences { get; set; }
        public List<UpdateSkillOfResumeDTO> SkillOfResumes { get; set; }
        public List<UpdateLanguageOfResumeDTO> LanguageOfResumes { get; set; }
    }
}
