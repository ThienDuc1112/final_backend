using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Responses;
using Candidate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Commands.CreateResume
{
    public class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, BaseCommandResponse>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;

        public CreateResumeCommandHandler(IResumeRepository resumeRepository, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateResumeValidator();
            var validationResult = await validator.ValidateAsync(request.ResumeDTO);

            if (validationResult.IsValid == false)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var resume = _mapper.Map<Resume>(request.ResumeDTO);
                await _resumeRepository.Add(resume);
                response.Id = resume.Id;
                response.Success = true;
                response.Message = "Creation resume successful";

            }
            return response;

        }
    }
}
