using AutoMapper;
using Candidate.Application.DTOs.Education;
using Candidate.Application.DTOs.Experience;
using Candidate.Application.DTOs.Resume;
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
            CreateMap<Resume, ResumeDTO>().ReverseMap();
            CreateMap<Resume, CreateResumeDTO>().ReverseMap();
            CreateMap<Resume, UpdateResumeDTO>().ReverseMap();

            CreateMap<Education, CreateEducationDTO>().ReverseMap();
            CreateMap<Education, UpdateEducationDTO>().ReverseMap();

            CreateMap<Experience, CreateExperienceDTO>().ReverseMap();
            CreateMap<Experience, UpdateExperienceDTO>().ReverseMap();
        }

    }
}
