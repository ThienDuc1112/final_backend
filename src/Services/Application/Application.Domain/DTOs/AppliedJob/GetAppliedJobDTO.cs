﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.DTOs.AppliedJob
{
    public class GetAppliedJobDTO
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string AvatarUrl { get; set; }

    }
}
