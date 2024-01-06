using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Exceptions;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Medias.Commands.DeleteMedia
{
    public class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteMediaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            var media = await _unitOfWork.MediaRepository.GetById(request.Id);
            if (media == null)
            {
                throw new NotFoundException(nameof(Media), request.Id);
            }

            await _unitOfWork.MediaRepository.Delete(media);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
