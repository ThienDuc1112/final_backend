using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.Job
{
    public class GetListJobDTO
    {
        public List<GetJobDTO> GetJobDTOs { get; set; } = new List<GetJobDTO>();
        public int Total { get; set; }
    }
}
