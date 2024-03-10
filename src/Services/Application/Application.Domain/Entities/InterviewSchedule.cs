using Application.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Entities
{
    public class InterviewSchedule : EntityBase
    {
        public int AppliedJobId { get; set; }
        public DateTime InterviewTime { get; set; }
        public bool IsSelected { get; set; }

        [ForeignKey("AppliedJobId")]
        public AppliedJob AppliedJob { get; set; }
    }
}
