using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetListJob
{
    public class GetListJobQueryHandler : IRequestHandler<GetListJobQuery, List<GetJobDTO>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetListJobQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<List<GetJobDTO>> Handle(GetListJobQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetJobs(
                                           request.Page,
                                           request.Query,
                                           request.JobType,
                                           request.MinSalary,
                                           request.MaxSalary,
                                           request.Career,
                                           request.Experience,
                                           request.Date,
                                           request.Position,
                                           request.Education
                                           );
            foreach(var job in jobs)
            {
                job.Skills = job.RequiredSkills.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return jobs;

        }
    }
}
