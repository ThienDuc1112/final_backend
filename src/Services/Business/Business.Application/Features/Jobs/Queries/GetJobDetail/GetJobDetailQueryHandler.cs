using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.Job;
using Business.Application.Exceptions;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetJobDetail
{
    public class GetJobDetailQueryHandler : IRequestHandler<GetJobDetailQuery, GetJobDetailDTO>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetJobDetailQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<GetJobDetailDTO> Handle(GetJobDetailQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetById(request.Id);
            if (job == null)
            {
                throw new NotFoundException(nameof(Job), request.Id);
            }

            var jobDTO = _mapper.Map<GetJobDetailDTO>(job);

            return jobDTO;
        }
    }
}
