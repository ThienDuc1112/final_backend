using MediatR;
using Provider.Application.DTOs.Career;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Queries.GetCareerAdminList
{
    public class GetCareerAdminListQuery : IRequest<List<CareerDTO>>
    {
    }
}
