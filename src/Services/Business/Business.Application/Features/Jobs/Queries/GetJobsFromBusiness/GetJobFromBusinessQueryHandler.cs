using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.Job;
using Business.Application.Exceptions;
using Business.Application.Features.Jobs.Queries.GetListJob;
using Business.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetJobsFromBusiness
{
    public class GetJobFromBusinessQueryHandler : IRequestHandler<GetJobFromBusinessQuery, List<GetJobDTO>>
    {

        private readonly IJobRepository _jobRepository;


        public GetJobFromBusinessQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
        }
        public async Task<List<GetJobDTO>> Handle(GetJobFromBusinessQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetJobsFromBusiness(request.BusinessId);
            if (jobs == null)
            {
                throw new NotFoundException(nameof(Job), request.BusinessId);
            }
                                           
            foreach (var job in jobs)
            {
                job.Skills = job.RequiredSkills.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return jobs;
        }
    }
}
