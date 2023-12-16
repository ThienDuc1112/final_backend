using FluentValidation;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Career;
using Provider.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Skills.Commands.CreateSkill
{
    public class CreateSkillValidator : AbstractValidator<CreateSkillDTO>
    {
        private readonly ICareerRepository _careerRepository;
        public CreateSkillValidator(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;

            RuleFor(s => s.NameSkill)
                .NotEmpty().WithMessage("Name of Skill is required.")
                .MaximumLength(60).WithMessage("Name of Skill must not exceed 60 characters.");

            RuleFor(s => s.CareerId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var careerExist = await _careerRepository.Exists(id);
                    return careerExist;
                }).WithMessage("Name of Skill does not exist");
        }
    }
}
