using AutoMapper;
using Candidate.Domain.Entities;
using Candidate.Grpc.Protos;

namespace Candidate.Grpc.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Resume, ResumeModel>()
                .ForMember(src => src.Id, act => act.MapFrom(src => src.Id))
                .ForMember(src => src.FullName, act => act.MapFrom(src => src.FullName))
                .ForMember(src => src.Title, act => act.MapFrom(src => src.Title))
                .ForMember(src => src.Email, act => act.MapFrom(src => src.Email))
                .ForMember(src => src.AvatarUrl, act => act.MapFrom(src => src.AvatarUrl))
                .ReverseMap();
        }
    }
}
