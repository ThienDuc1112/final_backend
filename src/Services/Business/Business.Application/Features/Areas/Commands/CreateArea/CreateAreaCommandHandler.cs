using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Responses;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaCommandHandler
    {
        public class AreaCommandHandler : IRequestHandler<CreateAreaCommand, BaseCommandResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public AreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<BaseCommandResponse> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
            {
                var response = new BaseCommandResponse();
                var validator = new CreateAreaValidator();
                var validationTasks = request.AreaDTO.Select(dto => validator.ValidateAsync(dto));
                var validationResults = await Task.WhenAll(validationTasks);

                if (validationResults.Any(result => result.IsValid == false))
                {
                    var errors = validationResults.SelectMany(result => result.Errors);
                    response.Success = false;
                    response.Message = "Creation Failed";
                    response.Errors = (List<string>)errors;
                }
                else
                {
                    var areas = _mapper.Map<List<Area>>(request.AreaDTO);
                    foreach (var area in areas)
                    {
                        if (await _unitOfWork.AreaRepository.IsExisted(area.BusinessId, area.CareerId))
                        {
                            response.Id = 0;
                            response.Success = false;
                            response.Message = "Creation Failed";
                            response.Errors.Add("An area already exists for BusinessId: " + area.BusinessId + " and CareerId: " + area.CareerId);
                        }
                        else
                        {
                            await _unitOfWork.AreaRepository.Add(area);
                            await _unitOfWork.Save();
                            response.Success = true;
                            response.Message = "Creation Successful";
                            response.Id = area.Id;
                        }   
                    }

                }
                return response;
            }
        }
    }
}
