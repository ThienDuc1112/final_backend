using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Exceptions;
using Business.Application.Responses;
using MediatR;

namespace Business.Application.Features.Jobs.Commands.UpdateJob
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateJobValidator();
            var validationResult = await validator.ValidateAsync(request.JobDTO);

            if (!validationResult.IsValid)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Update job Failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            }
            else
            {
                var job = await _unitOfWork.JobRepository.GetById(request.JobDTO.Id);

                if (job == null)
                {
                    throw new NotFoundException(nameof(job), request.JobDTO.Id);
                }

                _mapper.Map(request.JobDTO, job);
                await _unitOfWork.JobRepository.Update(job);
                await _unitOfWork.Save();
                response.Id = job.Id;
                response.Success = true;
                response.Message = "Update Successful";
            }

            return response;
        }
    }
}
