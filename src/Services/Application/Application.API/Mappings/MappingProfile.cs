using Application.Domain.DTOs.AppliedJob;
using Application.Domain.DTOs.InterviewSchedule;
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
            CreateMap<AppliedJob, UpdateAppliedJobDTO>().ReverseMap();

            CreateMap<InterviewSchedule, CreateInterviewDTO>().ReverseMap();
            CreateMap<InterviewSchedule, GetInterviewDTO>().ReverseMap();
            CreateMap<InterviewSchedule, UpdateAppliedJobDTO>();


        }
    }
}
