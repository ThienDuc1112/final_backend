using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class GetAppliedJobNumberDTO
    {
        public int JobId { get; set; }
        public int AppliedNumber { get; set; }
        public int AcceptedNumber { get; set; }
    }
}
