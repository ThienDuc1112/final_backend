using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Exceptions;
using Business.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Commands.UpdateBusinessInfor
{
    public class UpdateBusinessInforCommandHandler : IRequestHandler<UpdateBusinessInforCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBusinessInforCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateBusinessInforCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateBusinessInforValidator();
            var validationResult = await validator.ValidateAsync(request.BusinessInforDTO);

            if (!validationResult.IsValid)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
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
                response.Id = businessInfor.Id;
                response.Success = true;
                response.Message = "Update successful";
            }

            return response;
        }
    }
}
