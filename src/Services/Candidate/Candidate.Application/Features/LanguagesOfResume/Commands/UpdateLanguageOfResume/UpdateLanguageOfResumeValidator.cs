using Candidate.Application.DTOs.LanguageOfResume;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.LanguagesOfResume.Commands.UpdateLanguageOfResume
{
    public class UpdateLanguageOfResumeValidator : AbstractValidator<UpdateLanguageOfResumeDTO>
    {
        public UpdateLanguageOfResumeValidator()
        {
            RuleFor(c => c.LanguageId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();

            RuleFor(c => c.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        }
    }
}
