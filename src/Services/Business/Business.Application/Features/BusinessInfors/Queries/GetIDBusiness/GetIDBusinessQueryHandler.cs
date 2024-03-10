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

namespace Business.Application.Features.BusinessInfors.Queries.GetIDBusiness
{
    public class GetIDBusinessQueryHandler : IRequestHandler<GetIDBusinessQuery, GetBusinessIdDTO>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public GetIDBusinessQueryHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<GetBusinessIdDTO> Handle(GetIDBusinessQuery request, CancellationToken cancellationToken)
        {
            var businessInfo = await _businessRepository.GetBusinessID(request.userId);
            if (businessInfo == null)
            {
                throw new NotFoundException(nameof(businessInfo), request.userId);
            }
            var business = _mapper.Map<GetBusinessIdDTO>(businessInfo);

            return business;
        }
    }
}
