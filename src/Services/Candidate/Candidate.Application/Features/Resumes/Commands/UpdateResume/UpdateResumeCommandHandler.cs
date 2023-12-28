using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Exceptions;
using Candidate.Application.Responses;
using Candidate.Domain.Entities;
using MediatR;


namespace Candidate.Application.Features.Resumes.Commands.UpdateResume
{
    public class UpdateResumeCommandHandler : IRequestHandler<UpdateResumeCommand, BaseCommandResponse>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;

        public UpdateResumeCommandHandler(IResumeRepository resumeRepository, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateResumeValidator();
            var validationResult = await validator.ValidateAsync(request.ResumeDTO);

            if (validationResult.IsValid == false)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var resume = await _resumeRepository.GetById(request.ResumeDTO.Id);

                if(resume == null) { throw new NotFoundException(nameof(resume), request.ResumeDTO.Id); }

                _mapper.Map(request.ResumeDTO, resume);
                await _resumeRepository.Update(resume);
                response.Id = resume.Id;
                response.Success = true;
                response.Message = "Update resume successful";

            }
            return response;
        }
    }
}
