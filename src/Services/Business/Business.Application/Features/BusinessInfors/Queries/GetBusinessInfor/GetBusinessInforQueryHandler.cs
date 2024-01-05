using AutoMapper;
using Business.Application.Contracts;
using Business.Application.DTOs.BusinessInfor;
using Business.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Queries.GetBusinessInfor
{
    public class GetBusinessInforQueryHandler : IRequestHandler<GetBusinessInforQuery, BusinessInforDTO>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public GetBusinessInforQueryHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<BusinessInforDTO> Handle(GetBusinessInforQuery request, CancellationToken cancellationToken)
        {
            var businessInfor = _businessRepository.GetBusinessInforWithRelevant(request.Id);
            if(businessInfor == null)
            {
                throw new NotFoundException(nameof(businessInfor), request.Id);
            }
            var businessInforDTO = _mapper.Map<BusinessInforDTO>(businessInfor);
            return businessInforDTO;
        }
    }
}
