using AutoMapper;
using Business.Domain.Entities;
using Business.Grpc.Protos;

namespace Business.Grpc.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Job, JobModel>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.NumberRecruitment, act => act.MapFrom(src => src.NumberRecruitment))
                .ReverseMap();
        }
    }
}
