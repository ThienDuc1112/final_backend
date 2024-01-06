using Business.Application.Contracts;
using Business.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Commands.DeleteJob
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteJobCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var deletedJob = await _unitOfWork.JobRepository.GetById(request.Id);
            if (deletedJob == null)
            {
                throw new NotFoundException(nameof(deletedJob), request.Id);
            }

            await _unitOfWork.JobRepository.Delete(deletedJob);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
