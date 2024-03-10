using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.Job
{
    public class GetJobDTO
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string LogoUrl { get; set; }
        public string Title { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string JobType { get; set; }
        public List<string> Skills { get; set; }
        public string RequiredSkills { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string Description { get; set; }

    }
}
