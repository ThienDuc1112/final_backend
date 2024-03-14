using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.Job
{
    public class GetJobWithBusinessDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberRecruitment { get; set; }
        public string BusinessName { get; set; }
        public int BusinessId { get; set; }
        public string AvatarUrl { get; set; }
    }
}
