using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.Job
{
    public class GetJobDashBoard
    {
        public int TotalJob { get; set; }
        public List<GetJobManagementDTO> Jobs { get; set; } = new List<GetJobManagementDTO>();
    }
}
