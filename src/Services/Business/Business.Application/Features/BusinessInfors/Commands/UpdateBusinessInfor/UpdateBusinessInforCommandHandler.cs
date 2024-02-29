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

                    _mapper.Map(request.BusinessInforDTO, businessInfor);
                    await _unitOfWork.BusinessRepository.Update(businessInfor);
                    await _unitOfWork.Save();
                
            }

            return Unit.Value;
        }
    }
}
