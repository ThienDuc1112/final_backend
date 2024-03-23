using Business.Application.DTOs.BusinessInfor;
using MediatR;


namespace Business.Application.Features.BusinessInfors.Queries.GetBusinessById
{
    public class GetBusinessByIdQuery : IRequest<BusinessInforDTO>
    {
        public int Id { get; set; }
    }
}
