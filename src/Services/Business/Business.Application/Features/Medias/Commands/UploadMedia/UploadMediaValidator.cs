using Business.Application.DTOs.Media;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Medias.Commands.UploadMedia
{
    public class UploadMediaValidator : AbstractValidator<UploadMediaDTO>
    {
        public UploadMediaValidator()
        {
            RuleFor(m => m.BusinessId).NotNull().WithMessage("BusinessId is required.")
              .NotEmpty().WithMessage("BusinessId cannot be empty.");

            RuleFor(m => m.Name).NotNull().WithMessage("Name is required.")
              .NotEmpty().WithMessage("Name cannot be empty.");

            RuleFor(m => m.Type).NotNull().WithMessage("Type is required.")
              .NotEmpty().WithMessage("Type cannot be empty.")
              .MaximumLength(50).WithMessage("Type must not exceed 50 characters.");
        }
    }
}
