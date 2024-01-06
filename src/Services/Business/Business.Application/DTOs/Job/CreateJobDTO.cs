

namespace Business.Application.DTOs.Job
{
    public class CreateJobDTO
    {
        public int CareerId { get; set; }
        public int BusinessId { get; set; }
        public string Title { get; set; }
        public int NumberRecruitment { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EducationLevelMin { get; set; }
        public string YearExpMin { get; set; }
        public string GenderRequirement { get; set; }
        public int LanguageRequirementId { get; set; }
        public string? Address { get; set; }
        public string JobType { get; set; }
        public string CareerLevel { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string Description { get; set; }
        public string Welfare { get; set; }
        public string Requirement { get; set; }
        public string? RequiredSkills { get; set; }
        public string Responsibilities { get; set; }
        public string Status { get; set; }
    }
}
