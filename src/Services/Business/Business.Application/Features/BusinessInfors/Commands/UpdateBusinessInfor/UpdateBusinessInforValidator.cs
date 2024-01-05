using Business.Application.DTOs.BusinessInfor;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.BusinessInfors.Commands.UpdateBusinessInfor
{
    public class UpdateBusinessInforValidator : AbstractValidator<UpdateBusinessInforDTO>
    {

        public UpdateBusinessInforValidator()
        {
            RuleFor(dto => dto.Id)
                .NotNull().WithMessage("Id is required")
                .NotEmpty().WithMessage("Is cannot be empty");

            RuleFor(dto => dto.FullName)
              .NotNull().WithMessage("FullName is required.")
              .NotEmpty().WithMessage("FullName cannot be empty.")
              .MaximumLength(200).WithMessage("FullName must not exceed 200 characters.");


            RuleFor(dto => dto.FoundedYear)
                .NotNull().WithMessage("FoundedYear is required.")
                .NotEmpty().WithMessage("FoundedYear cannot be empty.")
                .Must(y => IsFourDigitNumber(y.ToString()))
                .WithMessage("FoundedYear must be a 4-digit number.")
                .InclusiveBetween(1800, DateTime.Now.Year).WithMessage($"FoundedYear must be between 1800 and {DateTime.Now.Year}.");

            RuleFor(dto => dto.BusinessSize)
                .NotNull().WithMessage("BusinessSize is required.")
                .NotEmpty().WithMessage("BusinessSize cannot be empty.")
                 .MaximumLength(60).WithMessage("BusinessSize must not exceed 60 characters.");

            RuleFor(dto => dto.TaxCode)
                .NotNull().WithMessage("TaxCode is required.")
                .NotEmpty().WithMessage("TaxCode cannot be empty.")
                .MaximumLength(60).WithMessage("TaxCode must not exceed 60 characters.");

            RuleFor(dto => dto.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
                .EmailAddress().WithMessage("Invalid Email.");

            RuleFor(dto => dto.PhoneNumber)
                .NotNull().WithMessage("PhoneNumber is required.")
                .NotEmpty().WithMessage("PhoneNumber cannot be empty.")
                .Must(IsPhoneNumberNumeric).WithMessage("{PropertyName} must be a number")
                .MaximumLength(15).WithMessage("Email must not exceed 15 characters.");

            RuleFor(dto => dto.LicenseFont)
                .NotNull().WithMessage("LicenseFont is required.")
                .NotEmpty().WithMessage("LicenseFont cannot be empty.");

            RuleFor(dto => dto.LicenseBack)
                .NotNull().WithMessage("LicenseBack is required.")
                .NotEmpty().WithMessage("LicenseBack cannot be empty.");

            RuleFor(dto => dto.Address)
                .NotNull().WithMessage("Address is required.")
                .NotEmpty().WithMessage("Address cannot be empty.");
        }

        private static bool IsFourDigitNumber(string number)
        {
            int numericValue;
            return int.TryParse(number, out numericValue) && number.Length == 4;
        }

        private bool IsPhoneNumberNumeric(string phoneNumber)
        {
            long numericValue;
            return long.TryParse(phoneNumber, out numericValue);
        }
    }
}
