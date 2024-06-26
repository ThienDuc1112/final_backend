﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Domain.Entities
{
    public class LanguageOfResume
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey("ResumeId")]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
