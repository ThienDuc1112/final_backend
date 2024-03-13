using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.Job;
using Business.Application.Features.Jobs.Queries.GetJobManagement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetListJobApp
{
    public class GetListJobAppQueryHandler : IRequestHandler<GetListJobAppQuery, List<GetJobAppDTO>>
    {

        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetListJobAppQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<List<GetJobAppDTO>> Handle(GetListJobAppQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetJobApp(request.BusinessId);
            var jobDTOs = _mapper.Map<List<GetJobAppDTO>>(jobs);
            return jobDTOs;
        }
    }
}
