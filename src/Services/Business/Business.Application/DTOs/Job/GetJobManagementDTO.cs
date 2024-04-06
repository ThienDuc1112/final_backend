using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.Job
{
    public class GetJobManagementDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int NumberRecruitment { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class GetJobManagementListDTO
    {
        public List<GetJobManagementDTO> Jobs { get; set; } = new List<GetJobManagementDTO>();
        public int TotalJob { get; set; }
    }
}
