using Business.Application.DTOs.BusinessInfor;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Commands.ReviewBusinessInfor
{
    public class ReviewBusinessInforValidator : AbstractValidator<ReviewBusinessInforDTO>
    {
        public ReviewBusinessInforValidator()
        {
            RuleFor(dto => dto.Id)
              .NotNull().WithMessage("Id is required")
              .NotEmpty().WithMessage("Is cannot be empty");

            RuleFor(dto => dto.IsApproved)
                 .NotNull().WithMessage("Id is required")
              .NotEmpty().WithMessage("Is cannot be empty");
        }

    }
}
