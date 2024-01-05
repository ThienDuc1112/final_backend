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

namespace Business.Application.Features.BusinessInfors.Commands.CreateBusinessInfor
{
    public class CreateBusinessInforCommandHandler : IRequestHandler<CreateBusinessInforCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBusinessInforCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBusinessInforCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBusinessInforValidator();
            var validationResult = await validator.ValidateAsync(request.BusinessInforDTO);

            if (!validationResult.IsValid)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var businessInfor = _mapper.Map<BusinessInfor>(request.BusinessInforDTO);
                await _unitOfWork.BusinessRepository.Add(businessInfor);
                await _unitOfWork.Save();
                response.Id = businessInfor.Id;
                response.Success = true;
                response.Message = "Creation successful";
            }

            return response;
        }
    }
}
