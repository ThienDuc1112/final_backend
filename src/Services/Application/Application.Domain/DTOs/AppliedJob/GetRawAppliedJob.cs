using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class GetRawAppliedJob
    {
        public List<RawAppliedJob> Jobs { get; set; } = new List<RawAppliedJob>();
        public int ApplicationCount { get; set; }
        public int InterviewCount { get; set; }
    }
}
