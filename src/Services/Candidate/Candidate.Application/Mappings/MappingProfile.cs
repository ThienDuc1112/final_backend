using AutoMapper;
using Candidate.Application.DTOs.Education;
using Candidate.Application.DTOs.Experience;
using Candidate.Application.DTOs.LanguageOfResume;
using Candidate.Application.DTOs.Resume;
using Candidate.Application.DTOs.SkillOfResume;
using Candidate.Application.GrpcServices;
using Candidate.Domain.Entities;
using Grpc.Net.Client;
using Provider.Grpc.Protos;

namespace Candidate.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Resume, ResumeDTO>()
                .ForMember(dest => dest.EducationsDTO, opt => opt.MapFrom(src => src.Educations))
                .ForMember(dest => dest.ExperiencesDTO, opt => opt.MapFrom(src => src.Experiences))
                .ReverseMap();
            CreateMap<Resume, CreateResumeDTO>().ReverseMap();
            CreateMap<Resume, UpdateResumeDTO>().ReverseMap();
            CreateMap<Resume, GetResumeDTO>().ReverseMap();
            CreateMap<Resume, HideResumeDTO>().ReverseMap();

            CreateMap<Education, EducationDTO>().ReverseMap();
            CreateMap<Education, CreateEducationDTO>().ReverseMap();
            CreateMap<Education, UpdateEducationDTO>().ReverseMap();

            CreateMap<Experience, ExperienceDTO>().ReverseMap();
            CreateMap<Experience, CreateExperienceDTO>().ReverseMap();
            CreateMap<Experience, UpdateExperienceDTO>().ReverseMap();

            CreateMap<SkillOfResume, CreateSkillOfResumeDTO>().ReverseMap();
            CreateMap<SkillOfResume, SkillOfResumeDTO>().ReverseMap();
            CreateMap<SkillOfResume, UpdateSkillOfResumeDTO>().ReverseMap();

            CreateMap<LanguageOfResume, CreateLanguageOfResumeDTO>().ReverseMap();
            CreateMap<LanguageOfResume, LanguageOfResumeDTO>().ReverseMap();
            CreateMap<LanguageOfResume, UpdateLanguageOfResumeDTO>().ReverseMap();
        }

    }
}
