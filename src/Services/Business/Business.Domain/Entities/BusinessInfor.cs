﻿using Business.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Entities
{
    public class BusinessInfor : EntityBase
    {
        public string? ShortName { get; set; }
        public string FullName { get; set; }
        public int FoundedYear { get; set; }
        public string BusinessSize { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseFont { get; set; }
        public string LicenseBack { get; set; }
        public string? LogoUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? FaceBookUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public bool IsApproved { get; set; } = false;
        public string UserId { get; set; }
    }
}
