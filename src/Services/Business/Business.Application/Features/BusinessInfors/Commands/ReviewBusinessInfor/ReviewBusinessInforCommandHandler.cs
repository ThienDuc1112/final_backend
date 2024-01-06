using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.BusinessInfor;
using Business.Application.Exceptions;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Commands.ReviewBusinessInfor
{
    public class ReviewBusinessInforCommandHandler : IRequestHandler<ReviewBusinessInforCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewBusinessInforCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ReviewBusinessInforCommand request, CancellationToken cancellationToken)
        {
            var businessInfor = await _unitOfWork.BusinessRepository.GetById(request.BusinessInforDTO.Id);

            if (businessInfor == null)
            {
                throw new NotFoundException(nameof(businessInfor), request.BusinessInforDTO.Id);
            }
            var business = _mapper.Map<BusinessInfor>(businessInfor);
            await _unitOfWork.BusinessRepository.AcceptOrReject(business);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
