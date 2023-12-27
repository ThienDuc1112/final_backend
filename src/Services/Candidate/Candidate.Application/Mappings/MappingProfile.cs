using AutoMapper;
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
        }

    }
}
