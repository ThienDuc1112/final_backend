using MediatR;


namespace Provider.Application.Features.Careers.Commands.DeleteCareer
{
    public class DeleteCareerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
