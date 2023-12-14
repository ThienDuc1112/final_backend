using AutoMapper;
using Provider.Application.DTOs.Career;
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
        }
    }
}
