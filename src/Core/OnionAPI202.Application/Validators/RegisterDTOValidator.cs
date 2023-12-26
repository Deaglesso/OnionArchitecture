using FluentValidation;
using OnionAPI202.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Validators
{
    public class RegisterDTOValidator:AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().MaximumLength(256).Matches(@"^[-!#-'*+\/-9=?^-~]+(?:\.[-!#-'*+\/-9=?^-~]+)*@[-!#-'*+\/-9=?^-~]+(?:\.[-!#-'*+\/-9=?^-~]+)+$");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(150);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(150);
            RuleFor(x => x).Must(x => x.ConfirmPassword == x.Password);
            RuleFor(x => x.Username).NotEmpty().MaximumLength(50).MinimumLength(2);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(3);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(50).MinimumLength(3);

        }
    }
}
