using Business.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Entities
{
    public class Job : EntityBase
    {
        public int CareerId { get; set; }
        [ForeignKey("BusinessId")]
        public int BusinessId { get; set; }
        public string Title { get; set; }
        public int NumberRecruitment { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EducationLevelMin { get; set; }
        public string YearExpMin { get; set; }
        public string GenderRequirement { get; set; }
        public string LanguageRequirement { get; set; }
        public string? Address { get; set; }
        public string JobType { get; set; }
        public string ContractType { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string Description { get; set; }
        public string Welfare { get; set; }
        public string Requirement { get; set; }
        public string? RequiredSkills { get; set; }
        public string Responsibilities { get; set; }
        public string Status { get; set; } = "active";
        public Business Business { get; set; }
    }
}
