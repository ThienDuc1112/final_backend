using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.DTOs.LanguageOfResume;
using Candidate.Application.DTOs.Resume;
using Candidate.Application.DTOs.SkillOfResume;
using Candidate.Application.Exceptions;
using Candidate.Application.Features.Resumes.Queries.GetResume;
using Candidate.Application.GrpcServices;
using Candidate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Queries.GetResumeByUser
{
    public class GetResumeByQueryHandler :IRequestHandler<GetResumeByUserQuery, List<GetResumeDTO>>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;

        public GetResumeByQueryHandler(IResumeRepository resumeRepository, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        public async Task<List<GetResumeDTO>> Handle(GetResumeByUserQuery request, CancellationToken cancellationToken)
        {
            var resume = await _resumeRepository.FindResumeByUserId(request.UserId);
            if (resume == null)
            {
                throw new NotFoundException(nameof(Resume), request.UserId);
            }

            var resumeDTOs = _mapper.Map<List<GetResumeDTO>>(resume);

            return resumeDTOs;
        }
    }
}
