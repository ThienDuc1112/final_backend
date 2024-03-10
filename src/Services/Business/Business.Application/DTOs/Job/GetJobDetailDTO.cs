using Business.Application.DTOs.BusinessInfor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.Job
{
    public class GetJobDetailDTO
    {
        public int Id { get; set; }
        public string CareerName { get; set; }
        public string Title { get; set; }
        public int NumberRecruitment { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EducationLevelMin { get; set; }
        public string YearExpMin { get; set; }
        public string GenderRequirement { get; set; }
        public string LanguageRequirementName { get; set; }
        public string LanguageRequirementLevel { get; set; }
        public string CreatedDate { get; set; }
        public string? Address { get; set; }
        public string JobType { get; set; }
        public string CareerLevel { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string Description { get; set; }
        public string Welfare { get; set; }
        public string Requirement { get; set; }
        public string requiredSkills { get; set; }
        public string Responsibilities { get; set; }
        public string Status { get; set; }
        public GetBusinessPartDTO GetBusinessPartDTO { get; set; }
    }
}
