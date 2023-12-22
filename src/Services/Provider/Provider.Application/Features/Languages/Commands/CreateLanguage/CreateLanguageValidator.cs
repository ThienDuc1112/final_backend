using FluentValidation;
using Provider.Application.DTOs.Career;
using Provider.Application.DTOs.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageValidator : AbstractValidator<CreateLanguageDTO>
    {
        public CreateLanguageValidator()
        {
            RuleFor(o => o.Level)
                .NotEmpty().WithMessage("Level is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("Level must not exceed 30 characters.");

            RuleFor(o => o.LanguageName)
                .NotEmpty().WithMessage("LanguageName is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("Level must not exceed 30 characters.");
        }
    }
}
