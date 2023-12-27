using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.DTOs.Resume;
using Candidate.Application.GrpcServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Queries.GetResume
{
    public class GetResumeQueryHandler :IRequestHandler<GetResumeQuery, ResumeDTO>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;
        private readonly CareerGrpcService _careerGrpcService;

        public GetResumeQueryHandler(IResumeRepository resumeRepository, IMapper mapper, CareerGrpcService careerGrpcService)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
            _careerGrpcService = careerGrpcService;
        }

        public async Task<ResumeDTO> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            var resume = await _resumeRepository.GetById(request.ResumeId);
            if (resume == null)
            {
                throw new ArgumentException("not found");
            }
            var career = await _careerGrpcService.GetCareer(resume.CareerId);

            ResumeDTO resumeDTO = new ResumeDTO
            {
                NameCareer = career.Name,
                AvatarUrl = resume.AvatarUrl,
                FullName = resume.FullName,
                PhoneNumber = resume.PhoneNumber,
                Email = resume.Email,
                Linkedln = resume.Linkedln,
                Gender = resume.Gender,
                Country = resume.Country,
                DateOfBirth = resume.DateOfBirth,
                StatusOfEmployment = resume.StatusOfEmployment,
                Description = resume.Description,
                Title = resume.Title
            };
            return resumeDTO;
        }
    }
}
