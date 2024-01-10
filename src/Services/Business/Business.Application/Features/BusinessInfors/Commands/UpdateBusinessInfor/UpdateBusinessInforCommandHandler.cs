using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Exceptions;
using Business.Application.Features.Areas.Commands.CreateArea;
using Business.Application.Features.Medias.Commands.UploadMedia;
using Business.Application.Responses;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Commands.UpdateBusinessInfor
{
    public class UpdateBusinessInforCommandHandler : IRequestHandler<UpdateBusinessInforCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBusinessInforCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBusinessInforCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBusinessInforValidator();
            var validationResult = await validator.ValidateAsync(request.BusinessInforDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            else
            {
                var businessInfor = await _unitOfWork.BusinessRepository.GetById(request.BusinessInforDTO.Id);

                if (businessInfor == null)
                {
                    throw new NotFoundException(nameof(businessInfor), request.BusinessInforDTO.Id);
                }

                var areaValidator = new CreateAreaValidator();
                var validationTasks = request.BusinessInforDTO.AreaDTOs.Select(dto => areaValidator.ValidateAsync(dto));
                var validationResults = await Task.WhenAll(validationTasks);

                var mediaValidator = new UploadMediaValidator();
                var mediaValidationTasks = request.BusinessInforDTO.MediaDTOs.Select(dto => mediaValidator.ValidateAsync(dto));
                var mediaValidationResults = await Task.WhenAll(mediaValidationTasks);

                if (validationResults.Any(result => result.IsValid == false)||
                    mediaValidationResults.Any(result => result.IsValid == false))
                {
                    var errors = validationResults.SelectMany(result => result.Errors)
                                                 .Select(error => error.ErrorMessage)
                                                 .ToList();
                    var error2s = mediaValidationResults.SelectMany(result => result.Errors)
                                                .Select(error => error.ErrorMessage)
                                                .ToList();
                    foreach (var e in errors)
                    {
                        throw new ArgumentException(e);
                    }
                    foreach (var e in error2s)
                    {
                        throw new ArgumentException(e);
                    }
                }
                else
                {
                    foreach(var area in request.BusinessInforDTO.AreaDTOs)
                    {
                        if(await _unitOfWork.AreaRepository.IsExisted(area.BusinessId, area.CareerId)==false)
                        {
                           var obj = _mapper.Map<Area>(area);
                            await _unitOfWork.AreaRepository.Add(obj);
                        }
                    }
                    foreach (var media in request.BusinessInforDTO.MediaDTOs)
                    {
                            if(media.Id == 0)
                           {
                            var obj = _mapper.Map<Media>(media);
                            await _unitOfWork.MediaRepository.Add(obj);
                           }
                           
                    }

                    _mapper.Map(request.BusinessInforDTO, businessInfor);
                    await _unitOfWork.BusinessRepository.Update(businessInfor);
                    await _unitOfWork.Save();
                }
            }

            return Unit.Value;
        }
    }
}
