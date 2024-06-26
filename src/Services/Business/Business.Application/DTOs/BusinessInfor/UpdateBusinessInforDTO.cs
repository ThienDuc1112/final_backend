﻿using Business.Application.DTOs.Area;
using Business.Application.DTOs.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.DTOs.BusinessInfor
{
    public class UpdateBusinessInforDTO
    {
        public int Id { get; set; }
        public string? ShortName { get; set; }
        public string FullName { get; set; }
        public int FoundedYear { get; set; }
        public string BusinessSize { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? LogoUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? FaceBookUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }

        //public List<CreateAreaDTO> AreaDTOs { get; set; }
        //public List<UploadMediaDTO> MediaDTOs { get; set; }
    }
}
