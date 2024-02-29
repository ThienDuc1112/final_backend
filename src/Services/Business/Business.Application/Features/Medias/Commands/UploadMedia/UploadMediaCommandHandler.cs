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
            var validationResult = await validator.ValidateAsync(request.MediaDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
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
