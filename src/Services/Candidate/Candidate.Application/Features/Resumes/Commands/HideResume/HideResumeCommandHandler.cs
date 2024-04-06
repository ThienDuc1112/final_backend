using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using Candidate.Application.Features.Resumes.Commands.UpdateResume;
using Candidate.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.Resumes.Commands.HideResume
{
    public class HideResumeCommandHandler : IRequestHandler<HideResumeCommand, BaseCommandResponse>
    {

        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;

        public HideResumeCommandHandler(IResumeRepository resumeRepository, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(HideResumeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new HideResumeValidator();
            var validationResult = await validator.ValidateAsync(request.HideResumeDTO);

            if (validationResult.IsValid == false)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var resume = await _resumeRepository.GetById(request.HideResumeDTO.Id);

                if (resume == null) { throw new NotFoundException(nameof(resume), request.HideResumeDTO.Id); }

                _mapper.Map(request.HideResumeDTO, resume);
                await _resumeRepository.Update(resume);
                response.Id = resume.Id;
                response.Success = true;
                response.Message = "Update resume successful";
            }

            return response;
        }
    }
}
