using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Career;
using Provider.Application.Responses;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Careers.Commands.CreateCareer
{
    public class CreateCareerCommandHandler : IRequestHandler<CreateCareerCommand, BaseCommandResponse>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;

        public CreateCareerCommandHandler(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCareerValidator();
            var validationResult = await validator.ValidateAsync(request.CareerDTO);

            if (await _careerRepository.IsExisted(request.CareerDTO.Name))
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors.Add("This career was already existed");
            }
            else if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var career = _mapper.Map<Career>(request.CareerDTO);
                career = await _careerRepository.Add(career);
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = career.Id;
            }
            return response;
        }
    }
}
