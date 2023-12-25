using FluentValidation;
using OnionAPI202.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Validators
{
    public class CreateProductDTOValidator:AbstractValidator<CreateProductDTO> 
    {
        public CreateProductDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(100).WithMessage("Cannot exceed 100 characters")
                .MinimumLength(2).WithMessage("Minimum 2 characters required");



            RuleFor(x => x.SKU).NotEmpty().WithMessage("SKU cannot be empty")
                .Must(s => s.Length == 6).WithMessage("SKU Length must be 6");

            RuleFor(x => x.Price).Must(p => p >= 0);

            RuleFor(x => x.CategoryId).Must(c => c >= 0);

            RuleForEach(x=>x.ColorIds).Must(c => c >= 0);

        }
    }
}
