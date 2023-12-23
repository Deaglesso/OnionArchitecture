using FluentValidation;
using OnionAPI202.Application.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Validators
{
    public class CreateTagDTOValidator:AbstractValidator<CreateTagDTO>
    {
        public CreateTagDTOValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
