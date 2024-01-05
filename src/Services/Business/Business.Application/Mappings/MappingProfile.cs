using AutoMapper;
using Business.Application.DTOs.Area;
using Business.Application.DTOs.BusinessInfor;
using Business.Application.DTOs.Media;
using Business.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Area, AreaDTO>().ReverseMap();
            CreateMap<Area, CreateAreaDTO>().ReverseMap();

            CreateMap<BusinessInfor, BusinessInforDTO>().ReverseMap();
            CreateMap<BusinessInfor, CreateBusinessInforDTO>().ReverseMap();

            CreateMap<Media, MediaDTO>().ReverseMap();
        }
    }
}
