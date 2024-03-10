using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetJobManagement
{
    public class GetJobManagementQueryHandler : IRequestHandler<GetJobManagementQuery, List<GetJobManagementDTO>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetJobManagementQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<List<GetJobManagementDTO>> Handle(GetJobManagementQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetJobManagements(request.Page, request.BusinessId);
            return jobs;
        }
    }
}
