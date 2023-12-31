using Candidate.Application.DTOs.LanguageOfResume;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Features.LanguagesOfResume.Commands.CreateLanguageOfResume
{
    public class CreateLanguageOfResumeValidator : AbstractValidator<CreateLanguageOfResumeDTO>
    {
        public CreateLanguageOfResumeValidator()
        {
            RuleFor(c => c.LanguageId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();

            RuleFor(c => c.ResumeId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        }
    }
}
