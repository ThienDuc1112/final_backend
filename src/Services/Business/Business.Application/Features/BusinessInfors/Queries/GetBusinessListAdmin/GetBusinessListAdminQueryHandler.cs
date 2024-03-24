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

namespace Business.Application.Features.Jobs.Queries.GetBusinessListAdmin
{
    public class GetBusinessListAdminQueryHandler : IRequestHandler<GetBusinessListAdminQuery, GetBusinessListAdminDTO>
    {
        private readonly IBusinessRepository _businessRepository;

        public GetBusinessListAdminQueryHandler(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public async Task<GetBusinessListAdminDTO> Handle(GetBusinessListAdminQuery request, CancellationToken cancellationToken)
        {
            var data = await _businessRepository.GetBusinessListAdmin(request.Page, request.Status);
            if(data == null)
            {
                throw new NotFoundException(nameof(GetBusinessListAdminQuery), 0);
            }
            return data;
        }
    }
}
