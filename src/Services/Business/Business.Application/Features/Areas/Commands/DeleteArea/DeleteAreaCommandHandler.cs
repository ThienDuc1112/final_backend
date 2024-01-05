using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Areas.Commands.DeleteArea
{
    public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _unitOfWork.AreaRepository.GetById(request.Id);
            if (area == null)
            {
                throw new NotFoundException(nameof(area), request.Id);
            }

            await _unitOfWork.AreaRepository.Delete(area);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
