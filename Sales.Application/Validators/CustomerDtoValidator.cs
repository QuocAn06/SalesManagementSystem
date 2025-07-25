using FluentValidation;
using Sales.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Validators
{
    public class CustomerDtoValidator: AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator() {
            RuleFor(x => x.Name).NotEmpty()
                              .WithMessage("Name is required")
                              .MaximumLength(100);

            RuleFor(x => x.Email).NotEmpty()
                                 .WithMessage("Email is requied")
                                 .EmailAddress()
                                 .WithMessage("Invalid email format");

            RuleFor(x => x.PhoneNumber).NotEmpty()
                                      .WithMessage("Phone number is required")
                                      .Matches(@"^\d{10,15}$")
                                      .WithMessage("Phone number must be 10 - 15 digits");

            RuleFor(x => x.Address).NotEmpty()
                                   .WithMessage("Address is required");
        }
    }
}
