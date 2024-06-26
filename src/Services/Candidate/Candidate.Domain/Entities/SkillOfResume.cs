﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Domain.Entities
{
    public class SkillOfResume
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        [ForeignKey("ResumeId")]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
