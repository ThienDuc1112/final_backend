using AutoMapper;
using Business.Application.DTOs.Job;
using Business.Domain.Entities;
using Business.Grpc.Protos;

namespace Business.Grpc.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetJobWithBusinessDTO, JobModel>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.NumberRecruitment, act => act.MapFrom(src => src.NumberRecruitment))
                .ForMember(dest => dest.BusinessId, act => act.MapFrom(src => src.BusinessId))
                .ForMember(dest => dest.BusinessName, act => act.MapFrom(src => src.BusinessName))
                .ForMember(dest => dest.AvatarUrl, act => act.MapFrom(src => src.AvatarUrl))
                .ReverseMap();
               
                
        }
    }
}
