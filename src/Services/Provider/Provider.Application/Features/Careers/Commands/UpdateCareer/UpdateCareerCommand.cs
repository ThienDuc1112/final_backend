using MediatR;
using Provider.Application.DTOs.Career;

namespace Provider.Application.Features.Careers.Commands.UpdateCareer
{
    public class UpdateCareerCommand : IRequest<Unit>
    {
        public UpdateCareerDTO CareerDTO { get; set; }
    }
}
