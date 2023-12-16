using AutoMapper;
using Provider.Application.DTOs.Career;
using Provider.Application.DTOs.Skill;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Career, CareerDTO>().ReverseMap();
            CreateMap<Career, CreateCareerDTO>().ReverseMap();
            CreateMap<Career, UpdateCareerDTO>().ReverseMap();

            CreateMap<Skill, SkillDTO>()
                .ForMember(dest => dest.CareerDTO, act => act.MapFrom(src => src.Career)).ReverseMap();
            CreateMap<Skill, CreateSkillDTO>().ReverseMap();
                //.ForMember(dest => dest.CareerId, act => act.MapFrom(src => src.Career.Id))
                //.ReverseMap();
            CreateMap<Skill, UpdateSkillDTO>().ReverseMap();
        }
    }
}
