using AutoMapper;
using Provider.Domain.Entities;
using Provider.Grpc.Protos;

namespace Provider.Grpc.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Career, CareerModel>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
