using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Features.BusinessInfors.Commands.CreateBusinessInfor;
using Business.Application.Responses;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Medias.Commands.UploadMedia
{
    public class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UploadMediaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UploadMediaValidator();
            var validatorTasks = request.MediaDTO.Select(dto => validator.ValidateAsync(dto));
            var validationResults = await Task.WhenAll(validatorTasks);

            if (validationResults.Any(result => !result.IsValid))
            {
                var errors = validationResults.SelectMany(result => result.Errors);
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = (List<string>)errors;
            }
            else
            {
                var media = _mapper.Map<Media>(request.MediaDTO);
                await _unitOfWork.MediaRepository.Add(media);
                await _unitOfWork.Save();
                response.Id = media.Id;
                response.Success = true;
                response.Message = "Creation successful";
            }

            return response;
        }
    }
}
