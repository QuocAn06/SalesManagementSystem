using FluentValidation;
using Sales.Application.DTOs;
using Sales.Domain.Enums;
using System;
using System.Linq;

namespace Sales.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.");

            RuleFor(x => x.Username)
                .Must(u => !u.Contains(" "))
                .WithMessage("Username cannot contain spaces.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")    
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.Role)
                .Must(role => Enum.IsDefined(typeof(UserRole), role))
                .WithMessage("Invalid Role.");
        }
    }
}
