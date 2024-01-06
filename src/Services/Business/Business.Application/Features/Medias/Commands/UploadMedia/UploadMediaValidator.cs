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
            RuleFor(m => m.BusinessId).NotNull().WithMessage("FullName is required.")
              .NotEmpty().WithMessage("FullName cannot be empty.");

            RuleFor(m => m.Name).NotNull().WithMessage("FullName is required.")
              .NotEmpty().WithMessage("FullName cannot be empty.");

            RuleFor(m => m.Type).NotNull().WithMessage("FullName is required.")
              .NotEmpty().WithMessage("FullName cannot be empty.")
              .MaximumLength(50).WithMessage("FullName must not exceed 50 characters.");
        }
    }
}
