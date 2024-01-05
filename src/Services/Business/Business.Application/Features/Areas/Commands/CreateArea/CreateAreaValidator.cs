using Business.Application.DTOs.Area;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaValidator : AbstractValidator<CreateAreaDTO>
    {
        public CreateAreaValidator()
        {
            RuleFor(a => a.CareerId).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(a => a.BusinessId).NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull();
        }
    }
}
