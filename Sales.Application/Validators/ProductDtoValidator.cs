using FluentValidation;
using Sales.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Validators
{
    public class ProductDtoValidator: AbstractValidator<ProductDto>
    {
        public ProductDtoValidator() {
            RuleFor(p => p.Name).NotEmpty()
                                .WithMessage("Product is requied")
                                .MaximumLength(100)
                                .WithMessage("Product name must not exceed 100 characters.");

            RuleFor(p => p.Description).MaximumLength(500)
                                       .WithMessage("Description must not exceed 500 characters.");

            RuleFor(p => p.CurrentPrice).GreaterThanOrEqualTo(0)
                                        .WithMessage("Current price must be greater than or equal to 0.");

            RuleFor(p => p.StockQuantity).GreaterThanOrEqualTo(0)
                                         .WithMessage("Stock quantity must be greater than or equal to 0.");
        }
    }
}
