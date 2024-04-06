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

namespace Business.Application.Features.Jobs.Queries.GetDashBoardJob
{
    public class GetDashBoardJobQueryHandler : IRequestHandler<GetDashBoardJobQuery, GetJobDashBoard>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetDashBoardJobQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<GetJobDashBoard> Handle(GetDashBoardJobQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetJobDashBoard(request.BusinessId);
            if(job == null)
            {
                throw new NotFoundException(nameof(Job), request.BusinessId);
            }

            return job;
        }
    }
}
