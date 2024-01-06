using AutoMapper;
using Business.Application.Contracts;
using Business.Application.Features.Areas.Commands.CreateArea;
using Business.Application.Responses;
using Business.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Commands.CreateBusinessInfor
{
    public class CreateBusinessInforCommandHandler : IRequestHandler<CreateBusinessInforCommand, List<BaseCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBusinessInforCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BaseCommandResponse>> Handle(CreateBusinessInforCommand request, CancellationToken cancellationToken)
        {
            var responseList = new List<BaseCommandResponse>();
            var validator = new CreateBusinessInforValidator();
            var validationResult = await validator.ValidateAsync(request.BusinessInforDTO);
            var response = new BaseCommandResponse();
            if (!validationResult.IsValid)
            {
                response.Id = 0;
                response.Success = false;
                response.Message = "Creation profile failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                responseList.Add(response);
            }
            else
            {
                var businessInfor = _mapper.Map<BusinessInfor>(request.BusinessInforDTO);
                await _unitOfWork.BusinessRepository.Add(businessInfor);
                response.Id = businessInfor.Id;
                response.Success = true;
                response.Message = "Creating profile successfully";
                responseList.Add(response);

                //Create Area for business
                var areaValidator = new CreateAreaValidator();
                var validationTasks = request.AreaDTOs.Select(dto => areaValidator.ValidateAsync(dto));
                var validationResults = await Task.WhenAll(validationTasks);

                if (validationResults.Any(result => result.IsValid == false))
                {
                    var areaResponse = new BaseCommandResponse();
                    var errors = validationResults.SelectMany(result => result.Errors);
                    areaResponse.Success = false;
                    areaResponse.Message = "Creation area Failed";
                    areaResponse.Errors = (List<string>)errors;
                    responseList.Add(areaResponse);
                }
                else
                {
                    
                    var areas = _mapper.Map<List<Area>>(request.AreaDTOs);
                    foreach (var area in areas)
                    {
                        var areaResponse = new BaseCommandResponse();
                        if (await _unitOfWork.AreaRepository.IsExisted(area.BusinessId, area.CareerId))
                        {
                            areaResponse.Id = 0;
                            areaResponse.Success = false;
                            areaResponse.Message = "Creation area Failed";
                            areaResponse.Errors.Add("An area already exists for BusinessId: " + area.BusinessId + " and CareerId: " + area.CareerId);
                        }
                        else
                        {
                            area.BusinessId = businessInfor.Id;
                            await _unitOfWork.AreaRepository.Add(area);
                            areaResponse.Success = true;
                            areaResponse.Message = "Creation Successful";
                            areaResponse.Id = area.Id;
                        }
                        responseList.Add(areaResponse);
                    }
                }
                await _unitOfWork.Save();
            }

            return responseList;
        }
    }
}
