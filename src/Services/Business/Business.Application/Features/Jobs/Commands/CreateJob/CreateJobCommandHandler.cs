using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Responses;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateJobValidator();
            var validationResult = await validator.ValidateAsync(request.JobDTO);

            if (!validationResult.IsValid)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation job failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                    var job = _mapper.Map<Job>(request.JobDTO);
                    await _unitOfWork.JobRepository.Add(job);
                    await _unitOfWork.Save();
                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = job.Id;
            }

            return response;
        }
    }
}
