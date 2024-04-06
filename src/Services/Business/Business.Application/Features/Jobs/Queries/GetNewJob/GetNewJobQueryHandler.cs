using Business.Application.Contracts;
using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetNewJob
{
    public class GetNewJobQueryHandler : IRequestHandler<GetNewJobQuery, List<GetJobDTO>>
    {
        private readonly IJobRepository _jobRepository;

        public GetNewJobQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<GetJobDTO>> Handle(GetNewJobQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetNewJobs();
            foreach (var job in jobs)
            {
                job.Skills = job.RequiredSkills.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return jobs;
        }
    }
}
