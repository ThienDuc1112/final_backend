﻿using Candidate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Domain.Entities
{
    public class Resume : EntityBase
    {
        public string UserId { get; set; }
        public string CareerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Linkedln { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StatusOfEmployment { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public string Title { get; set; }

    }
}
