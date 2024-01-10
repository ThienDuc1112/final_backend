using AutoMapper;
using Business.Application.DTOs.Area;
using Business.Application.DTOs.BusinessInfor;
using Business.Application.DTOs.Job;
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

            CreateMap<BusinessInfor, BusinessInforDTO>()
                .ForMember(dest => dest.AreaDTOs, act => act.MapFrom(src => src.Areas))
               .ForMember(dest => dest.MediaDTOs, act => act.MapFrom(src => src.Medias))
                .ReverseMap();
            CreateMap<BusinessInfor, CreateBusinessInforDTO>().ReverseMap();
            CreateMap<BusinessInfor, UpdateBusinessInforDTO>().ReverseMap();

            CreateMap<Media, MediaDTO>().ReverseMap();
            CreateMap<Media, UploadMediaDTO>().ReverseMap();

            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Job, CreateJobDTO>().ReverseMap();
            CreateMap<Job, UpdateJobDTO>().ReverseMap();
            CreateMap<Job, GetJobDetailDTO>().ReverseMap();
        }
    }
}
