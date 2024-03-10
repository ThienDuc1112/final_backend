using AutoMapper;
using Provider.Application.DTOs.Career;
using Provider.Application.DTOs.Language;
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
            CreateMap<Career, GetCareerDTO>().ReverseMap();

            CreateMap<Skill, SkillDTO>()
                .ForMember(dest => dest.CareerDTO, act => act.MapFrom(src => src.Career)).ReverseMap();
            CreateMap<Skill, CreateSkillDTO>().ReverseMap();
            CreateMap<Skill, UpdateSkillDTO>().ReverseMap();
            CreateMap<Skill, GetSkillDTO>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.NameSkill))
                .ReverseMap();

            CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<Language, CreateLanguageDTO>().ReverseMap();
        }
    }
}
