using Candidate.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Domain.Entities
{
    public class Experience : EntityBase
    {
        public string Company { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public string Responsibility { get; set; }
        [ForeignKey("ResumeId")]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
