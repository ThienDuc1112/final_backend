using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.Matching
{
    public class GetMatchingJob
    {
        public int BusinessId { get; set; }
        public string FullName { get; set; }
        public string LogoUrl { get; set; }
        public string Title { get; set; }
        public string JobType { get; set; }
        public long SalaryMin { get; set; }
        public long SalaryMax { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
