using Application.Domain.DTOs.AppliedJob;
using Application.Domain.Entities;
using AutoMapper;

namespace Application.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppliedJob, CreateAppliedJobDTO>().ReverseMap();
            CreateMap<AppliedJob, GetAppliedJobDTO>().ReverseMap();
            CreateMap<QueryAppliedJobDTO, GetAppDetailDTO>().ReverseMap();
        }
    }
}
